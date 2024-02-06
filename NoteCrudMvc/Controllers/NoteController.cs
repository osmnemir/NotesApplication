using Microsoft.AspNetCore.Mvc;
using NoteCrudMvc.Models;

namespace NoteCrudMvc.Controllers
{
    public class NoteController : Controller
    {

        private readonly APIGateway aPIGateway;

        public NoteController(APIGateway aPIGateway)
        {
            this.aPIGateway = aPIGateway;
        }

        public IActionResult Index()
        {
            List<Note> notes;
            notes = aPIGateway.ListNotes();
            return View(notes);
        }





        [HttpGet]
        public IActionResult Create()
        {
            Note note = new Note();
            return View(note);
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {

            aPIGateway.CreateNote(note);


            return RedirectToAction("index");
        }


        public IActionResult Details(int Id)
        {
            Note note = new Note();
            note = aPIGateway.GetNote(Id);
            return View(note);
        }



        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Note note; 
            note = aPIGateway.GetNote(Id);
            return View(note);
        }

        [HttpPost]
        public IActionResult Edit(Note note)
        {

            aPIGateway.UpdateNote(note);
            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Note note;
            note = aPIGateway.GetNote(Id);


            return View(note);
        }

        [HttpPost]
        public IActionResult Delete(Note note)
        {
            aPIGateway.DleteNote(note.Id);
            return RedirectToAction("index");
        }
    }
}
