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
using COURSE_TYPE_2 = CmswebApI.DTOs.COURSE_TYPE;
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

            var coursesModel = new List<Course>
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
            _mockRepo.Setup(repo => repo.AddCoursesAsync()).ReturnsAsync(coursesModel);

            var expectedDtoList = new List<CourseDto>
            {
                new CourseDto { 
                    CourseID = 2,
                    CourseName = "Computer Science - Mock1",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE_2.Engineering 
                },
                new CourseDto { 
                    CourseID = 1,
                    CourseName = "Computer Science - Mock2",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE_2.Engineering 
                }
            };

            // Act
            var result = await _controller.GetCoursesAsync();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CourseDto>>>(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var actualCourseDtoList = (List<CourseDto>)okResult.Value;
            Assert.Equal(expectedDtoList.Count, actualCourseDtoList.Count);
            for (int i = 0; i < expectedDtoList.Count; i++)
            {
                Assert.Equal(expectedDtoList[i].CourseID, actualCourseDtoList[i].CourseID);
                Assert.Equal(expectedDtoList[i].CourseName, actualCourseDtoList[i].CourseName);
                Assert.Equal(expectedDtoList[i].CourseDuration, actualCourseDtoList[i].CourseDuration);
                Assert.Equal(expectedDtoList[i].CourseType, actualCourseDtoList[i].CourseType);
            }
        }
    
        [Fact]
        public async Task GetCoursesAsync_WhenErrorOccurs_ReturnsInternalServerError()
        {
            // Arrange
            _mockRepo = new Mock<ICmsrepository>();
            _controller = new CoursesController(_mockRepo.Object);
            _mockRepo.Setup(repo => repo.AddCoursesAsync()).ThrowsAsync(new Exception("Test exception message"));
            var expectedOutcome = StatusCodes.Status500InternalServerError;
            // Act
            var result = await _controller.GetCoursesAsync();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CourseDto>>>(result);
            Assert.IsType<ObjectResult>(result.Result);

            var actualResult = (ObjectResult)result.Result;
            Assert.Equal(expectedOutcome, actualResult.StatusCode);
        }

        [Fact]
        public async Task AddCoursesAsync_WhenErrorOccurs_ReturnsInternalServerError()
        {
            // Arrange
            _mockRepo = new Mock<ICmsrepository>();
            _controller = new CoursesController(_mockRepo.Object);
            _mockRepo.Setup(repo => repo.AddCourseAsync(It.IsAny<Course>())).ThrowsAsync(new Exception("Test exception message"));
            var expectedOutcome = StatusCodes.Status500InternalServerError;
            var dto = new CourseDto();

            // Act
            var result = await _controller.AddCourseAsync(dto);

            // Assert
            Assert.IsType<ActionResult<CourseDto>>(result);

            var actualResult = (ObjectResult)result.Result;
            Assert.Equal(expectedOutcome, actualResult.StatusCode);
        }
        [Fact]
        public async Task AddCoursesAsync_WhenCalled_ReturnsOkResultWithExpectedData()
        {
            // Arrange
            _mockRepo = new Mock<ICmsrepository>();
            _controller = new CoursesController(_mockRepo.Object);
            var newCourse = new Course() {
                CourseID = 100,
                CourseDuration = 2,
                CourseName = "Mock",
                CourseType = COURSE_TYPE.Medical                
            };
            _mockRepo.Setup(repo => repo.AddCourseAsync(It.IsAny<Course>())).ReturnsAsync(newCourse);
            var dto = new CourseDto();
            var expectedDto = new CourseDto()
            {
                CourseID = 100,
                CourseDuration = 2,
                CourseName = "Mock",
                CourseType = COURSE_TYPE_2.Medical
            };
           

            // Act
            var result = await _controller.AddCourseAsync(dto);

            // Assert
            Assert.IsType<ActionResult<CourseDto>>(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var actualCourseDto = (CourseDto)okResult.Value;
            Assert.Equal(expectedDto.CourseID, actualCourseDto.CourseID);
            Assert.Equal(expectedDto.CourseDuration, actualCourseDto.CourseDuration);
            Assert.Equal(expectedDto.CourseName, actualCourseDto.CourseName);

        }
    }
}