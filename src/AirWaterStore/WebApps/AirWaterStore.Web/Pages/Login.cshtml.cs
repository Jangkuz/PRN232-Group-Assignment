using AirWaterStore.Web.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace AirWaterStore.Web.Pages;

public class LoginModel(
    IAirWaterStoreService airWaterStoreService,
    ILogger<LoginModel> logger
    ) : PageModel
{

    [BindProperty]
    public LoginInputModel LoginInput { get; set; } = default!;

    public string ErrorMessage { get; set; } = string.Empty;

    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public void OnGet()
    {
        // Clear session on login page
        HttpContext.Session.Clear();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {

            var result = await airWaterStoreService.PostLogin(new LoginDto(
                LoginInput.Email,
                LoginInput.Password
                ));


            //Store token in HttpOnly cookie
            HttpContext.Response.Cookies.Append(AppConst.Cookie, result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            var handler = new JwtSecurityTokenHandler();
            var token = result.Token;
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type.Equals(AppConst.UserIdClaim))?.Value, out int id) ? id : 0;
            var userIsBanClaim = bool.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type.Equals(AppConst.IsBanClaim))?.Value, out bool isBan) ? isBan : false;

            var userRoleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals(AppConst.RoleClaim))?.Value ?? "";
            if (userIdClaim != 0)
            {
                if (userIsBanClaim)
                {
                    ErrorMessage = "Your account has been banned.";
                    return Page();
                }

                // Redirect based on role
                if (userRoleClaim.Equals(AppConst.Staff)) // Staff
                {
                    return RedirectToPage(AppRouting.AdminDashboard);
                }
                else
                {
                    return RedirectToPage(AppRouting.Home);
                }
            }

            return Page();
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Login failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);

            if (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                // Optionally parse JSON error from content
                //TODO: show model error
                ModelState.AddModelError(string.Empty, "Invalid login credentials.");
                ErrorMessage = "Invalid login credentials.";
            }
            else if (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                ModelState.AddModelError(string.Empty, "Unauthorized.");
                ErrorMessage = "Unauthorized.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Try again later.");
                ErrorMessage = "Server error. Try again later.";
            }

            return Page();
        }

    }
}

