using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHolidayPeriod
    {
        public DateOnly ValidateInitialDate(DateOnly Date);
        public DateOnly ValidateFinalDate( DateOnly Date);
        public DateOnly getEndDate();
        public DateOnly getStartDate();
        
    }
}