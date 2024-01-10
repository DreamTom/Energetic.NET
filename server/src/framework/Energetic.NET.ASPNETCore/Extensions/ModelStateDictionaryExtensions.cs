using Energetic.NET.ASPNETCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ModelStateDictionaryExtensions
    {
        public static IActionResult Formatter(this ModelStateDictionary modelState)
        {
            var error = modelState.Values.First(v => v.ValidationState == ModelValidationState.Invalid).Errors.First().ErrorMessage;
            return new BadRequestObjectResult(new ErrorResponseResult(error));
        }
    }
}
