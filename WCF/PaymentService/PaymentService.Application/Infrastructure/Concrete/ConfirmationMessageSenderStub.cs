using PaymentService.Application.Infrastructure.Abstract;

namespace PaymentService.Application.Infrastructure.Concrete
{
	public class ConfirmationMessageSenderStub : IConfirmationMessageSender
	{
		public void SendPhone(string phone, string confirmationCode)
		{
			//does something
		}
	}
}