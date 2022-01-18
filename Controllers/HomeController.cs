using ConferencesManagement.Data;
using ConferencesManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConferencesManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            Task.Run(() => CreateNewRoleWithUSer()).Wait();
        }

        private async Task CreateNewRoleWithUSer()
        {
            await CreateNewRoleWithUSerProccess(SystemRoles.All());
        }

        private async Task CreateNewRoleWithUSerProccess(List<string> RoleNames)
        {
            foreach (var RoleName in RoleNames)
            {
                if (await _roleManager.RoleExistsAsync(RoleName))
                    return;

                var role = new IdentityRole { Name = RoleName };
                var IdentityResult = await _roleManager.CreateAsync(role);

                if (IdentityResult.Succeeded)
                {
                    var user = new IdentityUser { UserName = RoleName.ToLower() + "@email.com", Email = RoleName.ToLower() + "@email.com" };
                    string userPWD = "123qwe!@#QWE";

                    var chkUser = await _userManager.CreateAsync(user, userPWD);

                    if (chkUser.Succeeded) await _userManager.AddToRoleAsync(user, RoleName);

                }
            }
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
