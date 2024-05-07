using Microsoft.AspNetCore.Mvc;
using MVCAssignment.WebApp.Areas.NashTech.Models;
using System.Collections;
using ClosedXML.Excel;
using System.Data;

namespace MVCAssignment.WebApp.Areas.NashTech.Controllers
{
    [Area("NashTech")]
    public class RookiesController : Controller
    {

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

            List<Person> MaleOnly = new List<Person>();
            foreach (Person male in people)
            {
                if (male.Gender == GenderType.Male) MaleOnly.Add(male);
            }
            return View(MaleOnly);
        }

        public IActionResult FindOldest()
        {
            List<Person> people = new List<Person>();
            people = AddPersonToList();

            Person oldestPerson = people[0];

            for (int i = 1; i < people.Count; i++)
            {
                if (people[i].DOB.Year < oldestPerson.DOB.Year) oldestPerson = people[i];
            }
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

            List<Person> peopleBasedOnYear = new List<Person>();

            switch (method)
            {
                case "greater":
                    for (int i = 0; i < people.Count; i++)
                    {
                        if (people[i].DOB.Year > year) peopleBasedOnYear.Add(people[i]);
                    }
                    return View(peopleBasedOnYear);
                case "less":
                    for (int i = 0; i < people.Count; i++)
                    {
                        if (people[i].DOB.Year < year) peopleBasedOnYear.Add(people[i]);
                    }
                    return View(peopleBasedOnYear);
                case "equal":
                    for (int i = 0; i < people.Count; i++)
                    {
                        if (people[i].DOB.Year == year) peopleBasedOnYear.Add(people[i]);
                    }
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
            people.Add(new Person("Minh", "Hoang", GenderType.Male, new DateOnly(1997, 12, 29), "0900000000", "Ha Noi", true));
            people.Add(new Person("Nhat", "Bui", GenderType.Male, new DateOnly(1998, 01, 30), "0911111111", "Hai Phong", true));
            people.Add(new Person("Hien", "Phan", GenderType.Female, new DateOnly(1999, 02, 01), "09222222222", "Nam Dinh", false));
            people.Add(new Person("Dung", "Nguyen", GenderType.Female, new DateOnly(2000, 03, 02), "093333333333", "Nha Trang", true));
            people.Add(new Person("Manh", "Tran", GenderType.Male, new DateOnly(2001, 04, 03), "09444444444", "Da Nang", false));
            return people;
        }
    }
}
