using Grpc.Core;
using HelloDapr.Contracts;

namespace HelloDapr.Service.Greeter.Services;

public class GreeterService : Contracts.Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name}!"
        });
    }
}