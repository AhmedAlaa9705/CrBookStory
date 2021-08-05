using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrBookStory.Models;
using CrBookStory.Models.Repository;
using CrBookStory.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CrBookStory.Controllers
{
    public class BookController : Controller
    {
        private IBookStoreRepository<Book> repository;

        private IBookStoreRepository<Auther> AutherRepository;

        private IHostingEnvironment Hosting;

        public BookController(IBookStoreRepository<Book> bookRepository, IBookStoreRepository<Auther> autherRepository,IHostingEnvironment hosting)
        {
            repository = bookRepository;
            AutherRepository = autherRepository;
            Hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = repository.GetAllWitheInclude("Auther");
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = repository.FindWithInclude(id, "Auther");
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {

            BookAutherViewModels viewModel = new BookAutherViewModels
            {
                Authers = DropDown()
            };
            return View(viewModel);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAutherViewModels viewModel)
        {
            try
            {
                string filename = UploudFile(viewModel.File) ?? string.Empty;

              
                if (!ModelState.IsValid)
                {
                    
                    BookAutherViewModels viewModels = new BookAutherViewModels
                    {
                        Authers = DropDown()
                    };
                    ModelState.AddModelError("","you have to fill all requirs");
                    return View(viewModels);
                }
                if (viewModel.AutherId == -1)
                {
                    ViewBag.msg = "Please Select an Auther";
                    BookAutherViewModels viewModels = new BookAutherViewModels
                    {
                        Authers = DropDown()
                    };
                    return View(viewModels);
                }
                var auther = AutherRepository.Find(viewModel.AutherId);
                Book book = new Book
                {
                    Id = viewModel.BookId,
                    Title=viewModel.Title,
                    Description=viewModel.Description,
                    Auther=auther,
                    ImgUrl = filename
                };
                repository.Insert(book);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book= repository.Find(id);
            BookAutherViewModels viewModels = new BookAutherViewModels
            {
                BookId=book.Id,
                Title=book.Title,
                Description=book.Description,
                Authers=AutherRepository.GetAll().ToList(),
                AutherId=book.AutherId,
                ImgUrl=book.ImgUrl
                
            };
            return View(viewModels);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAutherViewModels viewModel)
        {
            try
            {
                string filename = UploudFile(viewModel.File, viewModel.ImgUrl);
             
                var auther = AutherRepository.Find(viewModel.AutherId);
                Book book = new Book
                {
                  
                    Id=viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Auther = auther,
                    ImgUrl=filename
                   

                };
                repository.Update(viewModel.BookId, book);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult Search(string term)
        {
            var result= repository.Search(term);
            return View("Index",result);
        }

        // GET: BookController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = repository.FindWithInclude(id, "Auther");
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
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
        List<Auther> DropDown()
        {
            var authers = AutherRepository.GetAll().ToList();
            authers.Insert(0, new Auther { Id = -1, Name = "Please Select an Auther" });
            return authers;
        }
         public string UploudFile(IFormFile file)
        {
            if (file != null)
            {
                string uploud = Path.Combine(Hosting.WebRootPath, "Uplouds");
               
                string fullpath = Path.Combine(uploud, file.FileName);
                file.CopyTo(new FileStream(fullpath, FileMode.Create));
                return file.FileName;


            }
            return null;
        }
        public string UploudFile(IFormFile file,string ImgUrl)
        {
            if (file != null)
            {
                string uploud = Path.Combine(Hosting.WebRootPath, "Uplouds");
               
                string newpath = Path.Combine(uploud, file.FileName);
                
                string fulloldpath = Path.Combine(uploud, ImgUrl);
                if (fulloldpath != newpath)
                {
                    System.IO.File.Delete(fulloldpath);
                    file.CopyTo(new FileStream(newpath, FileMode.Create));

                }
                return file.FileName;

            }
            return ImgUrl;
        }
    }
}
