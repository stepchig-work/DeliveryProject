using FluentValidation;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IBaseValidator<in TEntity> : IValidator<TEntity>
		where TEntity : IIdentifiableEntity
	{
		void ValidateThrow(TEntity entity);
	}
}
