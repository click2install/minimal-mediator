using System.Text.Json;
using FluentValidation;
using Mediator;
using MinimalMediator.Extensions;
using MinimalMediator.Features.Example;
using MinimalMediator.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton)

  .AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped)
  .AddSingleton(typeof(IPipelineBehavior<,>), typeof(RequestValidatorBehavior<,>))

  .Configure<JsonSerializerOptions>(options =>
  {
    options.WriteIndented = true;
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  });

var app = builder.Build();
app
  .Mediate<ExampleRequest>(x => x.MapGet, "/example/{name}")
  .Run();
