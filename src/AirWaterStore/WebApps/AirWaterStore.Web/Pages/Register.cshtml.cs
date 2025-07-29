using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace AirWaterStore.Web.Pages;

public class RegisterModel(
IAirWaterStoreService airWaterStoreService,
ILogger<LoginModel> logger
) : PageModel
{

    [BindProperty]
    public RegisterInputModel RegisterInput { get; set; } = default!;

    public string ErrorMessage { get; set; } = string.Empty;

    public class RegisterInputModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {

            var result = await airWaterStoreService.PostRegister(new RegisterDto(
                RegisterInput.Username,
                RegisterInput.Email,
                RegisterInput.Password
                ));


            return RedirectToPage("/Login");
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Login failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);

            // Attempt to read the response content
            if (ex.HasContent)
            {
                // Use System.Text.Json to parse the content
                using var doc = JsonDocument.Parse(ex.Content!);
                if (doc.RootElement.TryGetProperty("detail", out var detailProp))
                {
                    var detailMessage = detailProp.GetString();
                    // Show to user or handle it
                    ErrorMessage = detailMessage!;
                }
                else
                {
                    // fallback: show entire content
                    ErrorMessage = ex.Content!;
                }
            }
            else
            {
                // fallback: no content
                ErrorMessage = "An unexpected error occurred."!;
            }

            return Page();
        }
    }
}