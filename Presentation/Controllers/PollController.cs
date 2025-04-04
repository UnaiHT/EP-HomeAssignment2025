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
            var polls = _pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);

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

            if (poll == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(poll);
            }
        }

        
        [HttpPost]
        public IActionResult Vote(Poll poll, string option)
        {
            switch (option)
            {
                case "Option1":
                    poll.Option1VotesCount += 1;
                    break;
                case "Option2":
                    poll.Option2VotesCount += 1;
                    break;
                case "Option3":
                    poll.Option3VotesCount += 1;
                    break;
                default:
                    return BadRequest("Invalid option selected.");
            }

            _pollRepository.Vote(poll);
            TempData["message"] = "Vote has been registered successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Results(int id)
        {
            var poll = _pollRepository.GetPoll(id);

            var totalVotes = poll.Option1VotesCount + poll.Option2VotesCount + poll.Option3VotesCount;

            return View(poll);
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
