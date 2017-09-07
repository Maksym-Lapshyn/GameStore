namespace PaymentService.Application.Infrastructure.Abstract
{
	public interface IConfirmationEmailSender
	{
		void Send(string confirmationCode);
	}
}