using Ejercicio_2.Data;
using Ejercicio_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ejercicio_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public VentaController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var venta = _dbContext.venta.Where(c=> c.Estado==true).ToList();

            var response = new
            {
                Status = 200,
                Message = "Listado de Ventas",
                Data = venta

            };

            return Ok(response);

            
        }

        [HttpGet("GetByCliente")]
        public IActionResult GetByIdCliente(int id)
        {
            var venta = _dbContext.venta.Where(c => c.ClienteId == id && c.cliente.Estado == true && c.Estado==true).ToList();

            if (venta == null)
            {
                return NotFound("Cliente No Encontrado");
            }

                var response = new
                {
                    Status = 200,
                    Message = "Listado de Ventas de cliente",
                    Data = venta

                };

                return Ok(response);


        }

        [HttpPost("VentaStore")]

        public IActionResult Store([FromForm] Venta request)
        {
            var cliente = _dbContext.cliente.Where(c => c.ClienteId == request.ClienteId && c.Estado==true).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound("Cliente no existe");
            }
            var producto = _dbContext.producto.Where(c => c.ProductoId == request.ProductoId && c.Estado == true).FirstOrDefault();
            if (producto == null)
            {
                return NotFound("Producto No existe");
            }
            var cantidad = request.Cantidad;
            var producto_precio = _dbContext.producto.Where(c => c.ProductoId == request.ProductoId).FirstOrDefault();
            var precio = producto_precio.PrecioUnitario;
            var precio_final = precio * cantidad;
            request.Cantidad = cantidad;
            request.Total=precio_final;
            _dbContext.venta.Add(request);
                _dbContext.SaveChanges();

                var response = new
                {
                    Status = 200,
                    Message = "Venta Creada",
                    Data = request

                };

                return Ok(response);


        }

        [HttpPut("VentaUpdate/{id}")]

        public IActionResult Update([FromForm] Venta request , int id)
        {
            var venta = _dbContext.venta.Where(c => c.VentaId == id && c.Estado==true).FirstOrDefault();
            if (venta == null)
            {
                return NotFound("Venta no encontrada");
            }
            var cliente = _dbContext.cliente.Where(c => c.ClienteId == request.ClienteId && c.Estado == true).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound("Cliente no existe");
            }
            var producto = _dbContext.producto.Where(c => c.ProductoId == request.ProductoId && c.Estado == true).FirstOrDefault();
            if (producto == null)
            {
                return NotFound("Producto No existe");
            }
            
            var cantidad = request.Cantidad;
            var producto_precio = _dbContext.producto.Where(c => c.ProductoId == request.ProductoId).FirstOrDefault();
            var precio = producto_precio.PrecioUnitario;
            var precio_final = precio*cantidad;

                venta.ProductoId = request.ProductoId;
                venta.ClienteId = request.ClienteId;
                venta.Cantidad = cantidad;
            
                venta.Total = precio_final;
                _dbContext.venta.Update(venta);
                _dbContext.SaveChanges();

                var response = new
                {
                    Status = 200,
                    Message = "Venta Actualizad",
                    Data = venta

                };

                return Ok(response);


        }
        [HttpPut("VentaDelete/{id}")]

        public IActionResult Delete(int id)
        {
            var venta = _dbContext.venta.Where(c => c.VentaId == id && c.Estado == true).FirstOrDefault();
            if (venta == null)
            {
                return NotFound("Venta no encontrada");
            }


            venta.Estado = false;

            _dbContext.venta.Update(venta);

            _dbContext.SaveChanges();

            return Ok("Venta Eliminada");


        }


    }
}
