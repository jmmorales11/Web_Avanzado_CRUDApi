using ESTUDIANTECRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ESTUDIANTECRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        // GET: api/<EstudiantesController>
        [HttpGet]
        public List<Estudiante> Get()
        {
            using (var dbContext = new DBINSTITUCIONContext())
            {
                List<Estudiante> lista = dbContext.Estudiantes.Include(c => c.oMateria).ToList();
                // Retornar la lista en formato JSON
                return lista;
            }
        }

       
    }
}
