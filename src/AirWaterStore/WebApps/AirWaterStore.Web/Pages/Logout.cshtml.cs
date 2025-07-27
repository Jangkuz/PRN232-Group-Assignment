namespace AirWaterStore.Web.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Response.Cookies.Delete(AppConst.Cookie);
            return RedirectToPage("/Login");
        }
    }
}
