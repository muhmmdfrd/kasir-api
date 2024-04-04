﻿using KasirApi.Api.Constants;
using KasirApi.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KasirApi.Api.Commons
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException)
            {
                context.Result = new ObjectResult(new ApiResponse<object>
                {
                    Success = false,
                    Code = ResponseConstant.BAD_REQUEST_CODE,
                    Message = ResponseConstant.BAD_REQUEST_MESSAGE,
                    Data = context.ModelState
                });
                context.ExceptionHandled = true;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new ApiResponse<object>
                {
                    Success = false,
                    Code = ResponseConstant.BAD_REQUEST_CODE,
                    Message = ResponseConstant.BAD_REQUEST_MESSAGE,
                    Data = context.ModelState
                });
            }
        }
    }

    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;
        public object? Value { get; set; } = default;
    }
}
