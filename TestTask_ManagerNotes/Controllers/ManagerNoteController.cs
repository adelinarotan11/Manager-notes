using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TestTask_ManagerNotes.Models;

namespace TestTask_ManagerNotes.Controllers
{
    public class ManagerNoteController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IStringLocalizer<ManagerNoteController> _localizer;
        public ManagerNoteController(ManagerContext context, IStringLocalizer<ManagerNoteController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }


        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.managerNotes.ToListAsync());
        //}



        // GET: ManagerNote/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new ManagerNote());
            else
                return View(_context.managerNotes.Find(id));
        }
        public PartialViewResult ListOfNotes(string titleOfNote)
        {
            ViewBag.SelectedNote = titleOfNote;

            IEnumerable<string> TitleNotes = _context.managerNotes
                .Select(note => note.Title)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(TitleNotes);

        }

        public ViewResult Index(string titleOfNote)
        {
            if (_context.managerNotes.Count() > 0)
            {
                ManagerNoteAll model = new ManagerNoteAll
                {
                    ManagerNotes = _context.managerNotes
                        .Where(p => p.Title == titleOfNote)
                        .Distinct()
                        .OrderBy(i => i.ManagerNotesId),
                    CurrentTitle = titleOfNote
                };


                return View(model);
            }
            else
            {
                ViewBag.message = _localizer["No notes found! Create its now!"];
                return View();
            }
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("ManagerNotesId,Title,TextOfNote")] ManagerNote managerNote)
        {
            if (ModelState.IsValid)
            {
                if (managerNote.ManagerNotesId == 0)
                {
                    _context.Add(managerNote);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _context.Update(managerNote);
                    await _context.SaveChangesAsync();
                    return RedirectToRoute(new
                    {
                        controller = "ManagerNote",
                        action = "Index",
                        titleOfNote = managerNote.Title
                    });
                }
            }
            return View(managerNote);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var manager = await _context.managerNotes.FindAsync(id);
            _context.managerNotes.Remove(manager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult VerifyTitle(string Title)
        {
            if (Title.Length <= 200 && Title.Length > 3)
                return Json(data: true);
            else
                return Json(data: false);
        }


        public JsonResult VerifyTextOfNote(string TextOfNote)
        {
            if (TextOfNote.Length <= 2000 && TextOfNote.Length > 3)
                return Json(data: true);
            else
                return Json(data: false);
        }
    }
}
