using System.Xml.XPath;

namespace Domain;

public class Holiday : IHoliday
{
	private IColaborator _colaborator;

	private List<HolidayPeriod> _holidayPeriods = new List<HolidayPeriod>();

	public Holiday(IColaborator colab)
	{
		if(colab!=null)
			_colaborator = colab;
		else
			throw new ArgumentException("Invalid argument: colaborator must be non null");

		
	}

	public HolidayPeriod addHolidayPeriod(DateOnly startDate, DateOnly endDate) {

	   HolidayPeriod holiday = new HolidayPeriod(startDate, endDate);
	   _holidayPeriods.Add(holiday);
	   return holiday;
	}
	// public HolidayPeriod AddHolidayPeriod(HolidayPeriodFactory hpFactory, DateOnly startDate, DateOnly endDate)
    // {
    //     //HolidayPeriod holidayPeriod = new HolidayPeriod(startDate, endDate);
    //     HolidayPeriod holidayPeriod = hpFactory.NewHolidayPeriod(startDate, endDate);
    //     _holidayPeriods.Add(holidayPeriod);
    //     return holidayPeriod;
    // }

	public List<HolidayPeriod> GetHolidayPeriod( DateOnly startDate, DateOnly endDate){

		List<HolidayPeriod> result = [];

		foreach(HolidayPeriod p in _holidayPeriods){
			if(p.getStartDate() >= startDate && p.getEndDate() <= endDate){
				result.Add(p);
			}
		}

		return result;
	}



	public int GetDaysOfHolidayInsidePeriod(DateOnly projectStartDate, DateOnly projectEndDate){

		var totalResult = 0;
		foreach(var period in _holidayPeriods){
			DateOnly periodStart = period._startDate > projectStartDate ? period._startDate : projectStartDate;
			DateOnly periodEnd = period._endDate < projectEndDate ? period._endDate : projectEndDate;
			// Check if the holiday period intersects with the specified period
			if (periodStart <= periodEnd)
			{
				// Calculate the number of days within the intersection

				int daysInIntersection = (periodEnd.ToDateTime(TimeOnly.Parse("10:00PM")) - periodStart.ToDateTime(TimeOnly.Parse("10:00PM"))).Days + 1;

				totalResult += daysInIntersection;
			}
		}


		return totalResult;
	}

	public IColaborator GetColaboratorwithMoreThen(int XDays){
        foreach (var period in _holidayPeriods)
		{
			DateTime endDateTime = period._endDate.ToDateTime(TimeOnly.Parse("10:00PM"));
			DateTime startDateTime = period._startDate.ToDateTime(TimeOnly.Parse("10:00PM"));
			TimeSpan difference = endDateTime.Subtract(startDateTime);
            int numberOfDays = difference.Days;
            if (numberOfDays > XDays){
				return _colaborator;
			}

		}
		return null;
	}

	public bool IsColaboradorInHoliday(IColaborator colaborator)
    {
        if (colaborator.Equals(_colaborator))
        {
            return true;
        }
 
        return false;
    }


}




