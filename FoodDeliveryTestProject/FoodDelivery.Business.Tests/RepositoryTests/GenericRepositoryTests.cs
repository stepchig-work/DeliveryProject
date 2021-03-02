using AutoMapper;
using AutoMapper.Configuration;
using FoodDelivery.Common.Interface;
using FoodDelivery.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Business.Tests.RepositoryTests
{
	public abstract class GenericRepositoryTests<TEntity, TRepository>
		: IClassFixture<TestFixture<Startup>>, IDisposable
		where TEntity : class, IIdentifiableEntity, new()
		where TRepository : IRepository<TEntity>
	{
		public readonly TRepository repository;
		public readonly IFoodDeliveryDbContext context;
		public readonly IMapper mapper;
		public readonly TestFixture<Startup> fixture;
		public readonly IConfiguration configuration;

		public GenericRepositoryTests(TestFixture<Startup> fixture)
		{
			Contract.Requires(fixture != null);

			this.fixture = fixture;
			repository = (TRepository)fixture.Server.Host.Services.GetService(typeof(TRepository));
			mapper = (IMapper)fixture.Server.Host.Services.GetService(typeof(IMapper));
			configuration = (IConfiguration)fixture.Server.Host.Services.GetService(typeof(IConfiguration));
			context = CreateDbContext();
		}
		private IFoodDeliveryDbContext CreateDbContext()
		{
			var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;
			var dbContext = new FoodDeliveryDbContext(options);
			dbContext.SaveChanges();
			return dbContext;
		}

		[Fact]
		public async Task FindAccountTest()
		{
			//Arrange
			var generatedEntity = await GetNewAddedEntity();
			try
			{
				//Act
				var foundEntity = repository.Find(generatedEntity.EntityId);

				//Assert
				Assert.NotNull(foundEntity);
			}
			finally
			{
				//Clean
				CleaningDb(generatedEntity);
			}
		}

		[Fact]
		public async Task DeleteAccountTest()
		{
			//Arrange
			var generatedEntity = await GetNewAddedEntity();
			try
			{
				//Act
				repository.Remove(generatedEntity);
				var foundEntity = repository.Find(generatedEntity.EntityId);

				//Assert
				Assert.Null(foundEntity);

			}
			finally
			{
				//Clean
				CleaningDb(generatedEntity);
			}
		}
		[Fact]
		public async Task DeleteAccountByIdTest()
		{
			//Arrange
			var generatedEntity = await GetNewAddedEntity();
			try
			{
				//Act
				repository.Remove(generatedEntity.EntityId);
				var foundEntity = repository.Find(generatedEntity.EntityId);

				//Assert
				Assert.Null(foundEntity);
			}
			finally
			{
				//Clean
				CleaningDb(generatedEntity);
			}
		}

		protected abstract Task<TEntity> GetNewAddedEntity();
		protected abstract void CleaningDb(TEntity entity);

		public virtual void Dispose()
		{
			context.Dispose();
		}
	}
}
