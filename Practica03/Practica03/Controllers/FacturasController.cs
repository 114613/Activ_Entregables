using Microsoft.AspNetCore.Mvc;
using Practica03Library.Data;
using Practica03Library.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Practica03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IAplicacion _aplicacion;

        public FacturasController(IAplicacion aplicacion)
        {
            _aplicacion = aplicacion;
        }

        // GET: api/<FacturasController>
        [HttpGet("consultar")]
        public ActionResult<List<Factura>> Get([FromQuery] DateTime? fecha, [FromQuery] string formaPago)
        {
            try
            {
                var facturas = _aplicacion.ConsultarFactura(fecha, formaPago);
                return Ok(facturas);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // GET api/<FacturasController>/5
        [HttpGet("{id}")]
        public ActionResult<Factura> Get(int id)
        {
            try
            {
                var factura = _aplicacion.ConsultarFactura(id);
                if(factura == null)
                {
                    return NotFound();
                }
                return Ok(factura);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // POST api/<FacturasController>
        [HttpPost]
        public ActionResult Post([FromBody] Factura value)
        {
            try
            {
                if(value == null)
                {
                    return BadRequest("La factura no puede ser nula.");
                }
                _aplicacion.RegistrarFactura(value);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // PUT api/<FacturasController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Factura value)
        {
            try
            {
                _aplicacion.EditarFactura(value);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
