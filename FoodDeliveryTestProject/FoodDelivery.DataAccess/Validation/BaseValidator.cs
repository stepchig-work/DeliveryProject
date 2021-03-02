using FluentValidation;

using FoodDelivery.Common.Interface;

namespace FoodDelivery.DataAccess
{
	public class BaseValidator<TEntity> : AbstractValidator<TEntity>, IValidator<TEntity>
		where TEntity: IIdentifiableEntity
	{		
		public void ValidateThrow(TEntity entity)
		{
			var validationResult = Validate(entity);
			if (!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.Errors);				
			}
		}
	}
}
