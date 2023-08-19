using LanguageExt.Common;

namespace MinimalMediator.Features.Example;

public readonly record struct ExampleRequest : IHttpRequest
{
  public string Name { get; init; }

  public int Age { get; init; }
}
