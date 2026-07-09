using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Application.ACL;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services;
using Moq;

namespace KiWhisky.FrutiLogicPlatform.Tests.AlertsAndNotifications.Application;

public class AlertsAndNotificationsContextFacadeTests
{
    [Fact]
    public async Task CreateAlert_PreservesAccountAndInventoryOrder()
    {
        CreateAlertCommand? captured = null;
        var commandService = new Mock<IAlertCommandService>();
        commandService
            .Setup(service => service.Handle(It.IsAny<CreateAlertCommand>()))
            .Callback<CreateAlertCommand>(command => captured = command)
            .ReturnsAsync((Alert?)null);
        var facade = new AlertsAndNotificationsContextFacade(
            commandService.Object,
            Mock.Of<IAlertQueryService>());

        await facade.CreateAlert(
            "title", "message", "Warning", "ProductLowStock", "account-1", "inventory-1");

        Assert.NotNull(captured);
        Assert.Equal("account-1", captured.AccountId.GetId);
        Assert.Equal("inventory-1", captured.InventoryId.GetId);
    }
}
