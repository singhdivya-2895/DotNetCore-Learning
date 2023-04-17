using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using Xunit;
using CmswebApI.Controllers;
using CmswebApI.Repository.Repositories;
using CmswebApI.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using CmswebApI.DTOs;
using COURSE_TYPE = CmswebApI.Repository.Models.COURSE_TYPE;
using Microsoft.AspNetCore.Http;

namespace CmsWebApi.Test.Controllers
{
    public class CoursesControllerTests
    {
        private Mock<ICmsrepository> _mockRepo;
        private CoursesController _controller;

        [Fact]
        public void Constructor_Throws()
        {
            // Arrange
            // Act 
            Action act = () => _ = new CoursesController(null);
            // Assert
            act.Should().Throw<ArgumentNullException>("Because the Icmsrepository was not supplied");
        }


        [Fact]
        public void Constructor_Success()
        {
            // Arrange            
            var cmsRepository = new Mock<ICmsrepository>();
            // Act 
            Action act = () => _ = new CoursesController(cmsRepository.Object);
            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public async Task GetCoursesAsync_WhenCalled_ReturnsOkResultWithExpectedData()
        {
            // Arrange
            _mockRepo = new Mock<ICmsrepository>();
            _controller = new CoursesController(_mockRepo.Object);

            var courses = new List<Course>
            {
                new Course { 
                    CourseID = 2,
                    CourseName = "Computer Science - Mock1",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.Engineering 
                },
                new Course { 
                    CourseID = 1,
                    CourseName = "Computer Science - Mock2",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.Engineering 
                }
            };
            _mockRepo.Setup(repo => repo.GetAllCoursesAsync()).ReturnsAsync(courses);

            // Act
            var result = await _controller.GetCoursesAsync();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CourseDto>>>(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var coursesResult = (List<CourseDto>)okResult.Value;
            Assert.Equal(courses.Count, coursesResult.Count);

            for (int i = 0; i < courses.Count; i++)
            {
                Assert.Equal(courses[i].CourseID, coursesResult[i].CourseID);
                Assert.Equal(courses[i].CourseName, coursesResult[i].CourseName);
                Assert.Equal(courses[i].CourseDuration, coursesResult[i].CourseDuration);
                Assert.Equal(courses[i].CourseType.ToString(), coursesResult[i].CourseType.ToString());
            }
        }
    
        [Fact]
        public async Task GetCoursesAsync_WhenErrorOccurs_ReturnsInternalServerError()
        {
            // Arrange
            _mockRepo = new Mock<ICmsrepository>();
            _controller = new CoursesController(_mockRepo.Object);
            _mockRepo.Setup(repo => repo.GetAllCoursesAsync()).ThrowsAsync(new Exception("Test exception message"));

            // Act
            var result = await _controller.GetCoursesAsync();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CourseDto>>>(result);
            Assert.IsType<ObjectResult>(result.Result);

            var statusCodeResult = (ObjectResult)result.Result;
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}