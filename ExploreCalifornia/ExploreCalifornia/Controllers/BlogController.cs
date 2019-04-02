using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.Controllers
{
    public class BlogController : Controller
    {
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
    }
}