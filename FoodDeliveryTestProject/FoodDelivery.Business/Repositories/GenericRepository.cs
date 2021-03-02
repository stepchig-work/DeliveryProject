using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Repositories
{
	public abstract class GenericRepository<TInnerRepository, TClientEntity, TBusinessEntity> : IRepository<TClientEntity>
		where TClientEntity : class, IIdentifiableEntity, new()
		where TBusinessEntity : class, IIdentifiableEntity, new()
		where TInnerRepository : IRepository<TBusinessEntity>
	{
		protected IMapper mapper;
		protected TInnerRepository innerRepository;
		public GenericRepository(IMapper mapper, TInnerRepository innerRepository)
		{
			Contract.Requires(mapper != null);
			Contract.Requires(innerRepository != null);

			this.mapper = mapper;
			this.innerRepository = innerRepository;
		}
		public async Task<TClientEntity> AddAsync(TClientEntity entity)
		{
			var addedEntity = mapper.Map<TBusinessEntity>(entity);
			var resultEntity = await innerRepository.AddAsync(addedEntity);
			return mapper.Map<TClientEntity>(resultEntity);
		}

		public async Task AddRange(IEnumerable<TClientEntity> entities)
		{
			var addedEntities = new List<TBusinessEntity>();
			foreach (var entity in entities)
			{
				addedEntities.Add(mapper.Map<TBusinessEntity>(entity));
			}
			await innerRepository.AddRange(addedEntities);
		}

		public TClientEntity Find(int id)
		{
			var result = innerRepository.Find(id);
			return mapper.Map<TClientEntity>(result);
		}

		public IEnumerable<TClientEntity> GetEntities()
		{
			var allEntities =  innerRepository.GetEntities();
			var result = new List<TClientEntity>();
			foreach(var entity in allEntities)
			{
				result.Add(mapper.Map<TClientEntity>(entity));
			}
			return result;
		}

		public void Remove(TClientEntity entity)
		{
			var removeEntity = mapper.Map<TBusinessEntity>(entity);
			innerRepository.Remove(removeEntity);
		}

		public void Remove(int id)
		{
			innerRepository.Remove(id);
		}

		public TClientEntity Update(TClientEntity entity)
		{
			var updateEntity = mapper.Map<TBusinessEntity>(entity);
			var resultEntity = innerRepository.Update(updateEntity);
			return mapper.Map<TClientEntity>(resultEntity);
		}
	}
}
