using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web2236903.Models
{
    public class Detalles
    {
        public string nombreCliente { get; set; }
        public string documentoCliente { get; set; }
        public string correoCliente { get; set; }
        public DateTime fechaCompra { get; set; }
        public int totalCompra { get; set; }
    }
}