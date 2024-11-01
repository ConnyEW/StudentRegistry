using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistry
{
    public class StudentGenerator
    {
        Random rand = new Random();

        public List<string> FirstNames = new List<string>
        {
            "Conny", "Tomas", "Roger", "Alfred", "Emil", "Anton", "Adam", "Erik", "Jonas", "Ulf", "Victor", "Oskar", "Ludvig", "Johan", "Pål", "Marcus", "Niklas", "Per", "Karl", "Gustav", 
            "Jennifer", "Malin", "Amanda", "Frida", "Fredrika", "Nora", "Matilda", "Kajsa", "Alice", "Evelina", "Linda", "Moa", "Rebecca", "Sara", "Josefine", "Stina", "Helena", "Emma", "Emelie", "Anna"
        };
        public static List<string> LastNames = new List<string>
        {
            "Engström", "Wernersson", "Jonsson", "Jansson", "Andersson", "Johansson", "Gustavsson", "Lundgren", "Haraldsson", "Nilsson", "Berggren", "Enberg", "Fredriksson", "Dalgårdh", "Rapp", "Grenström", "Kvist", "Ung", "Myrh", "Kämpe"
        };
        public static List<string> Cities = new List<string>
        {
            "Arbrå", "Bollnäs", "Alfta", "Edsbyn", "Ljusdal", "Vallsta", "Söderhamn", "Hudiksvall", "Gävle", "Ockelbo", "Järvsö", "Delsbo", "Kilafors", "Rengsjö", "Undersvik", "Iste", "Ramsjö", "Ånge", "Enånger", "Kårböle", "Ljusne", "Sandviken"
        };

        // Generate randomized students and set properties based on above lists
        public List<Student> Generate(int numberOfStudents)
        {
            List<Student> students = new List<Student>();
            for (int i = 0; i < numberOfStudents; i++)
            {
                var student = new Student(FirstNames[rand.Next(0, FirstNames.Count)], LastNames[rand.Next(0, LastNames.Count)], Cities[rand.Next(0, Cities.Count)], rand.Next(16, 70));
                students.Add(student);
            }
            return students;
        }
    }
}
