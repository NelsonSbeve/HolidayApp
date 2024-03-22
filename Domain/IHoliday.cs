using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHoliday
    {
        
        int GetDaysOfHolidayInsidePeriod(DateOnly StartDate, DateOnly EndDate);
        public List<HolidayPeriod> GetHolidayPeriod( DateOnly startDate, DateOnly endDate);
        public bool IsColaboradorInHoliday(IColaborator colaborator);
        public IColaborator GetColaboratorwithMoreThen(int XDays);
    }
}