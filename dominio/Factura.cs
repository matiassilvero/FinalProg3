using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Factura
    {
        public int Id { get; set; }
        public int NumeroFactura { get; set; }//Va ser el IdPedido
        public int Mesa { get; set; }
        public int Mesero { get; set; }
        public DateTime Fecha { get; set; }
        public float Importe { get; set; }
    }
}
