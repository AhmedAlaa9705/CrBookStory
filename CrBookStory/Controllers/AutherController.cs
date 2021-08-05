using CrBookStory.Models;
using CrBookStory.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.Controllers
{
    public class AutherController : Controller
    {
        private IBookStoreRepository<Auther> repository;
        public AutherController(IBookStoreRepository<Auther> autherRepository)
        {
            repository = autherRepository;
        }
        // GET: AutherController
        public ActionResult Index()
        {
            var authers = repository.GetAll();
            return View(authers);
        }

        // GET: AutherController/Details/5
        public ActionResult Details(int id)
        {
            var auther = repository.Find(id);
            return View(auther);
        }

        // GET: AutherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auther auther)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                repository.Insert(auther);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Edit/5
        public ActionResult Edit(int id)
        {
            var auther = repository.Find(id);
            return View(auther);
        }

        // POST: AutherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Auther auther)
        {
            try
            {
           

                repository.Update(id,auther);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Delete/5
        public ActionResult Delete(int id)
        {
            var auther = repository.Find(id);
            return View(auther);
        }

        // POST: AutherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Auther auther)
        {
            try
            {
               
                repository.Delete(id);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
