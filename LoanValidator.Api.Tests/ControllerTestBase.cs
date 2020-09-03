using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanValidator.Api.Tests
{
    public class ControllerTestBase<T> where T : ControllerBase
    {
        public T Controller { get; set; }

        public void AddActorToContext(ClaimsPrincipal actor)
        {
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = actor
                }
            };
        }

    }
}
