using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TransactionAPI.Model;
using TransactionAPI.Service;

namespace TransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        string connectionString = "Data Source=SWAPNILLAPTOP;Initial Catalog=transactionDataBase;Integrated Security=True;TrustServerCertificate=True;";
        //private TransactionService transactionService = new TransactionService();

        [HttpPost("AddAllTransaction")]
        public IActionResult AddAllTransaction([FromBody] AddTransaction addTransaction)
        {   
            TransactionService transactionService = new TransactionService();

            int rows=transactionService.addtransactions(addTransaction);
            return Ok();
                //  return CreatedAtAction(nameof(AddTransaction), new { id = addTransaction.Item }, "Transaction created successfully."); 
        }




        [HttpGet("GetTransaction")]
        //public IActionResult GetTransaction()
        public IActionResult GetTransaction([FromQuery] String sortproperty=null)

        {
            List<TransactionRequest> rows;
            TransactionService transactionService = new TransactionService();
            try
            {
               
                rows = transactionService.GetAllTransaction(sortproperty);
                return Ok(rows);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error retrieving data from the database: " + ex.Message);
            }
          
        }


        [HttpPatch("updateTransaction")]
        public IActionResult updateTransaction([FromBody] UpdateTransactionRequest updateTransactionRequest)
        {
            TransactionService transactionService = new TransactionService();
            int rows;
            rows = transactionService.UpdateSingleRecordById(updateTransactionRequest);
            //UpdateTransactionResponse response;

            Console.WriteLine("Updated Rows " + rows);
            return Ok();
        }

        [HttpDelete("deleteTransaction")]
        public IActionResult deleteTransaction([FromBody]DeleteTransaction deleteTransaction)
        {
            TransactionService transactionService = new TransactionService();
            try
            {
          Boolean rows=  transactionService.deleteRecords(deleteTransaction);
            Console.WriteLine("Deleted Rows" + rows);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error retrieving data from the database: " + ex.Message);
            }
            return Ok();
        }
        [HttpDelete("purgeTransaction")]
        public IActionResult purgeTransaction([FromBody] DeleteTransaction deleteTransaction)
        {
            TransactionService transactionService = new TransactionService();
            try
            {
                Boolean rows = transactionService.purgeRecords(deleteTransaction);
                Console.WriteLine("Deleted Rows" + rows);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error retrieving data from the database: " + ex.Message);
            }
            return Ok();
        }
    }
    }
