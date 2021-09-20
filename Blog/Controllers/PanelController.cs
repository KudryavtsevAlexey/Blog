using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFileManager _fileManager;

        public PanelController(IRepository repository,
                                IFileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
        }


        public IActionResult Index()
        {
            var posts = _repository.GetAllPosts();
            return View(posts);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var post = _repository.GetPost((int)id);
            var pvm = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
            };
            return View(pvm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel pvm)
        {
            var post = new Post
            {
                Id = pvm.Id,
                Title = pvm.Title,
                Body = pvm.Body,
                Image=await _fileManager.SaveImage(pvm.Image),
            };
            if (post.Id > 0)
            {
                _repository.UpdatePost(post);
            }
            else
            {
                _repository.AddPost(post);
            }
            if (await _repository.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repository.RemovePost(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
