using Db;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Services
{
    public class DataServices

    {

        public void DeleteOrder()
        //Metodo Eliminar una orden.

        {
            Console.WriteLine("SubMenu Eliminar Orden");
            Console.WriteLine("Ingresar Id orden");
            int.TryParse(Console.ReadLine(), out int orderid);
            using (var context = new NORTHWINDEntities())

            {



               
                    var query = context.Orders.Where(o => o.OrderID == orderid).FirstOrDefault<Orders>();
                if(query!=null)
                {
                    var customerid = query.CustomerID;
                    var query2 = context.Customers.Where(c => c.CustomerID == customerid).FirstOrDefault();
                    var country1 = "France";
                    var country2 = "Mexico";

                    if (query2 != null)
                    {

                        if ((query2.Country != country1) || (query2.Country != country2))
                        {
                            var DeleteOrderDetails = context.Order_Details.Where(O => O.OrderID == orderid);

                            if (DeleteOrderDetails.Count() != 0)
                            {
                                foreach (var order in DeleteOrderDetails)
                                {
                                    context.Order_Details.Remove(order);
                                }
                                var DeleteOrder = context.Orders.Where(o => o.OrderID == orderid).First();
                                context.Orders.Remove(DeleteOrder);


                                context.SaveChanges();
                                Console.WriteLine($"Order {orderid} eliminada, cliente {query2.CustomerID}, pais {query2.Country}");

                            }
                            else
                            {
                                Console.WriteLine($"El detalle de orden {orderid} Ya fue anteriormente eliminada");
                            }

                        }

                        else
                        {
                            Console.WriteLine($"No se pueden eliminar Detalles de ordenes con clientes de {country1} y {country2}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("no existe el ordenID a buscar");
                    }

                }
                else
                {
                    Console.WriteLine("no existe el ordenID a buscar");
                }
                    
             }
                
                
        }







           








        
        public void CreateOrder()
        {
            Console.WriteLine("SubMenu Crear Orden");

            var context = new NORTHWINDEntities();
            {
                //Ingreso orderid

                Console.WriteLine("Ingresar ID orden ");
                int.TryParse(Console.ReadLine(), out int orderID);
                var existeOrderID = context.Orders.Where(o => o.OrderID == orderID);
                if(existeOrderID!=null)
                {
                    Console.WriteLine($"Ya Existe el orderID numero {orderID} ");
                }
                else
                {
                 

                //Ingreso CustomerID
                Console.WriteLine("Ingresar ID Cliente");
                var customerID = Console.ReadLine();




                    //Ingreso Empleado


                    Employees EmpleadoId;

                    do { 

                    Console.WriteLine("Ingresar Nombre Empleado");
                    var nomEmp = Console.ReadLine();
                    Console.WriteLine("Ingresar Apellido Empleado");
                    var apeEmp = Console.ReadLine();
                     EmpleadoId = context.Employees.Where(e => (e.FirstName == nomEmp) && e.LastName == apeEmp).FirstOrDefault();
                        //Muestra el mensaje.
                        if(EmpleadoId==null)
                        {
                        Console.WriteLine("Debe Ingresar un Empleado que exista");
                        }
                        
                        

                    } while (EmpleadoId==null);


                //Ingreso OrderDate, Validar si el ingreso de formato es correcto
                Console.WriteLine("Ingresar fecha de Orden dd-mm-yyyy");
                DateTime OrderDate = Convert.ToDateTime(Console.ReadLine());
                //Ingreso RequieredDate
                Console.WriteLine("Ingresar fecha Requerida dd-mm-yyyy");
                DateTime RequieredDate = Convert.ToDateTime(Console.ReadLine());
                //Ingreso ShippedDate
                Console.WriteLine("Ingresar fecha de Envio dd-mm-yyyy");
                DateTime shippedDate = Convert.ToDateTime(Console.ReadLine());
                //Ingreso ShipVia
                Console.WriteLine("Ingresar via de envio");
                int.TryParse(Console.ReadLine(), out int shipvia);
                // Ingreso Freight
                Console.WriteLine("Ingresar Precio de Envio");
                 var freight =  decimal.Parse(Console.ReadLine());
                // Ingreso ShipName
                Console.WriteLine("Ingresar nombre del envio");
                string shipName = Console.ReadLine();
                // Ingreso ShipAddress
                Console.WriteLine("Ingresar direccion de envio");
                string shipaddress = Console.ReadLine();
                // Ingreso Shipcity
                Console.WriteLine("Ingresar ciudad de envio");
                string shipCity = Console.ReadLine();
                // Ingreso ShipRegion
                Console.WriteLine("Ingresar region de envio");
                string shipRegion = Console.ReadLine();
                // Ingreso ShipPostalCode
                Console.WriteLine("Ingresar codigo postal de envio");
                string shipPostalCode = Console.ReadLine();
                
                    // Ingreso ShipCountry
                Console.WriteLine("Ingresar Pais de envio");
                string shipCountry = Console.ReadLine();

                    // Creacion del objeto order.
                    Orders order = new Orders()
                    {
                        OrderID = orderID,
                        CustomerID = customerID,
                        EmployeeID = EmpleadoId.EmployeeID,
                        OrderDate = OrderDate,
                        RequiredDate = RequieredDate,
                        ShippedDate = shippedDate,

                        ShipVia = shipvia,
                        Freight = freight,
                        ShipName = shipName,
                        ShipAddress = shipaddress,

                        ShipCity = shipCity,
                        ShipRegion = shipRegion,
                        ShipPostalCode = shipPostalCode,
                        ShipCountry = shipCountry

                    };


                    //Agregar
                    
                    context.Orders.Add(order);

                    //SaveChanges

                    context.SaveChanges();
                       

                    




                    //Ingreso detalle de order
                    string opcion;

                do
                {


                    short x;
                    float discount;


                        int productID;
                        IQueryable<Products> queryProd;



                        Console.WriteLine("Ingresar Detalle de orden");
                        Console.WriteLine("Ingresar ID de Producto");

                        do
                        {
                            productID = int.Parse(Console.ReadLine());
                            queryProd = context.Products.Where(p => p.ProductID == productID);
                            if (queryProd == null)
                            {
                                Console.WriteLine("Ingresar ID de producto valida");
                            }
                        } while (queryProd == null);



                        var unitPrice = queryProd.FirstOrDefault().UnitPrice;



                        do
                        {

                        Console.WriteLine("Ingresar Cantidades del producto, debe ser mayor a 0");
                        x = short.Parse(Console.ReadLine());
                      



                    } while (x<= 0);

                    do
                    {
                        Console.WriteLine("Ingresar Descuento, debe ser mayor o igual a 0, y menos a 30");
                        discount = float.Parse(Console.ReadLine());

                    } while ((discount > 30) || (discount < 0));


                    Console.WriteLine("desea seguir ingresando detalles de ordenes? si/no");
                    opcion = Console.ReadLine().ToLower();

                    while (opcion != "si" || opcion != "no")
                    {
                        Console.WriteLine("Debe ingresar si/no");
                        opcion = Console.ReadLine().ToLower();


                    };

                        var  OrdenDetalle = context.Order_Details.Where(c => c.OrderID == orderID);
                        foreach (var ord in OrdenDetalle)
                        {
                            ord.OrderID = orderID;
                            ord.ProductID = productID;
                            ord.UnitPrice = unitPrice.Value;
                            ord.Quantity = x;
                            ord.Discount = discount;
                        }

                        //Guardado de cambios
                        context.SaveChanges();
                        
                        // Para calcular
                        OrdenDetalle = context.Order_Details.Where(c => c.OrderID == orderID);
                        var desc = Convert.ToDouble(OrdenDetalle.First().Discount);
                        var precioUni = Convert.ToDouble(OrdenDetalle.First().UnitPrice);
                        var cant = Convert.ToDouble(OrdenDetalle.First().Quantity);
                        var importeTot = ((1 - desc) * (precioUni * cant));



                        Console.WriteLine($"Orden ID {orderID} con importe {importeTot} se a creado correctamente ");
               
                   

                       

                } while (opcion =="si");


                }
                


                






            }


        






        }

        public void UpdateOrder()


        {
            using (var Context = new NORTHWINDEntities())
            {
                //Ingreso orderid

                Console.WriteLine("Ingresar ID orden ");
                int.TryParse(Console.ReadLine(), out int orderID);
                var existeOrderID = Context.Order_Details.Where(o => o.OrderID == orderID);
                IQueryable<Order_Details> OrdenDetalle;
                if (existeOrderID != null)
                {

                    OrdenDetalle = Context.Order_Details.Where(c => c.OrderID == orderID);
                    short x;
                    float discount;
                    string productName;
                    Products queryProd;
                    


                    Console.WriteLine("Ingresar Detalle de orden");
                    Console.WriteLine("Ingresar Nombre de Producto");
                    
                    do
                    {
                        productName = Console.ReadLine();
                        queryProd = Context.Products.Where(p => p.ProductName == productName).FirstOrDefault();
                        if (queryProd == null)
                        {
                            Console.WriteLine("Ingresar nombre de producto valido");
                        }
                    } while (queryProd == null);

                    var productID = queryProd.ProductID;



                    var unitPrice = queryProd.UnitPrice;

                    

                    


                    do
                    {

                        Console.WriteLine("Ingresar Cantidades del producto, debe ser mayor a 0");
                        x = short.Parse(Console.ReadLine());




                    } while (x <= 0);

                    do
                    {
                        Console.WriteLine("Ingresar Descuento, debe ser mayor o igual a 0%, y menos a 30%");
                        discount = float.Parse(Console.ReadLine());

                    } while ((discount > 30) || (discount < 0));

                    foreach(var ord in OrdenDetalle)
                    {
                        ord.ProductID = productID;
                        ord.UnitPrice = unitPrice.Value;
                        ord.Quantity = x;
                        ord.Discount = discount/100;
                    }

                    //Guardado de cambios
                    Context.SaveChanges();

                    // Para calcular
                    OrdenDetalle = Context.Order_Details.Where(c => c.OrderID == orderID);
                    var desc = Convert.ToDouble(OrdenDetalle.First().Discount);
                    var precioUni = Convert.ToDouble(OrdenDetalle.First().UnitPrice);
                    var cant =Convert.ToDouble( OrdenDetalle.First().Quantity);
                    var importeTot = ((1 - desc) * (precioUni * cant));
                   
                    Console.WriteLine($"El {orderID} con importe total {importeTot} a sido modificado");



                } else
                
                {
                    Console.WriteLine("No Existe el orderID ingresado");
                }

                
            }
                


               
            


        }


        public void MostrarFacturas()
        {
            using (var context = new NORTHWINDEntities())
            {
                Console.WriteLine("Submenu Mostrar Facturas");
                foreach(var ord in context.Orders)
                {
                    var orderID = ord.OrderID;
                    Console.WriteLine($"Numero de orden {orderID}");
                    var CustomerName = context.Customers.Where(c => c.CustomerID == ord.CustomerID).First();
                    Console.WriteLine($"Nombre del cliente {CustomerName.CompanyName}");
                    var OrdenDetalle = context.Order_Details.Where(o => o.OrderID == ord.OrderID).FirstOrDefault();
                    if(OrdenDetalle ==null)
                    {
                        Console.WriteLine($"No existen detalles ordenes con ese Order ID");
                    }

                    else
                    {
                        var desc = Convert.ToDouble(OrdenDetalle.Discount);
                        var precioUni = Convert.ToDouble(OrdenDetalle.UnitPrice);
                        var cant = Convert.ToDouble(OrdenDetalle.Quantity);
                        var importeTot = ((1 - desc) * (precioUni * cant));

                        Console.WriteLine($"Importe Total {importeTot}");

                    }

                    
                   





                }

            }
                

        
        }

       


    }
}

