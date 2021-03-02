using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Common.Interface
{
	public interface IRepository<TEntity> 
		where TEntity: class, IIdentifiableEntity, new()
	{
		public Task<TEntity> AddAsync(TEntity entity);
		public Task AddRange(IEnumerable<TEntity> entities);
		public void Remove(TEntity entity);
		public void Remove(int id);
		public TEntity Update(TEntity entity);
		public TEntity Find(int id);
		public IEnumerable<TEntity> GetEntities();
	}
}
