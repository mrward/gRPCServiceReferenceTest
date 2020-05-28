using System;
using GrpcGreeter;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace GrpcGreeterClient
{
	class Program
	{
		static void Main (string[] args)
		{
			try {
				Run ().Wait ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		static async Task Run ()
		{
			// https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1#call-insecure-grpc-services-with-net-core-client
			AppContext.SetSwitch ("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

			using var channel = GrpcChannel.ForAddress ("http://localhost:5000");
			var client = new Greeter.GreeterClient (channel);
			var reply = await client.SayHelloAsync (new HelloRequest { Name = "GreeterClient" });
			Console.WriteLine ("Greeting: " + reply.Message);
			Console.WriteLine ("Press any key to exit...");
			Console.ReadKey ();
		}
	}
}
