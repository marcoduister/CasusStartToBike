using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CasusStartToBike.Helper
{
    public class CheckAge : ValidationAttribute
    {
        private int _MinAge;


        public CheckAge(int minAge)
        {
            _MinAge = minAge;
        }


        public override bool IsValid(object value)
        {
            DateTime NewDate;
            if (DateTime.TryParse(value.ToString(), out NewDate))
            {
                ErrorMessage = "uw moet minimaal 14 zijn om mee temogen doen.";
                return NewDate.AddYears(_MinAge) < DateTime.Now;
            }
            ErrorMessage = "uw moet minimaal 14 zijn om mee temogen doen.";

            return false;
        }
    }
}
