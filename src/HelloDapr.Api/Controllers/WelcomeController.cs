using HelloDapr.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HelloDapr.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WelcomeController : ControllerBase
{
    private readonly ILogger<WelcomeController> _logger;
    private readonly Greeter.GreeterClient _greeterClient;
    private readonly Addition.AdditionClient _additionClient;

    public WelcomeController(ILogger<WelcomeController> logger, Greeter.GreeterClient greeterClient, Addition.AdditionClient additionClient)
    {
        _logger = logger;
        _greeterClient = greeterClient;
        _additionClient = additionClient;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IActionResult> WelcomeAsync(string name, int yearBorn, CancellationToken token = default)
    {
        _logger.LogInformation("Creating a greeting for {Name}", name);
        var greeting = await _greeterClient.SayHelloAsync(new HelloRequest { Name = name }, cancellationToken: token);
        var age = await _additionClient.AddNumbersAsync(new AddNumbersRequest { Number = { DateTimeOffset.Now.Year, -yearBorn } }, cancellationToken: token);
        
        return Ok($"{greeting.Message} You are {age.Result} years old.");
    }
}