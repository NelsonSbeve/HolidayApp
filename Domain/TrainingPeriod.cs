using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class TrainingPeriod
    {
        DateOnly _startDate;
        DateOnly _endDate;
 
        int _status;
 
        public TrainingPeriod(DateOnly startDate, DateOnly endDate)
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
}
