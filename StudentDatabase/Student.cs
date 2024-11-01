using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistry
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string City { get; set; } = "";

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 15)
                {
                    _age = 16;  // Let's assume this school is for ages 16 and up.
                }
                else
                {
                    _age = value;
                }
            }
        }

        public Student(string firstName, string lastName, string city, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Age = age;
        }

        public override string? ToString()
        {
            return $"ID: {StudentId.ToString().PadRight(5)} \tNAME: {FirstName} {LastName.PadRight(23)} \tAGE: {Age.ToString().PadRight(2)} \tCITY: {City}";
        }
    }
}
