using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WeatherNote.Models;
using WeatherNote.Services;

namespace WeatherNote.Controllers
{
    public class NoteController : Controller
    {

        private ApplicationDbContext _context;

        public NoteController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        // GET: Note
        public ViewResult Index(string sortOrder, string searchString)
        {
            // Sort function using viewbag
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "message_desc" : "";
            ViewBag.DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            var notes = from n in _context.Notes
                           select n;
            if (!String.IsNullOrEmpty(searchString))
            {
                notes = notes.Where(n => n.Message.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "message_desc":
                    notes = notes.OrderByDescending(n => n.Message);
                    break;
                case "Date":
                    notes = notes.OrderBy(n => n.Date);
                    break;
                case "date_desc":
                    notes = notes.OrderByDescending(n => n.Date);
                    break;
                default:
                    notes = notes.OrderBy(n => n.Message);
                    break;
            }

            var weatherService = new OpenWeatherMapService();
            var notelist = notes.ToList();

            foreach (var note in notelist)
            {
                weatherService.FindMaxTemp(note);
            }

            return View(notes.ToList());
        }


        // GET: Note/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            var note = new Note();

            return View("NoteForm", note);
        }

        // POST: Note/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Save(Note note)
        {
            if (note.Id == 0)
                _context.Notes.Add(note);
            
            else
            {
                var noteInDb = _context.Notes.Single(n => n.Id == note.Id);
                noteInDb.Date = note.Date;
                noteInDb.Message = note.Message;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Note");
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int id)
        {
            var note = _context.Notes.SingleOrDefault(n => n.Id == id);

            if (note == null)
                return HttpNotFound();

            return View("NoteForm");
        }

        // POST: Note/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int id)
        {
            var note = _context.Notes.SingleOrDefault(n => n.Id == id);

            _context.Notes.Remove(note);

            _context.SaveChanges();

            return RedirectToAction("Index", "Note");
        }

        // POST: Note/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
