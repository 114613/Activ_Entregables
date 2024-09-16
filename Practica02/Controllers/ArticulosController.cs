using Microsoft.AspNetCore.Mvc;
using Practica02.Models;
using Practica02.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Practica02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IAplicacion _servicio;

        public ArticulosController(IAplicacion servicio)
        {
            _servicio = servicio;
        }


        // GET: api/<ArticulosController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_servicio.ListarArticulos());
        }

        // GET api/<ArticulosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var articulo = _servicio.ConsultarArticulo(id);
            if (articulo == null){
                return NotFound("No se encuentra el artículo con el Id especificado.");
            }
            return Ok(articulo);
        }

        // POST api/<ArticulosController>
        [HttpPost]
        public IActionResult Post([FromBody] Articulo articulo)
        {
            _servicio.AgregarArticulo(articulo);
            return Ok("El artículo ha sido agregado exitosamente.");
        }

        // PUT api/<ArticulosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Articulo articulo)
        {
            articulo.Id = id;
            _servicio.EditarArticulo(articulo);
            return Ok("El artículo ha sido editado exitosamente.");
        }
        
        

        // DELETE api/<ArticulosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _servicio.EliminarArticulo(id);
            return Ok("El artículo ha sido eliminado exitosamente.");
        }
    }
}
