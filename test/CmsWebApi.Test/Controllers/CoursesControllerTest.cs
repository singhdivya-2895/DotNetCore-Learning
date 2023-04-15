using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using Xunit;
using CmswebApI.Controllers;

namespace CmsWebApi.Test.Controllers
{
    public class CoursesControllerTests
    {
        [Fact]
        public void Throws_Exception()
        {
            // Arrange
            // Act 
            // Assert
            Action act = () => _ = new CoursesController(null);
            act.Should().Throw<ArgumentNullException>("Because the Icmsrepository was not supplied");
        }
    }
}