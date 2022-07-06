using Ejercicio_2.Data;
using Ejercicio_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Ejercicio_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public ClienteController(DataDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var cliente = _dbContext.cliente.Where(c => c.Estado == true).ToList();
            var response = new
            {
                Status = 200,
                Message = "Listado Cliente",
                Data = cliente

            };
            return Ok(response);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _dbContext.cliente.Where(c => c.ClienteId == id && c.Estado == true).FirstOrDefault();

            if (cliente==null)
            {
                return NotFound("Id Cliente No Encontrado");
            }
            var response = new
            {
                Status = 200,
                Message = "Cliente",
                Data = cliente

            };
            return Ok(response);

        }

        [HttpGet("GetByName/{Nombre}")]
        public IActionResult GetByName(string Nombre)
        {
            var cliente = _dbContext.cliente.Where(c => c.Nombre == Nombre && c.Estado == true).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound("Nombre  Cliente No Encontrado");
            }
            var response = new
            {
                Status = 200,
                Message = "Cliente",
                Data = cliente

            };
            return Ok(response);

        }
        [HttpPost("ClienteStore")]
        public IActionResult Store([FromForm] Cliente request)
        {
            var cliente = _dbContext.cliente.Where(c => c.Nombre == request.Nombre && c.Apellido == request.Apellido).FirstOrDefault();
            if (cliente !=null)
            {
                return NotFound("Cliente ya existe");

            }
            _dbContext.cliente.Add(request);

            _dbContext.SaveChanges();

                var response = new
                {
                    Status = 200,
                    Message = "Cliente",
                    Data = request

                };
                return Ok(response);




        }

        [HttpPut("ClienteUpdate/{id}")]
        public IActionResult Store([FromForm] Cliente request , int id)
        {
            var clienteid = _dbContext.cliente.Where(c => c.ClienteId == id && c.Estado == true).FirstOrDefault();
            if(clienteid == null)
            {
                return NotFound("Cliente No Encontrado");
            }
            var cliente = _dbContext.cliente.Where(c => c.Nombre == request.Nombre && c.Apellido == request.Apellido).FirstOrDefault();
            if (cliente != null)
            {
                return NotFound("Cliente ya existe");

             }
                clienteid.Nombre = request.Nombre;
                clienteid.Apellido = request.Apellido;
                clienteid.Domicilio = request.Domicilio;
                clienteid.Telefono = request.Telefono;
                
                _dbContext.cliente.Update(clienteid);

                    _dbContext.SaveChanges();

                    var response = new
                    {
                        Status = 200,
                        Message = "Cliente",
                        Data = clienteid

                    };
                    return Ok(response);




        }
        [HttpPut("ClienteDelete/{id}")]
        public IActionResult Delete([FromForm] Cliente request , int id)
        {
            var cliente = _dbContext.cliente.Where(c=> c.ClienteId==id ).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound("Cliente No Encontrado");
            }

                cliente.Estado = false;

                _dbContext.cliente.Update(cliente);
                _dbContext.SaveChanges();

                
               
                return Ok("Cliente Eliminado");

        }


    }
}
