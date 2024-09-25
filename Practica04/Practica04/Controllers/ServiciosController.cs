using ClassLibrary.Models;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Practica04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioService _service;

        public ServiciosController(IServicioService service)
        {
            _service = service;
        }

        // GET: api/<ServiciosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var servicios = _service.GetAll();
                return Ok(servicios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        // GET api/<ServiciosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var servicio = _service.GetById(id);
                if(servicio == null)
                {
                    return NotFound();
                }
                return Ok(servicio);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        // POST api/<ServiciosController>
        [HttpPost]
        public IActionResult Post([FromBody] Servicio value)
        {
            try
            {
                if (IsValid(value))
                {
                    _service.Save(value);
                    return Ok("Servicio creado.");
                }
                else
                {
                    return BadRequest("Los datos son incorrectos o incompletos.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        private bool IsValid(Servicio value)
        {
            return string.IsNullOrEmpty(value.Nombre) && value.Costo == 0;
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Servicio value)
        {
            try
            {
                if (IsValid(value))
                {
                    var servicio = _service.GetById(id);
                    _service.Save(servicio);
                    return Ok();
                }
                else
                {
                    return BadRequest("Los datos son incorrectos o incompletos.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        // DELETE api/<ServiciosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("El servicio ha sido eliminado exitosamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }
    }
}
