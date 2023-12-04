using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESTUDIANTECRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ESTUDIANTECRUD.Models.ViewModels;

namespace ESTUDIANTECRUD.Controllers
{

    public class HomeController : Controller
    {
        private readonly DBINSTITUCIONContext _DBContext;

        public HomeController(DBINSTITUCIONContext context)
        {
            _DBContext = context;
        }

        [HttpGet]
        public IActionResult Index(string idEstudiante, string contra)
        {
            Estudiante oEstudiante = _DBContext.Estudiantes.Include(c => c.oMateria).Where(e => e.CedulaEstudiante== idEstudiante && e.Contrasena==contra).FirstOrDefault();
            return View(oEstudiante);
        }
        [HttpPost]
        //Valida las credenciales 
        public IActionResult Index(Estudiante est)
        {
            if (ModelState.IsValid)
            {
                var user = _DBContext.Estudiantes.SingleOrDefault(u=> u.CedulaEstudiante==est.CedulaEstudiante && u.Contrasena==est.Contrasena);
                if (user != null)
                {
                    ViewBag.ErrorMessage = "Ingreso valido";
                    return RedirectToAction("Inicio", "Home");
                }

            }
            ViewBag.ErrorMessage = "Ingreso invalido";
            return View("Index");
        }
        [HttpGet]
        public IActionResult Inicio()
        {
            List<Estudiante> lista = _DBContext.Estudiantes.Include(c=> c.oMateria).ToList();
            return View(lista);  
        }

        //Enlistar en un archivp json
        public static JsonResult lista1()
        {
            using (var dbContext = new DBINSTITUCIONContext())
            {
                List<Estudiante> lista = dbContext.Estudiantes.Include(c => c.oMateria).ToList();
                // Retornar la lista en formato JSON
                return new JsonResult(lista);
            }
            
        }



        [HttpGet]
        //Ver los detalles de un alumno con su id
        public IActionResult Estudiante_Detalle(int idEstudiante)
        {
            EstudianteVM oEstudianteVM = new EstudianteVM()
            {
                oEstudiante = new Estudiante(),
                oListaMateria = _DBContext.Materia.Select(cargo => new SelectListItem()
                {
                    Text = cargo.NombreMateria,
                    Value = cargo.IdMateria.ToString()
                }).ToList()
            };

            if (idEstudiante != 0)
            {
                oEstudianteVM.oEstudiante = _DBContext.Estudiantes.Find(idEstudiante);
            }


            return View(oEstudianteVM);
        }

        //Insertar el estudiante 
        [HttpPost]
        public IActionResult Estudiante_Detalle(EstudianteVM oEstudianteVM)
        {
            if (oEstudianteVM.oEstudiante.IdEstudiante == 0)
            {
                _DBContext.Estudiantes.Add(oEstudianteVM.oEstudiante);
            }
            else
            {
                _DBContext.Estudiantes.Update(oEstudianteVM.oEstudiante);
            }
            _DBContext.SaveChanges();

            return RedirectToAction("Inicio","Home");
        }

        [HttpGet]
        //Ve que estudiante quiere eliminar con su id
        public IActionResult Eliminar(int idEstudiante)
        {
            Estudiante oEstudiante = _DBContext.Estudiantes.Include(c => c.oMateria).Where(e => e.IdEstudiante == idEstudiante).FirstOrDefault();


            return View(oEstudiante);
        }
        
        [HttpPost]
        //Elimina el estudiante el objeto
        public IActionResult Eliminar(Estudiante oEstudiante)
        {
            _DBContext.Estudiantes.Remove(oEstudiante);
            _DBContext.SaveChanges();
            return RedirectToAction("Inicio", "Home");
        }

       


    }
}