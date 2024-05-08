using Microsoft.AspNetCore.Mvc;
using MVCAssignment.WebApp.Areas.NashTech.Models;
using System.Collections;
using ClosedXML.Excel;
using System.Data;
using System.Linq;
using Humanizer;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MVCAssignment.WebApp.Areas.NashTech.Controllers
{
    [Area("NashTech")]
    public class RookiesController : Controller
    {
        private readonly IPersonService _personService;

        // Constructor with dependency injection
        public RookiesController(IPersonService personService)
        {
            _personService = personService;
        }

        public IActionResult ViewUpdate(string id)
        {
            return View("Update", id);
        }

        [HttpPost]
        public IActionResult Update(string id, string firstname, string lastname, GenderType gender, DateOnly DOB, string phonenumber, string birthplace, bool isgraduated)
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            for (int i = 0; i<people.Count; i++)
            {
                if (int.TryParse(id, out int result)) 
                {
                    if (people[i].Id == result)
                    {
                        people[i].FirstName = firstname;
                        people[i].LastName = lastname;
                        people[i].Gender = gender;
                        people[i].DOB = DOB;
                        people[i].PhoneNumber = phonenumber;
                        people[i].BirthPlace = birthplace;
                        people[i].isGraduated = isgraduated;

                        return View("Index", people);
                    }
                }
                
            }
            return View("Index", people);
        }

        public IActionResult ViewCreate()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(string firstname, string lastname, GenderType gender, DateOnly DOB, string phonenumber, string birthplace, bool isgraduated)
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            Person newPerson = new Person();
            newPerson.Id = people.Count + 1;
            newPerson.FirstName = firstname;
            newPerson.LastName = lastname;
            newPerson.Gender = gender;
            newPerson.DOB = DOB;
            newPerson.PhoneNumber = phonenumber;
            newPerson.BirthPlace = birthplace;
            newPerson.isGraduated = isgraduated;

            people.Add(newPerson);

            return View("Index", people);
        }

        public IActionResult Index()
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();
            return View(people);
        }

        public IActionResult FindMale()
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            IEnumerable<Person> MaleOnly = from person in people
                                           where person.Gender == GenderType.Male
                                           select person;

            return View(MaleOnly);
        }

        public IActionResult FindOldest()
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            IEnumerable<Person> oldestPerson = from person in people
                                               where person.DOB.Year == people.Min(x => x.DOB.Year)
                                               select person;
            return View(oldestPerson);
        }

        public IActionResult FullName()
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            return View(people);
        }

        public IActionResult FindBirthYear(string method, int year)
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            IEnumerable<Person> peopleBasedOnYear = new List<Person>();

            switch (method)
            {
                case "greater":
                    peopleBasedOnYear = from person in people
                                        where person.DOB.Year > year
                                        select person;
                    return View(peopleBasedOnYear);
                case "less":
                    peopleBasedOnYear = from person in people
                                        where person.DOB.Year < year
                                        select person;
                    return View(peopleBasedOnYear);
                case "equal":
                    peopleBasedOnYear = from person in people
                                        where person.DOB.Year == year
                                        select person;
                    return View(peopleBasedOnYear);
            }

            return View(peopleBasedOnYear);
        }
        public FileResult ReturnFile()
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            DataTable dt = new DataTable("List");

            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("FirstName"),
                                            new DataColumn("LastName"),
                                            new DataColumn("Gender"),
                                            new DataColumn("DOB"),
                                            new DataColumn("PhoneNumber"),
                                            new DataColumn("BirthPlace"),
                                            new DataColumn("isGraduated") });
            foreach (var person in people)
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

        public List<Person> AddPersonToList()
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
