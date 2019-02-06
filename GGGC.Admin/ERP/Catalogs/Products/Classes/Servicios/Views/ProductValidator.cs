using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using FluentValidation;
using GGGC.Admin.ERP.Catalogs.Products.Classes.Servicios.ViewModels;

namespace GGGC.Admin.ERP.Catalogs.Products.Classes.Servicios.Views
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(user => user.Descripcion)
                .NotEmpty()
                .WithMessage("Debe Ingresar una descripcion.");

            RuleFor(user => user.Linea)
               .NotEmpty()
               .NotNull()
              .WithMessage("Debe Ingresar Linea.");


            RuleFor(user => user.LineID)
              .NotEmpty()
              .NotNull()
              .NotEqual(0)
             .WithMessage("LINEAAAa.");
            

            //RuleFor(user => user.Email)
            //    .EmailAddress()
            //    .WithMessage("Please Specify a Valid E-Mail Address");

            //RuleFor(user => user.Zip)
            //    .Must(BeAValidZip)
            //    .WithMessage("Please Enter a Valid Zip Code");

        }

        private static bool BeAValidZip(string zip)
        {
            if (!string.IsNullOrEmpty(zip))
            {
                var regex = new Regex(@"\d{5}");
                return regex.IsMatch(zip);
            }
            return false;
        }
    }
}
