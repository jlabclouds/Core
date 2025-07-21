var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.WebAPI>("corewebapi");

builder.AddProject<Projects.WebAPInAOT>("core-webapinaot");

builder.AddProject<Projects.GrpcService>("grpcservice");

builder.AddProject<Projects.GrpcServicenAOT>("grpcservicenaot");

builder.Build().Run();
