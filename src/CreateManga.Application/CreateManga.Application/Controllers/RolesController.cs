namespace CreateManga.Application.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Constants.RolesConstants;

    [Authorize(Roles = RolesConstants.ADMIN_ROLE_NAME)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Create()
        {
            

            return this.Redirect("/");
        }
    }
}
