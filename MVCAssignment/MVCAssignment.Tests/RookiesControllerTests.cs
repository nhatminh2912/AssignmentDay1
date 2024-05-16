using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCAssignment.WebApp;
using MVCAssignment.WebApp.Areas.NashTech.Controllers;
using MVCAssignment.WebApp.Areas.NashTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MVCAssignment.Tests.Controllers
{
    public class RookiesControllerTests
    {
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly RookiesController _controller;
        private readonly List<Person> _testPeople;

        public RookiesControllerTests()
        {
            _mockPersonService = new Mock<IPersonService>();
            _controller = new RookiesController(_mockPersonService.Object);
            _testPeople = _controller.ListInitial(); // Use the ListInitial method to get initial data
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfPeople()
        {
            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);
            Assert.Equal(_testPeople.Count, model.Count());
        }

        [Fact]
        public void ViewUpdate_ReturnsViewResult_WithId()
        {
            string id = "1";

            var result = _controller.ViewUpdate(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Update", viewResult.ViewName);
            Assert.Equal(id, viewResult.Model);
        }

        [Fact]
        public void Update_ValidId_UpdatesPerson()
        {
            string id = "1";
            string firstname = "NewFirstName";
            string lastname = "NewLastName";
            GenderType gender = GenderType.Female;
            DateOnly DOB = new DateOnly(1990, 1, 1);
            string phonenumber = "0123456789";
            string birthplace = "NewPlace";
            bool isgraduated = true;

            var result = _controller.Update(id, firstname, lastname, gender, DOB, phonenumber, birthplace, isgraduated);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);
            var updatedPerson = model.FirstOrDefault(p => p.Id == int.Parse(id));

            Assert.NotNull(updatedPerson);
            Assert.Equal(firstname, updatedPerson.FirstName);
            Assert.Equal(lastname, updatedPerson.LastName);
            Assert.Equal(gender, updatedPerson.Gender);
            Assert.Equal(DOB, updatedPerson.DOB);
            Assert.Equal(phonenumber, updatedPerson.PhoneNumber);
            Assert.Equal(birthplace, updatedPerson.BirthPlace);
            Assert.Equal(isgraduated, updatedPerson.isGraduated);
        }

        [Fact]
        public void ViewDetails_ReturnsViewResult_WithPersonDetails()
        {
            int id = 1;
            var person = _testPeople.First(p => p.Id == id);

            var result = _controller.ViewDetails(id, person.FirstName, person.LastName, person.Gender, person.DOB, person.PhoneNumber, person.BirthPlace, person.isGraduated);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Person>(viewResult.ViewData.Model);

            Assert.Equal(person.Id, model.Id);
            Assert.Equal(person.FirstName, model.FirstName);
            Assert.Equal(person.LastName, model.LastName);
            Assert.Equal(person.Gender, model.Gender);
            Assert.Equal(person.DOB, model.DOB);
            Assert.Equal(person.PhoneNumber, model.PhoneNumber);
            Assert.Equal(person.BirthPlace, model.BirthPlace);
            Assert.Equal(person.isGraduated, model.isGraduated);
        }

        [Fact]
        public void Delete_ValidId_RemovesPerson()
        {
            int id = 1;

            var result = _controller.Delete(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);

            Assert.DoesNotContain(model, p => p.Id == id);
        }

        [Fact]
        public void ViewCreate_ReturnsViewResult()
        {
            var result = _controller.ViewCreate();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Create", viewResult.ViewName);
        }

        [Fact]
        public void Create_AddsNewPerson()
        {
            string firstname = "NewFirstName";
            string lastname = "NewLastName";
            GenderType gender = GenderType.Female;
            DateOnly DOB = new DateOnly(1990, 1, 1);
            string phonenumber = "0123456789";
            string birthplace = "NewPlace";
            bool isgraduated = true;

            var result = _controller.Create(firstname, lastname, gender, DOB, phonenumber, birthplace, isgraduated);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);

            Assert.Contains(model, p => p.FirstName == firstname && p.LastName == lastname && p.Gender == gender && p.DOB == DOB && p.PhoneNumber == phonenumber && p.BirthPlace == birthplace && p.isGraduated == isgraduated);
        }

        [Fact]
        public void FindMale_ReturnsMaleOnly()
        {
            var result = _controller.FindMale();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);

            Assert.All(model, p => Assert.Equal(GenderType.Male, p.Gender));
        }

        [Fact]
        public void FindOldest_ReturnsOldestPerson()
        {
            var result = _controller.FindOldest();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);
            var oldestYear = _testPeople.Min(p => p.DOB.Year);

            Assert.All(model, p => Assert.Equal(oldestYear, p.DOB.Year));
        }

        [Fact]
        public void FullName_ReturnsViewResult_WithListOfPeople()
        {
            var result = _controller.FullName();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);
            Assert.Equal(_testPeople.Count, model.Count());
        }

        [Theory]
        [InlineData("greater", 2000)]
        [InlineData("less", 2000)]
        [InlineData("equal", 2000)]
        public void FindBirthYear_ReturnsPeopleBasedOnYear(string method, int year)
        {
            var result = _controller.FindBirthYear(method, year);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.ViewData.Model);

            switch (method)
            {
                case "greater":
                    Assert.All(model, p => Assert.True(p.DOB.Year > year));
                    break;
                case "less":
                    Assert.All(model, p => Assert.True(p.DOB.Year < year));
                    break;
                case "equal":
                    Assert.All(model, p => Assert.Equal(year, p.DOB.Year));
                    break;
            }
        }
    }
}
