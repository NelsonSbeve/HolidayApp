using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITrainingPeriodFactory
    {
         public TrainingPeriod NewTrainingPeriod( DateOnly startDate, DateOnly endDate);
    }
}