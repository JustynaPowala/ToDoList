using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using ToDoList.WebApi.Exceptions;
using ToDoListContracts;

namespace ToDoList.WebApi
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainValidationException validationException)
            {
                context.Result = new ObjectResult(new ErrorInfo(validationException.Message))
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            else if (context.Exception is EntityNotFoundException entityNotFound)
            {
                context.Result = new ObjectResult(new ErrorInfo(entityNotFound.Message))
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            else
            {
                context.Result = new ObjectResult(new ErrorInfo(context.Exception.Message))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
