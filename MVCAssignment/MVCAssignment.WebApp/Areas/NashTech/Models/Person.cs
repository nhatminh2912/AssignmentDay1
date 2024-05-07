using System.ComponentModel.DataAnnotations;

namespace MVCAssignment.WebApp.Areas.NashTech.Models
{
    public class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public GenderType Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string BirthPlace { get; set; } = string.Empty;
        public bool isGraduated { get; set; }
        public Person()
        {

        }
        public Person(string firstName, string lastName, GenderType gender, DateOnly dOB, string phoneNumber, string birthPlace, bool isGraduated)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DOB = dOB;
            PhoneNumber = phoneNumber;
            BirthPlace = birthPlace;
            this.isGraduated = isGraduated;
        }

    }
}
