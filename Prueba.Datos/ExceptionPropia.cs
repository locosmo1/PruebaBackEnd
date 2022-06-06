using System;

namespace Prueba.Datos
{
    public class ExceptionPropia : ApplicationException
    {
        public int numero;
        public ExceptionPropia(string mensaje, Exception original) : base(mensaje, original) { }

        public ExceptionPropia(string mensaje) : base(mensaje) { }

        public ExceptionPropia(int numero) : base()
        {
            this.numero = numero;
        }
    }
}