﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;
using Domain.Models;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        PollRepository _pollRepository;

        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }



        // GET: PollController
        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls();

            return View(polls);
        }

        // GET: PollController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PollController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PollController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PollController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PollController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PollController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PollController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
