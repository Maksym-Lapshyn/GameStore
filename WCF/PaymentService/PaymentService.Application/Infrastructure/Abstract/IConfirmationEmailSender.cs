namespace PaymentService.Application.Infrastructure.Abstract
{
	public interface IConfirmationEmailSender
	{
		void SendEmail(string email, string confirmationCode);
	}
}