using Common.Abstract;
using Common.Concrete;
using Ninject;
using Ninject.Web.Common;
using PaymentService.Application.Infrastructure.Abstract;
using PaymentService.Application.Infrastructure.Concrete;
using PaymentService.DAL.Abstract;
using PaymentService.DAL.Concrete;

namespace PaymentService.Application
{
	public class Global : NinjectHttpApplication
	{
		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			AddBindings(kernel);

			return kernel;
		}

		private void AddBindings(IKernel kernel)
		{
			kernel.Bind<IPaymentRepository>().To<PaymentRepositoryStub>().InSingletonScope();
			kernel.Bind<IAccountRepository>().To<AccountRepositoryStub>().InSingletonScope();
			kernel.Bind<IConfirmationEmailSender>().To<ConfirmationEmailSenderStub>();
			kernel.Bind<IConfirmationMessageSender>().To<ConfirmationMessageSenderStub>();
			kernel.Bind<ILogger>().To<Logger>();
		}
	}
}