using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.UI.Areas.Admin.Models.UserHdVMs;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    public class UserHdController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserHdVM
            {
                FullName = "Hayrettin DEMİR",
                Role = "Web Designer",
                ProfileUrl = "users-profile.html",
                SettingsUrl = "users-profile.html",
                HelpUrl = "pages-faq.html",
                SignOutUrl = "#",
                ProfileImageUrl = "/images/profile.jpg", // Profil fotoğrafı yolu
                Biography = "Hayrettin DEMİR, deneyimli bir web tasarımcısıdır. Yıllardır sektörde çeşitli projelerde yer almış ve kullanıcı odaklı tasarımlar yapmıştır."
            };

            return View(model);
        }
    }
}
