using Grpc.Core;

namespace HelloDapr.Service.Math.Services;

public class MathService : Addition.AdditionBase
{
    public override Task<AddNumbersReply> AddNumbers(AddNumbersRequest request, ServerCallContext context) =>
        Task.FromResult<AddNumbersReply>(new() { Result = request.Number.Sum() });
}