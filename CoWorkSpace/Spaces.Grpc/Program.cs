
using CDN.Grpc.Protos;
using Spaces.Grpc.Extensions;
using Spaces.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCommonServices(builder.Configuration);

// Add Grpc services to the container.
builder.Services.AddGrpcClient<ImageProtoService.ImageProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:ImagesUri")!)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<SpaceGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
