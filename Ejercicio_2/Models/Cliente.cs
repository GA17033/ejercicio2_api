using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio_2.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        
        public string Apellido { get; set; }
        public string Domicilio { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public virtual ICollection<Venta> venta { get; set; }
    }
}
