using dotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Npgsql;

namespace dotNetProject.Controllers
{
    public class FormController : Controller
    {
        private readonly ILogger<FormController> _logger;
        private readonly DbContextDatabase _context;

        public FormController(ILogger<FormController> logger, DbContextDatabase context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult FormView()
        {
            var FormData = new FormDataModal();

            //string connectionString = "Host=localhost;port=5432;Database=form_data;Username=form_data;Password=form_data";
            //FormDataModal? users = null;

            //using (NpgsqlConnection conn = new(connectionString))
            //{
            //    conn.Open();
            //    Console.WriteLine("connection is Open");
            //    string sqlQuery = "SELECT * FROM public.user_data ORDER BY user_id ASC LIMIT 1;";
            //    NpgsqlCommand cmd = new(sqlQuery, conn);
            //    NpgsqlDataReader reader = cmd.ExecuteReader();

            //    if (reader.HasRows)
            //    {
            //        Console.WriteLine("There are rows");

            //        while (reader.Read())
            //        {
            //            users = new FormDataModal
            //            {
            //                UserId = reader.GetInt32(0),
            //                FirsName = reader.GetString(1),
            //                MiddleName = reader.GetString(2),
            //                LastName = reader.GetString(3),
            //                Email = reader.GetString(4),
            //                Phone = reader.GetString(5),
            //                Age = reader.GetInt32(6)
            //            };
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No rows found.");
            //    }
            //}

                return View(FormData);
        }

        public IActionResult OperationView()
        {
            return View();
        }

        // POST method for inserting data into database
        // Triggered when 'submit' is cliced in 'create user' page
        // Form/InsertUser POST method

        public IActionResult InsertUser(FormDataModal formData)
        {
            if (ModelState.IsValid)
            {
                if (formData.DateOfBirth.HasValue)
                {
                    formData.DateOfBirth = formData.DateOfBirth.Value.Date;
                }
                _context.UserData.Add(formData);
                _context.SaveChanges();

                ViewBag.showModal = true;
                return View("FormView", formData);
            }
            return View("FormView", formData);

        }   
    }
}
