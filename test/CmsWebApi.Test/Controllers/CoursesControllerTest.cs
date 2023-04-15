using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using Xunit;
using CmswebApI.Controllers;
using CmswebApI.Repository.Repositories;

namespace CmsWebApi.Test.Controllers
{
    public class CoursesControllerTests
    {
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
    }
}