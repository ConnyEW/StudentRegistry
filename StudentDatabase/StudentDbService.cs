using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistry
{
    public class StudentDbService
    {
        StudentDbContext dbContext = new StudentDbContext();
        StudentGenerator studentGenerator = new StudentGenerator();
        private string input = "";

        public string AddStudent(string[] details)
        {
            // details is the string array returned from GetInputForAddStudent()
            var student = new Student(details[0], details[1], details[2], Convert.ToInt32(details[3]));
            dbContext.Add(student);
            dbContext.SaveChanges();

            return $"Student {student.FirstName} {student.LastName}, {student.Age} from {student.City} has been successfully added to the database.";
        }

        public List<Student> SearchStudent(string keyword)
        {
            // Using ToLower to make search string case-insensitive
            keyword = keyword.ToLower();

            // Filter first name, last name and city by keyword and then concatenate lists
            var searchFirstName = dbContext.Students.Where(s => s.FirstName.ToLower().Contains(keyword)).ToList();
            var searchLastName = dbContext.Students.Where(s => s.LastName.ToLower().Contains(keyword)).ToList();
            var searchCity = dbContext.Students.Where(s => s.City.ToLower().Contains(keyword)).ToList();

            var searchResults = new List<Student>();
            searchResults = searchFirstName.Concat(searchLastName).Concat(searchCity).ToList();

            searchResults.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.City);

            return searchResults;
        }

        public void EditStudent(string[] details)
        {
            // details is the string array returned from GetInputForEditStudent()
            var student = GetStudentById(Convert.ToInt32(details[0]));
            student.FirstName = details[1];
            student.LastName = details[2];
            student.City = details[3];
            student.Age = Convert.ToInt32(details[4]);
            dbContext.SaveChanges();
        }

        public DbSet<Student> GetStudentList()
        {
            return dbContext.Students;
        }

        public void GenerateMockDatabase()
        {
            dbContext.Students.AddRange(studentGenerator.Generate(100));
            dbContext.SaveChanges();
        }

        public void ClearDatabase()
        {
            var students = dbContext.Students;
            dbContext.Students.RemoveRange(students);
            dbContext.SaveChanges();
        }

        public Student GetStudentById(int id)
        {
            var student = dbContext.Students.FirstOrDefault(s => s.StudentId == id);
            return student;
        }

        public bool StudentExists(int id)
        {
            return dbContext.Students.Any(s => s.StudentId == id);
        }
    }
}
