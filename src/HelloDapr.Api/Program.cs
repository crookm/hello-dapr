using Dapr.Client;
using HelloDapr.Service.Greeter;
using HelloDapr.Service.Math;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDaprClient();


var daprGrpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT");
var daprProxyUri = new Uri($"http://127.0.0.1:{daprGrpcPort}");

builder.Services.AddGrpcClient<Addition.AdditionClient>(options => options.Address = daprProxyUri)
    .AddInterceptor(() => new InvocationInterceptor("math-service", null));
builder.Services.AddGrpcClient<Greeter.GreeterClient>(options => options.Address = daprProxyUri)
    .AddInterceptor(() => new InvocationInterceptor("greeter-service", null));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();