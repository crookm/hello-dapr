# Hello Dapr

### Running the services

In the directory of each service, run the following command:

```sh
dapr run --app-id <service id> --app-port <hosted port> -- dotnet run
```

Where `<service id>` is one of `math-service`, `greeter-service`, or `api-service`. These are arbitrary tags, but must be shared across all applications. In this set of services, you'll only see it used in `src/HelloDapr.Api/Program.cs` where I configure a gRPC inteceptor to add the id to every request as metadata. This is so the Dapr gRPC proxy knows where to direct requests.

The `<hosted port>` is the port where the application will run, which is where Dapr will direct requests.
