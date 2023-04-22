using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using CmswebApI.DTOs;

namespace CmswebApI.Validators
{
    // AbstractValidator on your model, so that middleware can run the validation.
    // Status Code: 400 (Bad Request) if validation fails
    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {

        public CourseDtoValidator()
        {
            //RuleFor(t => t.CourseID)
            //    .NotEmpty() // Check if it is empty else give the below validation message
            //        .WithMessage("Please Provide a Course ID");

            RuleFor(t => t.CourseName)
                .NotEmpty() // Check if it is empty else give the below validation message
                    .WithMessage("Please Provide a Course Name")
                .Length(0, 25) // Check if the length is more than 0, and less than 25 else show the message
                    .WithMessage("You have exceeded max length allowed for Course Name.");

            RuleFor(t => t.CourseDuration)
                .InclusiveBetween(1,4) // Check if the value is between 1 and 4 else show the message
                    .WithMessage("Itne saal ka koun sa course hota hai bhai");
        }
    }
}