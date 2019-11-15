using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticaF.Models;

namespace PracticaF.Controllers
{
    public class EstudiantesController : Controller
    {
        private PracticaFEntities db = new PracticaFEntities();

        // GET: Estudiantes
        public ActionResult Index()
        {
            return View(db.Estudiante.ToList());
        }

        // GET: Estudiantes/Details/5
        public ActionResult Details(int? id)
        {
            var lista = from data in db.CursosEstudiantes
                        where data.IdEstudiante == id
                        select data;
            return View(lista);
        }

        // GET: Estudiantes/Create
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
