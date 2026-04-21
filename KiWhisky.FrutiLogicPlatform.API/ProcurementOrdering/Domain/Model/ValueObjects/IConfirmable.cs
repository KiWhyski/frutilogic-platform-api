namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.ValueObjects;

public interface IConfirmable
{
    void ProcessOrder();
    void ConfirmOrder();
    void ShipOrder();
    void ReceiveOrder();
    void CancelOrder();
}
