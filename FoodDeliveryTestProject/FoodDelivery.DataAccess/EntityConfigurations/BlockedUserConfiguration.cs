using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDelivery.Business.Entities;

namespace FoodDelivery.DataAccess
{
	public class BlockedUserConfiguration : IEntityTypeConfiguration<BlockedUsers>
	{
		public void Configure(EntityTypeBuilder<BlockedUsers> builder)
		{
			builder.HasKey(blockedUser => blockedUser.BlockedUserId);
			builder.Ignore(blockedUser => blockedUser.EntityId);
						
			builder.HasOne(blockedUser => blockedUser.Owner)
				.WithMany(account => account.BlockedUsers)
				.HasForeignKey(blockedUser => blockedUser.OwnerId);

		}
	}
}
