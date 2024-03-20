using System.Net;

namespace Domain;

public class HolidayPeriod 
{
	public DateOnly _startDate;
	public DateOnly _endDate;

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

	public DateOnly getStartDate(){
		return _startDate;
	}
	public DateOnly getEndDate(){
		return _endDate;
	}
}

