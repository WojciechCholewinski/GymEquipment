using GymEquipment.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace GymEquipment.Api.Contracts;

public static class ValidationResultExtensions
{
    public static IActionResult ToUnprocessableEntity(this ValidationResult validation, ControllerBase controller)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Validation failed"
        };

        var errors = validation.Errors
            .GroupBy(e => e.Field ?? string.Empty)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => $"{e.Code}: {e.Message}").ToArray());

        foreach (var kv in errors)
        {
            problemDetails.Errors.Add(kv.Key, kv.Value);
        }

        return controller.UnprocessableEntity(problemDetails);
    }
}