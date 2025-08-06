using Microsoft.AspNetCore.Mvc;
using StdTest.Data;
using StdTest.Models;
using System.Data.SqlClient;
namespace StdTest.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDAL _studentRepo;

        public StudentController(StudentDAL studentRepo)
        {
            _studentRepo = studentRepo;
        }

        // Action to display all students
        public IActionResult Index(int id)
        {
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
