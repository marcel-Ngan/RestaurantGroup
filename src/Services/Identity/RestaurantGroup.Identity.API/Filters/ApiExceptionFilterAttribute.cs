using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RestaurantGroup.Identity.Application.Exceptions;
using RestaurantGroup.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace RestaurantGroup.Identity.API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ApplicationValidationException), HandleValidationException },
                { typeof(UserAlreadyExistsException), HandleUserAlreadyExistsException },
                { typeof(InvalidCredentialsException), HandleInvalidCredentialsException },
                { typeof(UserNotFoundException), HandleUserNotFoundException },
                { typeof(RoleNotFoundException), HandleRoleNotFoundException },
                { typeof(UserNotActiveException), HandleUserNotActiveException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ApplicationValidationException)context.Exception;
            
            // Convert IReadOnlyDictionary to Dictionary for the ValidationProblemDetails constructor
            var errors = new Dictionary<string, string[]>(exception.Errors);
            
            var details = new ValidationProblemDetails(errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleUserAlreadyExistsException(ExceptionContext context)
        {
            var exception = context.Exception;
            
            var details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "User already exists",
                Detail = exception.Message
            };

            context.Result = new ConflictObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleInvalidCredentialsException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Invalid credentials",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

            context.Result = new UnauthorizedObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleUserNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception;
            
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User not found",
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleRoleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception;
            
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Role not found",
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleUserNotActiveException(ExceptionContext context)
        {
            var exception = context.Exception;
            
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "User account is disabled",
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred");
            
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}