using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio_2.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public float PrecioUnitario { get; set; }

        public float PrecioVenta { get; set; }
        public bool Estado { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public virtual ICollection<Venta> venta { get; set; }

    }
}
