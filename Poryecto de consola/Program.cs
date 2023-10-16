using System;
using System.Collections.Generic;

namespace Poryecto_de_consola
{
    class Program
    {
        static void Main(string[] args)
        {
            PlusMinus();
        }

        public static void PlusMinus()
        {
            int positivos = 0;
            int negativos = 0;
            int neutrales = 0;
            int total = 0;
            int tamano;
            decimal decimalValue;
            tamano = int.Parse(Console.ReadLine());
            string entrada = Console.ReadLine();
            foreach (int objeto in arr)
            {
                if (objeto > 0)
                {
                    positivos++;
                }
                else if (objeto < 0)
                {
                    negativos++;
                }
                else
                {
                    neutrales++;
                }
            }
            total = positivos + negativos + neutrales;
            if (positivos > 0)
            {
                decimalValue = decimal.Round(positivos / total, 6);
                Console.WriteLine(decimalValue);
            }
            if (negativos > 0)
            {
                decimalValue = decimal.Round(negativos / total, 6);
                Console.WriteLine(decimalValue);
            }
            if (neutrales > 0)
            {
                decimalValue = decimal.Round(neutrales / total, 6);
                Console.WriteLine(decimalValue);
            }
        }
    }
}
