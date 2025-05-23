﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;
using Domain.Models;
using Domain.Interfaces;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        // GET: PollController
        public IActionResult Index([FromServices] IPollsRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);

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
        public IActionResult Create(Poll poll, [FromServices] IPollsRepository pollRepository)
        {
            if (ModelState.IsValid)
            {
                pollRepository.CreatePoll(poll);
                TempData["message"] = "Poll was created successfully";

                return RedirectToAction("Index");

            }
            TempData["error"] = "Check your inputs";

                
            Poll myModel = new Poll();
            myModel = poll;

            return View(myModel);


        }

        [HttpGet]
        public IActionResult Vote(int id, [FromServices] IPollsRepository pollRepository)
        {
            var poll = pollRepository.GetPolls().SingleOrDefault(x => x.Id == id);

            if (poll == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var votes = pollRepository.GetVotes(id).Where(x=>x.UserId==userId);

                    if (!votes.Any())
                    {
                        return View(poll);
                    }
                    else
                    {
                        TempData["error"] = "You have already voted in this poll!";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["error"] = "You need to be connected to vote in a poll!";
                    return RedirectToAction("Index");
                }
            }
        }

        
        [HttpPost]
        public IActionResult Vote(Poll poll, string option, [FromServices] IPollsRepository pollRepository)
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            pollRepository.Vote(poll, userId);
            TempData["message"] = "Vote has been registered successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Results(int id, [FromServices] IPollsRepository pollRepository)
        {
            var poll = pollRepository.GetPolls().SingleOrDefault(x => x.Id == id);

            var totalVotes = poll.Option1VotesCount + poll.Option2VotesCount + poll.Option3VotesCount;

            return View(poll);
        }

              
    }
}
