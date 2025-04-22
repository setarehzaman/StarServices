using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.Feature;

namespace STS.Infrastructure.SqlServer.Configurations.Feature;

public class OrderStatusConfigs : IEntityTypeConfiguration<OrderStatus>
{
    // the table for the OrderStatusEnum in DataBase
    public void Configure(EntityTypeBuilder<OrderStatus> builder) 
    {

        builder.HasKey(x => x.Id);

        #region SeedData

        builder.HasData(
            new OrderStatus { Id = 1, Title = "AwaitingSuggestions", DisplayName = "منتظر پیشنهاد متخصصان" },
            new OrderStatus { Id = 2, Title = "SelectingExpert", DisplayName = "منتظر انتخاب متخصص" },
            new OrderStatus { Id = 3, Title = "ExpertEnRoute", DisplayName = "منتظر آمدن متخصص به محل شما" },
            new OrderStatus { Id = 4, Title = "JobInProgress", DisplayName = "در دست انجام" },
            new OrderStatus { Id = 5, Title = "JobCompleted", DisplayName = "اتمام کار" },
            new OrderStatus { Id = 5, Title = "Paid", DisplayName = "پرداخت شده" }
            );

        #endregion
    }
}
