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

        //Controller action for displaying and empty 'create user' page
        public IActionResult FormView()
        {
            return View();
        }

        //Controller action to display the user list page with empty filters
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

        // POST method for searching data in database using user entered filter data
        // Triggered when 'submit' is cliced in 'User List' page
        // Form/SearchUser POST method
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
                                MiddleName = !reader.IsDBNull(2)? reader.GetString(2): string.Empty,
                                LastName = reader.GetString(3),
                                Email = reader.GetString(4),
                                Phone = reader.GetString(5),
                                DateOfBirth = reader.GetDateTime(6),
                                Age = reader.GetInt32(7),
                                Nationality = reader.GetString(8),
                                Occupation = !reader.IsDBNull(9)? reader.GetString(9): string.Empty,
                                Address1 = !reader.IsDBNull(10)? reader.GetString(10): string.Empty,
                                Address2 = !reader.IsDBNull(11) ? reader.GetString(1) : string.Empty,
                                City = !reader.IsDBNull(12) ? reader.GetString(12) : string.Empty,
                                State = !reader.IsDBNull(13) ? reader.GetString(13) : string.Empty,
                                Country = !reader.IsDBNull(14) ? reader.GetString(14) : string.Empty,
                                Pincode = !reader.IsDBNull(15) ? reader.GetString(15) : string.Empty,
                                Degree = !reader.IsDBNull(16) ? reader.GetString(16) : string.Empty,
                                Institution = !reader.IsDBNull(17) ? reader.GetString(17) : string.Empty,
                                YearCompleted = !reader.IsDBNull(18)? reader.GetInt32(18) : 0
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
