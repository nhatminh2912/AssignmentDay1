using APIAssignmentDay2.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIAssignmentDay2.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("all")]
        public IActionResult GetAllPeople()
        {
            var allPeople = _personService.GetAllPeople();
            return Ok(allPeople);
        }

        // Add new person
        [HttpPost]
        public IActionResult AddPerson([FromBody] Person person)
        {
            _personService.AddPerson(person);
            return Ok("Person added successfully");
        }

        // Update person by Id
        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
        {
            var success = _personService.UpdatePerson(id, updatedPerson);
            if (success)
            {
                return Ok("Person updated successfully");
            }
            return NotFound("Person not found");
        }

        // Delete a person by Id
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var success = _personService.DeletePerson(id);
            if (success)
            {
                return Ok("Person deleted successfully");
            }
            return NotFound("Person not found");
        }

        // Filter list of people
        [HttpGet]
        public IActionResult FilterPeople([FromQuery] string name, [FromQuery] string gender, [FromQuery] string birthPlace)
        {
            var filteredPeople = _personService.FilterPeople(name, gender, birthPlace);
            return Ok(filteredPeople);
        }
    }
}