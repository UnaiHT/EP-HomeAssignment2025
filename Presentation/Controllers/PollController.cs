using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IActionResult Create()
        {
            Poll myModel = new Poll();

            return View(myModel);
        }

        // POST: PollController/Create
        [HttpPost]
        public IActionResult Create(Poll poll, [FromServices] IWebHostEnvironment host)
        {
            if (_pollRepository.GetPoll(poll.Id) != null)
            {
                TempData["error"] = "Poll already exists";
                return RedirectToAction("Index");
            }
            else
            {
                _pollRepository.CreatePoll(poll);
                TempData["message"] = "Poll was created successfully";

                return RedirectToAction("Index");

                /*
                Poll myModel = new Poll();
                myModel = poll; //Passing the same instance back to the page so that i show the end user the same data they gave me

                return View(myModel);*/
            }

        }

        [HttpGet]
        public IActionResult Vote(int id)
        {
            var poll = _pollRepository.GetPoll(id);

            return View(poll);
        }

        
        [HttpPost]
        public IActionResult Vote(Poll poll)
        {
            _pollRepository.UpdatePoll(poll);
            TempData["message"] = "Vote have been registered successfully";

            return RedirectToAction("Index");
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
