using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ContosoUniversity.Middleware;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : new()
    where TRequest : notnull, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        var context = new ValidationContext<TRequest>(request);
        var errors = new List<ValidationFailure>();

        foreach (var validator in _validators)
        {
            var err = await validator.ValidateAsync(context,cancellationToken);
            if (err != null && err.Errors.Any())
            {
                errors.AddRange(err.Errors);
            }                                
        }

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}