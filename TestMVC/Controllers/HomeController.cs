using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestBL.DTOs.CardItemDTOs;
using TestBL.Services.Abstractions;
using TestCORE.Models;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        readonly ICardItemService _service;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(ICardItemService service, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<GetCardItemDTO> cardItems = await _service.GetAllCardItemsAsync();
                return View(cardItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //Manually adding admin user and admin role to database

        //public async Task<IActionResult> SeedAdmin()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    return Ok("success");
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    var newAdmin = new AppUser
        //    {
        //        FirstName = "Test",
        //        LastName =  "Testov",
        //        UserName = "admin",
        //        Email = "admin@example.com"
        //    };

        //    var result = await _userManager.CreateAsync(newAdmin, "AdminPassword123!");

        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(":(");
        //    }
        //        await _userManager.AddToRoleAsync(newAdmin, "Admin");
        //        return Ok("success");
        //}
    }
}
