using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        [HttpGet("/signin-oidc")]
        public async Task<IActionResult> SignInOidc(string code)
        {
            // The middleware automatically processes the tokens from Cognito
            // and stores them in the cookie. You can now access the user's info.
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");

            return RedirectToAction("Index", "Home"); // Redirect to the main page after sign-in
        }

        [HttpGet("/signout")]
        public async Task<RedirectResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            var logoutUri = "https://us-east-1jk5bcolui.auth.us-east-1.amazoncognito.com/logout?client_id=54lj2rvipu25ndtav6bs8her47&logout_uri=" +
                   Uri.EscapeDataString("https://localhost:5001/AfterLogoutUrl");

            return Redirect(logoutUri);
        }
        [AllowAnonymous]
        [HttpGet("/AfterLogoutUrl")]
        public async Task<ActionResult> AfterLogoutUrl()
        {
            return Ok("Logout successful");
        }

        // PUT api/<UserLoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserLoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
