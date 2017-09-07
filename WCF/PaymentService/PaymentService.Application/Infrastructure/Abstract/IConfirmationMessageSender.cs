namespace PaymentService.Application.Infrastructure.Abstract
{
	public interface IConfirmationMessageSender
	{
		void Send(string confirmationCode);
	}
}