﻿using FluentValidation;
using Microsoft.AspNetCore.Builder;

namespace DotnetBoilerPlate.Api.Configurations;

public static class ValidationSetup
{
    public static void AddValidationSetup(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<Application.IAssemblyMarker>();
    }
}
