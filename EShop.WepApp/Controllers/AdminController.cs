using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.APIHelpers;
using EShop.WepApp.Fillters;
using EShop.WepApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
namespace EShop.WepApp.Controllers
{
    [ForbiddenForLoggedUserFillter]
    [ForbiddenForNotLoggedUserFillter]
    [AdminAddedBookFillter]
    public class AdminController : Controller
    {
        private IUnitOfWork uow;
        public EShopServices Services { get; }
        public AdminController(IUnitOfWork uow, EShopServices services)
        {
            this.uow = uow;
            Services = services;
        }
        
        public async Task<IActionResult> Index(string name)
        {
            MainClass model = await Services.GetBooksFromAPI(name);
            model.genres = uow.RepositoryGenre.GetAll();
            return View("AddBooks", model);
        }
        public ActionResult SelectedBooks()
        {
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> model = null;
            if (!(booksByte is null))
                model = JsonSerializer.Deserialize<List<Book>>(booksByte);
            else
            {
                model = new List<Book>();
            }
            return View("SelectedBooks", model);
        }

        public int PickedBooks(string image, string title, double price, int supplies, string authors, string genres, string description)
        {
            Book book = new Book
            {
                Image = image,
                Title = title,
                Price = price,
                Supplies = supplies,
                Autors = GetAuthors(authors),
                Genres = GetGenres(genres),
                Description = description
            };
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> books = null;
            if (!(booksByte is null))
                books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            else
            {
                books = new List<Book>();
            }

            if (books.Any(b => b.Title == book.Title && b.Description == book.Description))
                return books.Count;

            int? numberOfBooks = HttpContext.Session.GetInt32("numberOfSelectedBooks");
            if (numberOfBooks is null)
            {
                numberOfBooks = 1;
            }
            else
            {
                numberOfBooks++;
            }
            HttpContext.Session.SetInt32("numberOfSelectedBooks", (int)numberOfBooks);

            books.Add(book);
            HttpContext.Session.Set("book", JsonSerializer.SerializeToUtf8Bytes(books));
            return books.Count;
        }
        public void AddBooks()
        {
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> books = null;
            books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            

            books = new List<Book>();
            HttpContext.Session.Set("book", JsonSerializer.SerializeToUtf8Bytes(books));

            HttpContext.Session.Remove("numberOfSelectedBooks");
        }
        [HttpDelete]
        public int RemoveItemFromList(string title, string description)
        {
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            books.RemoveAll(b => b.Title == title && b.Description == description);
            HttpContext.Session.Set("book", JsonSerializer.SerializeToUtf8Bytes(books));//template method pattern
            int? items = HttpContext.Session.GetInt32("numberOfSelectedBooks");


            HttpContext.Session.SetInt32("numberOfSelectedBooks", (int)--items);
            return books.Count;
        }
        private List<Genre> GetGenres(string genres)
        {
            string[] genresArr = genres.Split("\n");
            List<Genre> genresList = new List<Genre>();
            foreach (string item in genresArr)
            {
                if (item != "")
                {
                    var genre = new Genre() { Name = item };
                    genresList.Add(genre);
                }

            }
            return genresList;
        }
        public List<Autor> GetAuthors(string authors)
        {
            string[] authorsArr = authors.Split("\n");
            List<Autor> authorsList = new List<Autor>();
            foreach (string item in authorsArr)
            {
                string author = item.Trim();
                if (author != "" && author != null)
                {
                    string[] name = author.Split(" ");
                    if (name.Length == 1)
                    {
                        authorsList.Add(new Autor { FirstName = name[0], LastName = "" });
                    }
                    if (name.Length == 2)
                        authorsList.Add(new Autor { FirstName = name[0], LastName = name[1] });
                    else if (name.Length > 2)
                    {
                        string lastname = "";
                        for (int i = 2; i < name.Length; i++)
                        {
                            lastname = lastname + name[i];
                        }
                        authorsList.Add(new Autor { FirstName = name[0] + " " + name[1], LastName = lastname });
                    }
                }
            }
            return authorsList;
        }

    }
}