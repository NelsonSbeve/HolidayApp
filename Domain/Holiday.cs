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

		//_colaborator = colab ?? throw new ArgumentException("Invalid argument: colaborator must be non null");
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

	public List<HolidayPeriod> GetHolidayPeriods()
    {
        return _holidayPeriods;
    }


	public int GetDaysOfHoliday(DateOnly startDate, DateOnly endDate){

		DateTime startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Year );
        DateTime endDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day);
		
		TimeSpan difference = endDateTime.Subtract(startDateTime);
		int numberOfDays = difference.Days;


		return numberOfDays;
	}

	public IColaborator GetColaborator() {


	   return _colaborator;
;
	}

}

