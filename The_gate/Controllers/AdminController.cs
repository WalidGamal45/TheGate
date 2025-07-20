﻿using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin admin2)
        {
            var admin = _admin.GetAdmins()
                              .FirstOrDefault(x => x.Username == admin2.Username && x.Password == admin2.Password);

            if (admin != null)
            {

                return RedirectToAction("HomePage");
            }

            ViewBag.Error = "the username or password is error";
            return View();
        }
        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult GetallAdmins()
        {
            var admin = _admin.GetAdmins();
            return PartialView("_IndexToAdmins", admin);
        }
        public IActionResult Details(int id)
        {
            var admin = _admin.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddAdminDto adminDto)
        {
            _admin.Add(adminDto);

            return RedirectToAction("HomePage", "Admin");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var admin = _admin.GetById(id);
            return View(admin);
        }
        [HttpPost]
        public ActionResult Edit(UpdateAdminDto newadminDto)

        {
            _admin.Update(newadminDto);

            return RedirectToAction("HomePage", "Admin");
        }
        public ActionResult Delete(int id)
        {
             _admin.Delete(id);
            return RedirectToAction("GetallAdmins");

        }

    }
}
