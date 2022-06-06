using System;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Entidad
{
    public class Producto
    {

        public Producto() { }

        public Producto(string imagen, string titulo, int precio, string color, int cantidad, string descripcion)
        {
            this.imagen = imagen;
            this.titulo = titulo;
            this.precio = precio;
            this.color = color;
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.imagenes = new List<string>();
        }

        public Producto(int idProducto, string imagen, string titulo, int precio, string color, int cantidad, string descripcion, int idEmpresa, int idCategoria, List<String> imagenes)
        {
            this.idProducto = idProducto;
            this.imagen = imagen;
            this.titulo = titulo;
            this.precio = precio;
            this.color = color;
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.idEmpresa = idEmpresa;
            this.idCategoria = idCategoria;
            this.imagenes = imagenes;
        }

        public Producto(int idProducto, string imagen, string titulo, int precio, string descripcion) {
            this.idProducto = idProducto;
            this.imagen = imagen;
            this.titulo = titulo;
            this.precio = precio;
            this.descripcion = descripcion;
        }

        public Producto(int idProducto, string imagen, string titulo, int precio, string color, int cantidad, string descripcion, int idEmpresa, int idCategoria)
        {
            this.idProducto = idProducto;
            this.imagen = imagen;
            this.titulo = titulo;
            this.precio = precio;
            this.color = color;
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.idEmpresa = idEmpresa;
            this.idCategoria = idCategoria;
        }

        public int idProducto { get; set; }

        public String imagen { get; set; }

        public String titulo { get; set; }

        public int precio { get; set; }

        public String color { get; set; }

        public int cantidad { get; set; }

        public String descripcion { get; set; }

        public int idEmpresa { get; set; }

        public int idCategoria { get; set; }

        public List<string> imagenes { get; set; } //List<string> string[]

        public string tostring()
        {
            return "Id: " + idProducto + " " + " url imagen: " + imagen + " " + " titulo: " + titulo + " " + " precio: " + precio + " Descripcion: " + descripcion + " Unidades: " + cantidad;
        }
    }
}
