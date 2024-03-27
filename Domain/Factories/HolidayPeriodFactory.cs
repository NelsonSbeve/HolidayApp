using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class HolidayPeriodFactory : IFactoryHolidayPeriod
    {
 
        public IHolidayPeriod newHolidayPeriod( DateOnly startDate, DateOnly endDate) {
 
            return new HolidayPeriod(startDate, endDate);
 
        }
    }
}