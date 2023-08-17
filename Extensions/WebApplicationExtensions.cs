using Mediator;
using MinimalMediator.Features;

namespace MinimalMediator.Extensions;

public static class WebApplicationExtensions
{
  public static WebApplication MediateGet<TRequest>(this WebApplication app, string template)
    where TRequest : IHttpRequest
  {
    app.MapGet(template, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));

    return app;
  }
}
