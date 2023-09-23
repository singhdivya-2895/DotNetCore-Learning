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
using CmswebApI.Mapping;

namespace CmsWebApi.Test.Mapping
{
    public class MappingTests
    {
        [Fact]
        public void Test_maptodto()
        {
            //Arrange
            var course = new Course()
            {
                CourseID = 2
            };
            var expectedId = 2;
            //Act
            var result = MappingHelper.MapCourseModelToCourseDto(course);
            //Assert
            Assert.Equal(expectedId, result.CourseID);
        }
        [Fact]
        public void Test_maptodomain()
        {
            //Arrange
            var coursedto = new CourseDto()
            {
                CourseID = 100
            };
            var expectedId = 100;
            //Act
            var result = MappingHelper.MapCourseDtoToCourseModel(coursedto);
            //Assert
            Assert.Equal(expectedId, result.CourseID);
        }
    }
}
