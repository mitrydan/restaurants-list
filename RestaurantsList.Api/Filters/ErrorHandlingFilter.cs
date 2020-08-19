using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RestaurantsList.Api.ApiResponses;
using Serilog;
using System;

namespace RestaurantsList.Api.Filters
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private const string InternalServerErrorMessage = "Internal server error";

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var statusCode = StatusCodes.Status500InternalServerError;
            var response = SerializeException(StatusCodes.Status500InternalServerError, InternalServerErrorMessage);

            switch (exception)
            {
                case Exceptions.ApplicationNotFoundException ex:
                    statusCode = StatusCodes.Status404NotFound;
                    response = SerializeException(StatusCodes.Status404NotFound, ex.Message);
                    break;

                case Exceptions.ApplicationException ex:
                    break;
            }

            context.Result = new JsonResult(response) { StatusCode = statusCode };
            context.ExceptionHandled = true;

            Log.Error(exception, InternalServerErrorMessage);
        }

        private string SerializeException(int statusCode, string message)
        {
            return JsonConvert.SerializeObject(new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
            });
        }
    }
}
