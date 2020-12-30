using CRM.Models;
using CRM.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService auth;
        public AuthenticationController(IAuthenticateService _auth) {
            auth = _auth;
        }
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody]AuthenticateRequest user) {
            var response = auth.Authenticate(user);
            if (response != null)
                return Ok(response);
            else
                return NotFound("El usuario o contraseña son incorrectos");
        }
    }
}
