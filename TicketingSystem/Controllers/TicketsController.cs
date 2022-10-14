using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TicketsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select * from dbo.Tickets";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TicketSysteAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Tickets ticket)
        {
            string query = @"
                    insert into dbo.Tickets
                    (Title, Category, CompUser, TicketStatus)
                    values 
                    (
                    '" + ticket.Title + @"'
                    ,'" + ticket.Category + @"'
                    ,'" + ticket.CompUser + @"'
                    ,'" + ticket.TicketStatus + @"'
                    )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TicketSysteAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Ticket Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Tickets ticket)
        {
            string query = @"
                    update dbo.Tickets set 
                    Title = '" + ticket.Title + @"'
                    ,Category = '" + ticket.Category + @"'
                    ,CompUser = '" + ticket.CompUser + @"'
                    where TicketNum = " + ticket.TicketNum + @" 
                    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TicketSysteAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Ticket Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Tickets
                    where TicketNum = " + id + @" 
                    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TicketSysteAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Ticket Deleted Successfully");
        }
    }
}
