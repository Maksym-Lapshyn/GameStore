using PaymentService;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using PaymentService.Initializers;
using PaymentService.Services.Abstract;
using PaymentService.Services.Concrete;

namespace ConsoleHost
{
	public class Program
	{
		private const string Uri = "http://localhost:8733/payment";

		private static void Run()
		{
			ServiceHost host = null;

			try
			{
				using (host = new ServiceHost(typeof(Payment)))
				{
					var endpoint = host.AddServiceEndpoint(typeof(IPayment), new WSHttpBinding(), Uri);
					host.Faulted += NotifyHostFault;
					var behaviour = new ServiceMetadataBehavior { HttpGetEnabled = true };
					host.Description.Behaviors.Add(behaviour);
					host.Open();
					Console.WriteLine(
						$"The Product service is running and is listening on: {endpoint.Address}, {endpoint.Binding.Name}");
					Console.WriteLine("Press any key to stop the service.");
					Console.ReadKey();
				}
			}
			finally
			{
				if (host?.State == CommunicationState.Faulted)
				{
					host.Abort();
				}
				else
				{
					host?.Close();
				}
			}
		}

		private static void NotifyHostFault(object sender, EventArgs e)
		{
			Console.WriteLine("The host was faulted.");
		}

		public static void Main(string[] args)
		{
			AccountsRepositoryInitializer.Initialize();
			Run();
		}
	}
}