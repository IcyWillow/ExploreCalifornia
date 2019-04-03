using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {

            var posts = new[]
            {
                new Post()
                {
                    Title = "My blog post",
                    Posted = DateTime.Now,
                    Author = "Serena Fleischmann",
                    Body = "This is a great blog post, don't you think?"
                },
                new Post()
                {
                    Title = "My second blog post",
                    Posted = DateTime.Now,
                    Author = "Serena Fleischmann",
                    Body = "This is ANOTHER great blog psot, don't you think?"
                },
            };

            return View(posts);
        }

        [Route("blog/{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {

            var post = new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Serena Fleischmann",
                Body = "This is a great blog psot, don't you think?"

            };

            return View(post);
        }

        [HttpGet, Route("create")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost, Route("create")]
        public IActionResult Create(Post post)
        {
            //Check if given data is valid.
            if (ModelState.IsValid)
                return View();

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            return View();
        }

    

    }
}