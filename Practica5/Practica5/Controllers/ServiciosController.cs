using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Practica05Library.Models;
using Practica05Library.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Practica5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly ITServicioRepository _repository;

        public ServiciosController(ITServicioRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ServiciosController>
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

        [HttpGet("/Promotion")]
        public IActionResult GetPromotion()
        {
            try
            {
                return Ok(_repository.GetPromotion());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        // GET api/<ServiciosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id != 0)
                {
                    return Ok(_repository.GetById(id));
                }
                else
                {
                    return BadRequest("El id ingresado es incorrecto.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        // POST api/<ServiciosController>
        [HttpPost]
        public IActionResult Post([FromBody] TServicio servicio)
        {
            try
            {
                if (IsValid(servicio))
                {
                    _repository.Create(servicio);
                    return Ok("El servicio ha sido creado exitosamente.");
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

        private bool IsValid(TServicio servicio)
        {
            return !string.IsNullOrEmpty(servicio.Nombre) && servicio.Costo != 0 && !string.IsNullOrEmpty(servicio.EnPromocion);
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TServicio servicio)
        {
            try
            {
                if(id != 0)
                {
                    if (_repository.Update(id, servicio))
                    {
                        return Ok("El servicio ha sido actualizado correctamente.");
                    }
                    else
                    {
                        return BadRequest("No han podido actualizarse los datos del servicio.");
                    }
                }
                else
                {
                    return BadRequest("Los datos del servicio son incorrectos o incompletos.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }

        // DELETE api/<ServiciosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if(id != 0)
                {
                    _repository.Delete(id);
                    return Ok("El servicio ha sido eliminado.");
                }
                else
                {
                    return BadRequest("El id ingresado no se encuentra registrado.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno del servidor.");
            }
        }
    }
}
