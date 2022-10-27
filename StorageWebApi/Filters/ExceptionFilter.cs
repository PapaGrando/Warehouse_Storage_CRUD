using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Storage.DataBase.Exceptions;

namespace Storage.WebApi.Filters
{
    public class ExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        private ILogger<ExceptionFilter> _logger;

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger = context.HttpContext.RequestServices
                .GetService<ILogger<ExceptionFilter>>();

            var ex = context.Exception;

            if (ex is BaseDataException)
            {
                ProceedExceptionHandling(context);

                context.ExceptionHandled = true;
                return;
            }

            _logger.LogError(ex, ex.Message);
#if DEBUG
            //in DEBUG displaing exception data on page
            context.ExceptionHandled = false;
#else
            //handle exception on RELEASE and returning status 500
            context.ExceptionHandled = true;
            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Content = "Unknown Internal Server Error"
            };
#endif
        }

        private void ProceedExceptionHandling(ExceptionContext context)
        {
            var ex = context.Exception;

            if (ex is NotFound)
            {
                _logger.LogWarning($"Entity not found in db: {ex.Message}");
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Content = ex.Message
                };

                return;
            }

            if (ex is NoCascadeDeletionException || ex is StorageItemDoesNotFitInCell)
            {
                _logger.LogWarning($"inDatabase Conflict: {ex.Message}");
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Content = ex.Message
                };

                return;
            }
        }
    }
}
