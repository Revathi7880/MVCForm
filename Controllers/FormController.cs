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
        
        public IActionResult SearchUser([FromBody] FormDataModal searchData)
        {
            var searchQuery = "SELECT * FROM public.form_data where 1=1";
            if(!string.IsNullOrEmpty(searchData.FirstName))
            {
                searchQuery += $" AND first_name ILIKE '%{searchData.FirstName}%'";
            }
            if (!string.IsNullOrEmpty(searchData.LastName))
            {
                searchQuery += $" AND last_name ILIKE '%{searchData.LastName}%'";
            }
            if (!string.IsNullOrEmpty(searchData.Email))
            {
                searchQuery += $" AND email ILIKE '%{searchData.Email}%'";
            }
            if (searchData.Age > 0)
            {
                searchQuery += $" AND age = {searchData.Age}";
            }
            if (!string.IsNullOrEmpty(searchData.Nationality))
            {
                searchQuery += $" AND nationality ILIKE '%{searchData.Nationality}%'";
            }
            if (!string.IsNullOrEmpty(searchData.Occupation))
            {
                searchQuery += $" AND occupation = '{searchData.Occupation}'";
            }
            Console.WriteLine(searchQuery);
            string connectionString = "Host=localhost;port=5432;Database=form_data;Username=form_data;Password=form_data";
            var users = new List<FormDataModal>();

            using(var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(searchQuery, conn))
                {
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new FormDataModal
                            {
                                UserId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(3),
                                Email = reader.GetString(4),
                                Nationality = reader.GetString(8),
                                Occupation = reader.GetString(9),
                            });
                        }
                    }
                }
            }
            return Json(users);
            //return View("OperationView", searchData);
        }
    }
}
