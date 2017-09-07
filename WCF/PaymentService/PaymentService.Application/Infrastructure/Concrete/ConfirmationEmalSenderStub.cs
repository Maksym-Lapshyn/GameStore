using PaymentService.Application.Infrastructure.Abstract;

namespace PaymentService.Application.Infrastructure.Concrete
{
	public class ConfirmationEmailSenderStub : IConfirmationEmailSender
	{
		public void Send(string confirmationCode)
		{
			//does something
		}
	}
}