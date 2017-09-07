using PaymentService.Application.Infrastructure.Abstract;

namespace PaymentService.Application.Infrastructure.Concrete
{
	public class ConfirmationEmailSenderStub : IConfirmationEmailSender
	{
		public void SendEmail(string email, string confirmationCode)
		{
			//does something
		}
	}
}