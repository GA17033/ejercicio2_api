using System;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio_2.Models
{
    public class Venta
    {
        [Key]
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public Producto producto { get; set; }
        public int ClienteId { get; set; }
        public Cliente cliente { get; set; }
        public int Cantidad { get; set; }

        public float Total { get; set; }

        public bool Estado { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
