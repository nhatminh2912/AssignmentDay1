using Microsoft.AspNetCore.Mvc;
using MVCAssignment.WebApp.Areas.NashTech.Models;
using ClosedXML.Excel;
using System.Data;

namespace MVCAssignment.WebApp.Areas.NashTech.Controllers
{
    [Area("NashTech")]
    public class RookiesController : Controller
    {
        private readonly IPersonService _personService;
        private List<Person> _people;
        // Constructor with dependency injection
        public RookiesController(IPersonService personService)
        {
            _personService = personService;
            _people = ListInitial();
        }

        public IActionResult ViewUpdate(string id)
        {
            return View("Update", id);
        }

        [HttpPost]
        public IActionResult Update(string id, string firstname, string lastname, GenderType gender, DateOnly DOB, string phonenumber, string birthplace, bool isgraduated)
        {

            for (int i = 0; i<_people.Count; i++)
            {
                if (int.TryParse(id, out int result)) 
                {
                    if (_people[i].Id == result)
                    {
                        _people[i].FirstName = firstname;
                        _people[i].LastName = lastname;
                        _people[i].Gender = gender;
                        _people[i].DOB = DOB;
                        _people[i].PhoneNumber = phonenumber;
                        _people[i].BirthPlace = birthplace;
                        _people[i].isGraduated = isgraduated;

                        return View("Index", _people);
                    }
                }
                
            }
            return View("Index", _people);
        }

        public IActionResult ViewDetails(int id, string firstname, string lastname, GenderType gender, DateOnly DOB, string phonenumber, string birthplace, bool isgraduated)
        {
                Person per = new Person
                {
                    Id=id,
                    FirstName = firstname,
                    LastName = lastname,
                    Gender = gender,
                    DOB = DOB,
                    PhoneNumber = phonenumber,
                    BirthPlace = birthplace,
                    isGraduated = isgraduated
                };
            return View("Details", per);
        }

        public IActionResult Delete(int id)
        {

            foreach (Person per in _people)
            {
                    if (per.Id == id)
                    {
                        _people.Remove(per);
                        return View("Index", _people);
                    }          
            }
            return View("Index", _people);
        }

        public IActionResult ViewCreate()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(string firstname, string lastname, GenderType gender, DateOnly DOB, string phonenumber, string birthplace, bool isgraduated)
        {

            Person newPerson = new Person {
                Id = _people.Count,
                FirstName = firstname,
                LastName = lastname,
                Gender = gender,
                DOB = DOB,
                PhoneNumber = phonenumber,
                BirthPlace  = birthplace,
                isGraduated = isgraduated
            };

            _people.Add(newPerson);

            return View("Index", _people);
        }

        public IActionResult Index()
        {
            return View(_people);
        }

        public IActionResult FindMale()
        {

            IEnumerable<Person> MaleOnly = from person in _people
                                           where person.Gender == GenderType.Male
                                           select person;

            return View(MaleOnly);
        }

        public IActionResult FindOldest()
        {

            IEnumerable<Person> oldestPerson = from person in _people
                                               where person.DOB.Year == _people.Min(x => x.DOB.Year)
                                               select person;
            return View(oldestPerson);
        }

        public IActionResult FullName()
        {
            return View(_people);
        }

        public IActionResult FindBirthYear(string method, int year)
        {

            IEnumerable<Person> peopleBasedOnYear = new List<Person>();

            switch (method)
            {
                case "greater":
                    peopleBasedOnYear = from person in _people
                                        where person.DOB.Year > year
                                        select person;
                    return View(peopleBasedOnYear);
                case "less":
                    peopleBasedOnYear = from person in _people
                                        where person.DOB.Year < year
                                        select person;
                    return View(peopleBasedOnYear);
                case "equal":
                    peopleBasedOnYear = from person in _people
                                        where person.DOB.Year == year
                                        select person;
                    return View(peopleBasedOnYear);
            }

            return View(peopleBasedOnYear);
        }
        public FileResult ReturnFile()
        {

            DataTable dt = new DataTable("List");

            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("FirstName"),
                                            new DataColumn("LastName"),
                                            new DataColumn("Gender"),
                                            new DataColumn("DOB"),
                                            new DataColumn("PhoneNumber"),
                                            new DataColumn("BirthPlace"),
                                            new DataColumn("isGraduated") });
            foreach (var person in _people)
            {
                dt.Rows.Add(person.FirstName, person.LastName, person.Gender, person.DOB, person.PhoneNumber, person.BirthPlace, person.isGraduated);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List.xlsx");
                }
            }
        }

        public List<Person> ListInitial()
        {
            List<Person> people = new List<Person>();
            people.Add(new Person(0, "Minh", "Hoang", GenderType.Male, new DateOnly(1997, 12, 29), "0900000000", "Ha Noi", true));
            people.Add(new Person(1, "Nhat", "Bui", GenderType.Male, new DateOnly(1998, 01, 30), "0911111111", "Hai Phong", true));
            people.Add(new Person(2, "Hien", "Phan", GenderType.Female, new DateOnly(1999, 02, 01), "09222222222", "Nam Dinh", false));
            people.Add(new Person(3, "Dung", "Nguyen", GenderType.Female, new DateOnly(2000, 03, 02), "093333333333", "Nha Trang", true));
            people.Add(new Person(4, "Manh", "Tran", GenderType.Male, new DateOnly(2001, 04, 03), "09444444444", "Da Nang", false));
            people.Add(new Person(5, "Hoang", "Nguyen", GenderType.Male, new DateOnly(2002, 05, 04), "0955555555", "Ha Dong", true));
            people.Add(new Person(6, "Chien", "Truong", GenderType.Female, new DateOnly(2003, 06, 05), "0966666666", "Hai Duong", false));
            people.Add(new Person(7, "Thang", "Le", GenderType.Male, new DateOnly(2004, 07, 06), "0977777777", "Can Tho", false));
            people.Add(new Person(8, "Cuong", "Pham", GenderType.Female, new DateOnly(2005, 08, 07), "0988888888", "Da Nang", true));
            people.Add(new Person(9, "Loi", "Loc", GenderType.Female, new DateOnly(2006, 09, 08), "09999999999", "Thanh Hoa", false));
            people.Add(new Person(10, "Vang", "Ha", GenderType.Male, new DateOnly(2007, 10, 09), "0987654321", "Nghe An", false));
            people.Sort((x,y)=>x.Id.CompareTo(y.Id));
            return people;
        }
    }
}
