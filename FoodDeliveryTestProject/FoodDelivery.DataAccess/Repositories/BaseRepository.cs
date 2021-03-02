using System;
using System.Diagnostics.Contracts;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using FoodDelivery.Common.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using FoodDelivery.DataAccess.Interface;
using FluentValidation;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public abstract class BaseRepository<TEntity, TDbContext> : IRepository<TEntity>, IDisposable
		where TEntity : class, IIdentifiableEntity, new()
		where TDbContext : DbContext
	{
		protected readonly TDbContext dbContext;
		protected readonly IBaseValidator<TEntity> validator;
		protected readonly IMapper mapper;
		private bool disposed;

		protected BaseRepository(TDbContext dbContext,
								IBaseValidator<TEntity> validator,
								IMapper mapper)
		{
			Contract.Requires(dbContext != null);
			Contract.Requires(validator != null);
			Contract.Requires(mapper != null);

			this.dbContext = dbContext;
			this.validator = validator;
			this.mapper = mapper;
		}
		protected abstract TEntity FindById(int id, TDbContext dbContext);

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			ValidateEntity(entity);

			var added = await dbContext.Set<TEntity>().AddAsync(entity);
			dbContext.SaveChanges();
			return added.Entity;
		}


		public void Remove(TEntity entity)
		{
			Remove(entity.EntityId);
		}

		public void Remove(int id)
		{
			var entity = FindById(id, dbContext);
			dbContext.Entry(entity).State = EntityState.Deleted;
			dbContext.SaveChanges();
		}

		public TEntity Update(TEntity entity)
		{
			validator.ValidateThrow(entity);

			var existingEntity = FindById(entity.EntityId, dbContext);
			mapper.Map(entity, existingEntity);
			dbContext.Entry(existingEntity).State = EntityState.Modified;
			dbContext.SaveChanges();
			return existingEntity;
		}

		public async Task AddRange(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				validator.ValidateThrow(entity);
			}

			await dbContext.Set<TEntity>().AddRangeAsync(entities);
			dbContext.SaveChanges();
		}

		public TEntity Find(int id)
		{
			return FindById(id, dbContext);
		}

		public IEnumerable<TEntity> GetEntities()
		{
			return dbContext.Set<TEntity>().ToList();
		}

		protected void ValidateEntity(TEntity entity)
		{
			if (entity.EntityId != 0)
			{
				throw new ValidationException("New entity Id cannot have a prederemined Id");
			}
			validator.ValidateThrow(entity);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					dbContext.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
