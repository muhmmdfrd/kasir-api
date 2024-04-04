using System.Net;
using KasirApi.Api.Constants;
using KasirApi.Api.Models;
using KasirApi.Core.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace KasirApi.Api.Commons
{
    public class FlozaApiController : ControllerBase
    {
        protected IHttpContextAccessor _contextAccessor;

        public FlozaApiController()
        {
            _contextAccessor = new HttpContextAccessor();
        }

        public FlozaApiController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        [NonAction]
        protected ObjectResult ApiOK()
        {
            var response = new ApiResponse<object?>().SuccessResponse(null, ResponseConstant.SUCCESS_MESSAGE);
            return Ok(response);
        }

        [NonAction]
        protected ObjectResult ApiOK<T>(T data)
        {
            var response = new ApiResponse<T>().SuccessResponse(data);
            return Ok(response);
        }

        [NonAction]
        public ObjectResult ApiOK(string message)
        {
            var response = new ApiResponse<object?>().SuccessResponse(null, message);
            return Ok(response);
        }
        
        [NonAction]
        protected ObjectResult ApiDataInvalid(string? message = null)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse<object?>().Fail(message ?? ResponseConstant.FAILED_CODE, ResponseConstant.FAILED_CODE));
        }

        protected string GetBaseUrl()
        {
            var request = Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }

        protected virtual CurrentUser CurrentUser
        {
            get
            {
                var user = new CurrentUser
                {
                    Id = int.Parse(GetClaim("Id") ?? "1"),
                    Username = GetClaim("Username") ?? "",
                    Name = GetClaim("Name") ?? "",
                };

                return user;
            }
        }

        private string? GetClaim(string type)
        {
            var claim = _contextAccessor.HttpContext?.User.Claims.Where(c => c.Type == type).FirstOrDefault();
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }
    }
}
