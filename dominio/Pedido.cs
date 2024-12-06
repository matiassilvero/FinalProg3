using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum Estado
    {
        EnPREPARACION = 1,
        ENTREGADO = 2,
        CANCELADO = 3,
        FACTURADO = 4
    }
    public class Pedido
    {
        public int Id {  get; set; }
        public int IdMesa {  get; set; }
        public int IdMesero {  get; set; }
        public DateTime Fecha { get; set; }
        public Estado Estado { get; set; }
    }
}
