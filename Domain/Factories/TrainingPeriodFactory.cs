using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Factories
{
    public class TrainingPeriodFactory
    {
        public ITrainingPeriod NewTrainingPeriod(DateOnly startDate, DateOnly endDate)
        {
            return new TrainingPeriod(startDate, endDate);
        }
    }
}