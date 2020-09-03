using LoanValidator.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanValidator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="loanService"></param>
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        /// <summary>
        /// Verify loan by id
        /// https://localhost:44314/api/loan/verify/123456
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("verify/{id}")]
        public ActionResult<string> Verify(int id)
        {
            var result = _loanService.Verify(id);

            return Ok(result);
        }
    }
}
