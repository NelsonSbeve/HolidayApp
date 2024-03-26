using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHoliday
    {
        
        int GetDaysOfHolidayInsidePeriod(DateOnly StartDate, DateOnly EndDate);
        public bool IsColaboradorInHoliday(IColaborator colaborator);
        public IColaborator GetColaboratorwithMoreThen(int XDays);
    }
}