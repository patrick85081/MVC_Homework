using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Homework1.Models.Validations
{
    public class CellPhoneValdationAttribute : RegularExpressionAttribute
    {
        public CellPhoneValdationAttribute() :
            base("\\d{4}-\\d{6}")
        {
        }
    }
}