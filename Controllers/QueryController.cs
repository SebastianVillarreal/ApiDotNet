using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Text;

namespace ApiCore.Controllers;

[ApiController]
public class VentasController : ControllerBase
{
    [HttpGet("ventas/all")]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        this.Conectar();
        return new []
        {
            new Person { Name = "Ana" },
            new Person { Name = "Felipe" },
            new Person { Name = "Emillia" }
        };
    }


    [HttpGet("ventas/conectar")]
    public void Conectar()
    {
        try 
        { 
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "localhost, 1433"; 
            builder.UserID = "sa";            
            builder.Password = "ABC1238f47";     
            builder.InitialCatalog = "TSQLV4";
        
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");
                
                connection.Open();       

                String sql = "exec GetSales";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                        }
                    }
                }                    
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        Console.WriteLine("\nDone. Press enter.");
        Console.ReadLine(); 
    }
}

public class Person
{
    public string Name { get; set; }
}