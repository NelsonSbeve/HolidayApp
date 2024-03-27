using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Training
    {
        private IColaborator _colaborator;
 
        private List<TrainingPeriod> _trainingPeriods = new List<TrainingPeriod>();
        public string Description;
 
        public Training(string description, IColaborator colab)
        {
            if(colab!=null)
                _colaborator = colab;
                else
            throw new ArgumentException("Invalid argument: colaborator must be non null");
 
            if (isValidDescription(description))
            {
                this.Description = description;
            }
            else
                throw new ArgumentException("Invalid description.");
        }
 
        private bool isValidDescription(string description)
        {

        {
            return !string.IsNullOrWhiteSpace(description);
        }
        }

        public TrainingPeriod addTrainingPeriod(DateOnly startDate, DateOnly endDate) {

        TrainingPeriod period = new TrainingPeriod(startDate, endDate);
        _trainingPeriods.Add(period);
        return period;
        }

        public int ListCount(){
            int count = 0;
            foreach(var period in _trainingPeriods){
                count++;
            }
            return count;
        }
    }
}