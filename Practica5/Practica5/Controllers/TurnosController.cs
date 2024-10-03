using Microsoft.AspNetCore.Mvc;
using Practica05Library.Models;
using Practica05Library.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Practica5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly ITTurnoRepository _repository;

        public TurnosController(ITTurnoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<TurnosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        // GET api/<TurnosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if(id != 0)
                {
                    return Ok(_repository.GetById(id));
                }
                else
                {
                    return BadRequest("El id no puede ser cero.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        [HttpGet("/Entre")]
        public IActionResult Get([FromQuery] DateTime fec1, [FromQuery] DateTime fec2)
        {
            try
            {
                if (fec1 <= fec2)
                {
                    return Ok(_repository.GetByDate(fec1, fec2));
                }
                else
                {
                    return BadRequest("Las fechas ingresadas son erróneas o incompletas.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        // POST api/<TurnosController>
        [HttpPost]
        public IActionResult Post([FromBody] TTurno turno)
        {
            try
            {
                if (IsValid(turno))
                {
                    return Ok(_repository.Create(turno));
                }
                else
                {
                    return BadRequest("Los datos son incorrectos o incompletos.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        private bool IsValid(TTurno turno)
        {
            return turno.Fecha != null && !string.IsNullOrEmpty(turno.Cliente);
        }

        // PUT api/<TurnosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            try
            {
                if (_repository.Update(id))
                {
                    return Ok("El turno ha sido actualizado correctamente.");
                }
                else
                {
                    return NotFound($"El turno con id {id} no existe.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        // DELETE api/<TurnosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_repository.Delete(id))
                {
                    return Ok("El turno ha sido cancelado exitosamente.");
                }
                else
                {
                    return NotFound($"El turno con id {id} no existe.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }
    }
}
