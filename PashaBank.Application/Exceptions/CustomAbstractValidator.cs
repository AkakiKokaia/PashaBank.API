﻿using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Exceptions
{
    public abstract class CustomAbstractValidator<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            try
            {
                var result = base.Validate(context);
                if (!result.IsValid)
                {
                    throw new PashaBank.Application.Exceptions.ValidationException(result.Errors);
                }
                return result;
            }
            catch (FluentValidation.ValidationException ex)
            {
                throw new PashaBank.Application.Exceptions.ValidationException(ex.Errors);
            }
        }
    }
}
