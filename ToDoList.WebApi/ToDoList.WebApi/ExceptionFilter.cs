using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using ToDoListContracts;

namespace ToDoList.WebApi
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context) 
		{ 
			if(context.Exception is DomainValidationException validationException)
			{
				context.Result = new ObjectResult(new ErrorInfo(validationException.Message))
				{
					StatusCode = (int)HttpStatusCode.BadRequest
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
