using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Domain
{
    public interface IFactoryHolidayPeriod
    {
        public IHolidayPeriod newHolidayPeriod(DateOnly startDate, DateOnly endDate);
    }
}