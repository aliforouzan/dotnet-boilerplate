﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBoilerPlate.Application.Filters.req.Behaviors;

public class ValidationResultPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
    where TResponse : IResult
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationResultPipelineBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validator = _serviceProvider.GetService<IValidator<TRequest>>();

        if (validator != null)
        {
        
            var result = await validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                // Reference: https://github.com/amantinband/error-or/issues/10
                /* Due to not wanting to use reflection, we assume that every request
                 * that wants to validate something also returns a result.
                 * Using implicit casts, we are able to use this same behavior for all of them
                 */
                return (dynamic)Result.Invalid(result.AsErrors());
            }
        }
        
        return await next();
    }
}


