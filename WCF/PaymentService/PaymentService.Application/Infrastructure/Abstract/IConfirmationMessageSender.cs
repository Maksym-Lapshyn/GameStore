namespace PaymentService.Application.Infrastructure.Abstract
{
	public interface IConfirmationMessageSender
	{
		void SendPhone(string phone, string confirmationCode);
	}
}