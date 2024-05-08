namespace MVCAssignment.WebApp.Areas.NashTech.Models
{
    public interface IPersonService
    {
        void Create(Person person);
        void Update(int personId, Person updatedPerson);
        void Delete(int personId);
        List<Person> ListAll();
    }
}
