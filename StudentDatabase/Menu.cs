using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistry
{
    public class Menu
    {
        StudentDbService dbService = new StudentDbService();
        private string? input = "";
        private string[]? inputs;
        private bool loop = true;
        public void Display()
        {
            while (loop)
            {
                input = "";
                do
                {
                    Console.Clear();
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("--------------------");
                    Console.WriteLine("1. Add student");
                    Console.WriteLine("2. Search for student by name or city");
                    Console.WriteLine("3. Edit student details");
                    Console.WriteLine("4. Display all students");
                    Console.WriteLine("5. Generate mock database (clears current)");
                    Console.WriteLine("--------------------");
                    input = Console.ReadLine();
                } while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5");

                switch (input)
                {
                    case "1":
                        AddStudent();
                        break;

                    case "2":
                        SearchAndPrint();
                        break;

                    case "3":
                        EditStudent();
                        break;

                    case "4":
                        PrintAllStudents();
                        break;

                    case "5":
                        ClearDbAndGenerateMockDb();
                        break;
                }
                Console.ReadKey();
            }
        }

        // Menu I/O methods

        private void AddStudent()
        {
            inputs = GetInputForAddStudent();
            Console.WriteLine(dbService.AddStudent(inputs));
        }

        private void EditStudent()
        {
            inputs = GetInputForEditStudent();
            if (inputs == null) // array is null if id is non-existent
            {
                return;
            }
            dbService.EditStudent(inputs);
        }

        private string[] GetInputForAddStudent()
        {
            Console.Write("\nFirst name: ");
            input = Console.ReadLine();
            string? firstName = string.IsNullOrWhiteSpace(input) ? "John" : input;

            Console.Write("Last name: ");
            input = Console.ReadLine();
            string? lastName = string.IsNullOrWhiteSpace(input) ? "Doe" : input;

            string? ageString;
            do
            {
                Console.Write("Age: ");
                ageString = Console.ReadLine();
            } while (!IsPositiveInteger(ageString));

            Console.Write("City: ");
            input = Console.ReadLine();
            string? city = string.IsNullOrWhiteSpace(input) ? "Arbrå" : input;

            return new string[] { firstName, lastName, city, ageString };
        }

        private string[] GetInputForEditStudent()
        {
            string firstName = "";
            string lastName = "";
            string age = "";
            string city = "";
            string idString = "";
            Student student;

            do
            {
                Console.WriteLine("\nWhich student ID would you like to edit the details of?");
                idString = Console.ReadLine();
            } while (!IsPositiveInteger(idString));
            int id = Convert.ToInt32(idString);

            if (!dbService.StudentExists(id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There is no student with ID# {id}.");
                Console.ResetColor();
                return null;
            }
            student = dbService.GetStudentById(id);

            Console.WriteLine("* * * Editing student details - leave field blank to leave it unchanged * * *");

            Console.Write($"First name: {student.FirstName}. Enter new first name: ");
            input = Console.ReadLine();
            if (input == "")
            {
                firstName = student.FirstName;
            }
            else
            {
                firstName = input;
            }

            Console.Write($"Last name: {student.LastName}. Enter new last name: ");
            input = Console.ReadLine();
            if (input == "")
            {
                lastName = student.LastName;
            }
            else
            {
                lastName = input;
            }

            do
            {
                Console.Write($"Age: {student.Age}. Enter new age: ");
                input = Console.ReadLine();
                if (input == "")
                {
                    age = student.Age.ToString();
                    break;
                }
            } while (!IsPositiveInteger(input));
            if (input != "")
            {
                age = input;
            }

            Console.Write($"City: {student.City}. Enter new city: ");
            input = Console.ReadLine();
            if (input == "")
            {
                city = student.City;
            }
            else
            {
                city = input;
            }

            return new string[] { idString, firstName, lastName, city, age };
        }

        private void SearchAndPrint()
        {
            Console.Write("\nSearch database with keyword: ");
            string? keyword = Console.ReadLine();
            var searchResults = dbService.SearchStudent(keyword);
            foreach (var student in searchResults)
            {
                Console.WriteLine(student.ToString());
            }
            Console.WriteLine($"Your search yielded {searchResults.Count()} results.");
        }

        private void PrintAllStudents()
        {
            var students = dbService.GetStudentList();
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }
        }

        private void ClearDbAndGenerateMockDb()
        {
            Console.WriteLine("Clearing database...");
            dbService.ClearDatabase();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database successfully cleared.");
            Console.ResetColor();

            Console.WriteLine("Generating mock database...");
            dbService.GenerateMockDatabase();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Mock database successfully created.");
            Console.ResetColor();
        }

        // Helper method(s)
        private bool IsPositiveInteger(string input)
        {
            if (!int.TryParse(input, out int output) || output < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Input must be a positive integer.");
                Console.ResetColor();
                return false;
            }
            return true;
        }

    }
}
