using Api.GrpcServices;
using Api.Interceptors;
using Api.Mapping;
using Application.Extensions;
using FluentValidation;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(opt =>
{
    opt.Interceptors.Add<ExceptionInterceptor>();
    opt.Interceptors.Add<ValidationInterceptor>();
    opt.Interceptors.Add<LoggingInterceptor>();
}).AddJsonTranscoding();

builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddAutoMapper(typeof(ProductTypeProfile), typeof(ProductProfile), typeof(DateTimeProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<ProductGrpcService>();

app.Run();