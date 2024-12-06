using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum TipoProducto
    {
        PLATO = 1,
        BEBIDA = 2
    }
    public class Producto
    {
        public int Id {  get; set; }
        public string Nombre {  get; set; }
        public int Stock {  get; set; }
        public float Precio {  get; set; }
        public string UrlImagen {  get; set; }
        public TipoProducto TipoProducto {  get; set; }
        public bool Activo {  get; set; }

    }
}
