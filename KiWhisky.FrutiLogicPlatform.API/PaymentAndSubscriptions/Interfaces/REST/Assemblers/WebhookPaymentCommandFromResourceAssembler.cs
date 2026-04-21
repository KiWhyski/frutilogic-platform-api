using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Assemblers;

public class WebhookPaymentCommandFromResourceAssembler
{
    public static WebhookPaymentCommand ToCommandFromResource(string paymentId)
    {
        return new WebhookPaymentCommand(paymentId);
    }   
}
