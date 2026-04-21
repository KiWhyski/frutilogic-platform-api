using Cortex.Mediator;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Infrastructure.Persistence.MongoDB.Repositories;

public class SalesOrderRepository : BaseRepository<SalesOrder>, ISalesOrderRepository
{
    private readonly IMongoCollection<SalesOrder> _salesOrders;

    public SalesOrderRepository(AppDbContext context, IMediator mediator) : base(context, mediator)
    {
        _salesOrders = context.GetCollection<SalesOrder>();
    }

    public async Task<SalesOrder> GenerateSalesOrder(GenerateSalesOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var salesOrder = new SalesOrder(
            command.orderCode,
            command.purchaseOrderId,
            command.items,
            command.status,
            command.catalogToBuyFrom,
            command.receiptDate,
            command.completitionDate,
            command.accountId);
        return salesOrder;
    }

    public async Task<IEnumerable<SalesOrder>> GetAllSalesOrdersByBuyerId(AccountId accountId)
    {
        ArgumentNullException.ThrowIfNull(accountId);

        var filter = Builders<SalesOrder>.Filter.Eq(x => x.AccountId, accountId);
        return await _salesOrders.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<SalesOrder>> GetAllSalesOrdersBySupplierId(AccountId supplierId)
    {
        ArgumentNullException.ThrowIfNull(supplierId);

        var filter = Builders<SalesOrder>.Filter.Eq(x => x.SupplierId, supplierId);
        return await _salesOrders.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<SalesOrder>> GetAllSalesOrders()
    {
        return await _salesOrders.Find(_ => true).ToListAsync();
    }

    public async Task<SalesOrder> GetByIdAsync(string id)
    {
        return await FindByIdAsync(id);
    }
}

