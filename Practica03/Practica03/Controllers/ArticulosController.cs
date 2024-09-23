using Microsoft.AspNetCore.Mvc;
using Practica03Library.Data;
using Practica03Library.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Practica03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IAplicacion _aplicacion;

        public ArticulosController(IAplicacion aplicacion)
        {
            _aplicacion = aplicacion;
        }

        // GET: api/<ArticulosController>
        [HttpGet]
        public ActionResult<List<Articulo>> Get()
        {
            try
            {
                var articulos = _aplicacion.Consultar();
                return Ok(articulos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // GET api/<ArticulosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var articulo = _aplicacion.ConsultarArticulo(id);
            try
            {
                if (articulo == null)
                {
                    return NotFound();
                }
                return Ok(articulo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Articulo no encontrado.");
            }
        }

        // POST api/<ArticulosController>
        [HttpPost]
        public IActionResult Post([FromBody] Articulo value)
        {
            try
            {
                if(value == null)
                {
                    return BadRequest("El artículo no puede ser nulo.");
                }

                _aplicacion.AgregarArticulo(value);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, el articulo no ha sido agregado.");
            }
        }

        // PUT api/<ArticulosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Articulo value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("El articulo no puede ser nulo.");
                }
                _aplicacion.EditarArticulo(value);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor.");
            }

        }

        // DELETE api/<ArticulosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _aplicacion.BajaArticulo(id);
                return Ok("Articulo eliminado exitosamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "El articulo no ha podido ser eliminado.");
            }
        }
    }
}
