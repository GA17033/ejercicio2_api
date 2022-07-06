using Ejercicio_2.Data;
using Ejercicio_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ejercicio_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public ProductoController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var producto = _dbContext.producto.Where(c => c.Estado == true).ToList();
            var response = new
            {
                Status = 200,
                Message = "Listado de Productos",
                Data = producto
            };

            return Ok(response);


        }

        [HttpGet("GetById/{id}")]

        public  IActionResult GetById(int id)
        {
            var producto = _dbContext.producto.Where(c => c.ProductoId == id && c.Estado == true).FirstOrDefault();

            var response = new
            {
                Status = 200,
                Message = "Producto Por Id",
                Data = producto
            };

            return Ok(response);

        }

        [HttpGet("GetByName/{Nombre}")]

        public IActionResult GetByName(string Nombre)
        {
            var producto = _dbContext.producto.Where(c => c.Nombre == Nombre && c.Estado == true).FirstOrDefault();

            var response = new
            {
                Status = 200,
                Message = "Producto Por Nombre",
                Data = producto
            };

            return Ok(response);

        }
        [HttpPost("StoreProducto")]
        public IActionResult Store([FromForm] Producto request)
        {
            var producto = _dbContext.producto.Where(c=> c.Nombre==request.Nombre && c.Estado==true).FirstOrDefault();
            if (producto != null)
            {
                return BadRequest("Producto ya existe");
            }
              _dbContext.producto.Add(request);

              _dbContext.SaveChanges();


                var response = new
                {
                    Status = 200,
                    Message = "Producto Creado",
                    Data = request
                };

                return Ok(response);


        }

        [HttpPut("UpdateProducto/{id}")]
        public IActionResult Update([FromForm] Producto request , int id)
        {
            var producto = _dbContext.producto.Where(c => c.ProductoId == id && c.Estado == true).FirstOrDefault();
            if (producto == null)
            {
                return BadRequest("Producto no Encontrado");
            }

                producto.Nombre = request.Nombre;
                producto.Descripcion = request.Descripcion;
                producto.PrecioUnitario = request.PrecioUnitario;
                producto.PrecioVenta = request.PrecioVenta;

                _dbContext.producto.Update(producto);
                _dbContext.SaveChanges();


                    var response = new
                    {
                        Status = 200,
                        Message = "Producto Actualizado",
                        Data = producto
                    };

                    return Ok(response);


        }

        [HttpPut("DeleteProducto/{id}")]
        public IActionResult Delete( int id)
        {
            var producto = _dbContext.producto.Where(c => c.ProductoId == id && c.Estado == true).FirstOrDefault();
            if (producto == null)
            {
                return BadRequest("Producto No Encontrado");
            }

                 producto.Estado = false;

                _dbContext.producto.Update(producto);
                _dbContext.SaveChanges();

                return Ok("Producto Eliminado");


        }

    }
}
