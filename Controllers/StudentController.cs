using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StdTest.Data;
using StdTest.Models;
using System.Data.SqlClient;
namespace StdTest.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDAL _studentRepo;
        private readonly string _connectionString;
        public StudentController(StudentDAL studentRepo, IConfiguration configuration)
        {
            _studentRepo = studentRepo;
            _connectionString = configuration.GetConnectionString("MyConnection");
        }


        // Action to display all students
        public IActionResult Index(int id=1)
        {
            int limit = 6;
            int recCount = 0;

            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            using (SqlCommand countCmd = new SqlCommand("SELECT COUNT(*) FROM Students", con))
            {
                recCount = (int)countCmd.ExecuteScalar();

                int noOfPage = (int)Math.Ceiling((double)recCount / limit);

                ViewBag.TotalPages = noOfPage;

            }
            var students = _studentRepo.GetAllStudents(id);
            return View(students);
        }

        // Action to handle the form submission for adding a new student
        [HttpPost]
        public IActionResult AddStudent(Students student)
        {
            if (ModelState.IsValid)
            {
                _studentRepo.AddStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }


        // Action to display the form for adding a new student
        public IActionResult AddStudentForm()
        {
            return View();
        }

        //Method to get specific Student from database
        public IActionResult Edit(int id)
        {
            var student = _studentRepo.GetStudentById(id);
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            _studentRepo.DelStudentById(id);
            return RedirectToAction("Index");
        }
        public IActionResult View(int id)
        {
            var student = _studentRepo.GetStudentById(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Students student)
        {
            if (ModelState.IsValid)
            {
                _studentRepo.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
    }
}
