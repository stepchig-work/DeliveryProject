using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDelivery.Business.Entities;

namespace FoodDelivery.DataAccess
{
	public class AccountConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.HasKey(account => account.AccountId);
			builder.Ignore(account => account.EntityId);

			builder.HasIndex(account => account.UserName)
				.IsUnique();

			builder.Property(account => account.Name)
				.HasMaxLength(50);
			builder.Property(account => account.Surname)
				.HasMaxLength(50);
			builder.Property(account => account.UserName)
				.HasMaxLength(50);
			builder.Property(account => account.Address)
				.HasMaxLength(100);
		}
	}
}
