using Microsoft.AspNetCore.Mvc;
using StdTest.Models;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace StdTest.Data
{
    public class StudentDAL
    {
        private readonly string _connectionString;

        public StudentDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

        // Method to Get all students from Database
        [Obsolete]
        public IEnumerable<Students> GetAllStudents()
        {
            List<Students> students = new List<Students>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students", conn))
                //using (SqlCommand cmd = new SqlCommand("SELECT TOP (6) * FROM Students", conn))
                //using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students ORDER BY StudentId OFFSET @Pg ROWS FETCH NEXT 6 ROWS ONLY", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        students.Add(new Students
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            Name = reader["Name"].ToString(),
                            lName = reader["lName"].ToString(),
                            Address = reader["Address"].ToString(),
                            Mobile = Convert.ToInt64(reader["Mobile"])
                        });
                    }
                }
            }

            return students;
        }

        // Method to Get specific student from Database
        public IEnumerable<Students> GetStudentById(int id)
        {
            List<Students> student = new List<Students>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Students WHERE StudentId = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        student.Add(new Students
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            Name = reader["Name"].ToString(),
                            lName = reader["lName"].ToString(),
                            Address = reader["Address"].ToString(),
                            Mobile = Convert.ToInt64(reader["Mobile"])
                        });
                    }
                }
            }

            return student;
        }

        // Method to Insert a Student in Database
        public void AddStudent(Students student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "EXEC spAddEmployee @Name, @lName, @Address, @Mobile";
                //string query = "INSERT INTO Students (Name, lName, Address, Mobile) VALUES (@Name, @lName, @Address, @Mobile)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@lName", student.lName);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to Update a Student in Database
        public void UpdateStudent(Students student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Students SET Name=@Name, lName=@lName, Address=@Address, Mobile=@Mobile WHERE StudentId=@stdId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@stdId", student.StudentId);
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@lName", student.lName);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}