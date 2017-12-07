using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;


namespace TrabajoPracticoAgustinBiglieri
{
    class Program
    {
        static void Main(string[] args)
        {
            //Metodo principal, Inicio del Programa.
            string x;

            do
            {
                Console.WriteLine("Bienvenido al  Trabajo Practico ");
                Console.WriteLine("Menu CRUD");

                do
                {
                    Console.WriteLine("Ingresar  - 1 Eliminar Order - 2 Crear Orden -3 Actualizar Orden  -4 Mostrar Facturas existentes -5 Salir");
                    x = Console.ReadLine();

                } while(x != "1" && x != "2" && x != "3" && x != "4" && x!="5");

                //creacion instancia DataServices.
                var Ser = new DataServices();





                switch (x)
                {
                    case "1":
                        

                        
                        Ser.DeleteOrder();
                        break;
                    case "2":
                        Ser.CreateOrder();
                        break;
                    case "3":
                        Ser.UpdateOrder();
                        break;
                        
                    case "4":
                        Ser.MostrarFacturas();
                        break;

                    case "5":
                        break;
                   




                }

            } while (x != "5");
            
        }
    }
}
