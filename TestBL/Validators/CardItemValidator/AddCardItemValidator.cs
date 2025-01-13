using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBL.DTOs.CardItemDTOs;

namespace TestBL.Validators.CardItemValidator;

public class AddCardItemValidator : AbstractValidator<AddCardItemDTO>
{
    public AddCardItemValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Please specify a title.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please specify a description.");
        RuleFor(x => x.Image).NotEmpty().WithMessage("Please insert an image.");
    }
}
