using PaymentService.Application.Infrastructure.Abstract;

namespace PaymentService.Application.Infrastructure.Concrete
{
	public class ConfirmationMessageSenderStub : IConfirmationMessageSender
	{
		public void Send(string confirmationCode)
		{
			//does something
		}
	}
}