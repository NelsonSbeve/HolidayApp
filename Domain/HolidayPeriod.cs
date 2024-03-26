using System.Net;

namespace Domain;

public class HolidayPeriod 
{
	private DateOnly _startDate;
	private DateOnly _endDate;

	int _status;

	public HolidayPeriod(DateOnly startDate, DateOnly endDate)
	{
		if( startDate < endDate ) {
			_startDate = startDate;
			_endDate = endDate;
		}
		else
			throw new ArgumentException("invalid arguments: start date >= end date.");
	}

	// public int DurationInDays(){
	// 	var days= _endDate.DayNumber-_startDate.DayNumber;
	// 	return days;

	// }
	public DateOnly ValidateInitialDate(DateOnly Date)
    {
        var StartDate = _startDate >= Date ? _startDate : Date;
        
		return StartDate;
    }

	public DateOnly ValidateFinalDate( DateOnly Date)
	{
		var EndDate = _endDate <= Date ? _endDate : Date;
		return EndDate;
	}

	public DateOnly getStartDate(){
		return _startDate;
	}
	public DateOnly getEndDate(){
		return _endDate;
	}
}

