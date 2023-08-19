using FluentValidation;
using Mediator;
using MinimalMediator.Features;

namespace MinimalMediator.Validation;

public sealed class RequestValidatorBehavior<TMessage, TResult> : IPipelineBehavior<TMessage, IResult>
  where TMessage : IHttpRequest
{
  public IValidator<TMessage> _validator { get; }

  public RequestValidatorBehavior(IValidator<TMessage> validator)
  {
    _validator = validator;
  }

  public async ValueTask<IResult> Handle(
    TMessage message,
    CancellationToken cancellationToken,
    MessageHandlerDelegate<TMessage, IResult> next
  )
  {
    var result = await _validator.ValidateAsync(message);
    if (!result.IsValid)
    {
      return new ValidationFailedResult(result.Errors);
    }

    return await next(message, cancellationToken);
  }
}
