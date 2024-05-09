using APIAssignmentDay2.Models;

namespace APIAssignmentDay2.Services
{
    public class PersonService
    {
        private readonly List<Person> _people;

        public PersonService()
        {
            // Initialize the list with dummy data, and use unique Id values
            _people = new List<Person>
        {
            new Person { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 5, 15), Gender = "Male", BirthPlace = "New York" },
            new Person { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1985, 10, 20), Gender = "Female", BirthPlace = "Los Angeles" }
            // Add more dummy data with unique Ids as needed
        };
        }

        // Method to list all people
        public List<Person> GetAllPeople()
        {
            return _people;
        }

        // Function to add a new person
        public void AddPerson(Person person)
        {
            // Assign a new Id value to the person and add it to the list
            person.Id = Guid.NewGuid();
            _people.Add(person);
        }

        // Function to update a person by Id
        public bool UpdatePerson(Guid id, Person updatedPerson)
        {
            var person = _people.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                // Update the person's properties
                person.FirstName = updatedPerson.FirstName;
                person.LastName = updatedPerson.LastName;
                person.DateOfBirth = updatedPerson.DateOfBirth;
                person.Gender = updatedPerson.Gender;
                person.BirthPlace = updatedPerson.BirthPlace;
                return true;
            }
            return false;
        }

        // Function to delete a person by Id
        public bool DeletePerson(Guid id)
        {
            var person = _people.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                _people.Remove(person);
                return true;
            }
            return false;
        }

        // Function to filter people based on name, gender, and birth place
        public List<Person> FilterPeople(string name, string gender, string birthPlace)
        {
            var query = _people.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name));
            }

            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(p => p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(birthPlace))
            {
                query = query.Where(p => p.BirthPlace.Equals(birthPlace, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }
    }
}