using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MossadAPI.Services;
using MossadAPI.Services.Implementation;

namespace MossadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly LoginService _loginService;
        public LoginsController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult> GetToken([FromBody]string id)
        {
            try
            {
                string token = await _loginService.Login(id);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
