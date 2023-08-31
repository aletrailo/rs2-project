using CDN.Grpc.Protos;
using Spaces.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Grpc services to the container.
builder.Services.AddGrpcClient<ImageProtoService.ImageProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:ImagesUri")!)
);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
