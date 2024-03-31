using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain
{
    public class Training
    {
        // private IColaborator _colaborator;
 
        private List<TrainingPeriod> _trainingPeriods = new List<TrainingPeriod>();
        public string Description;
        private List<ISkills> _skillsPrevious;
        private List<ISkills> _skillsFinal;
 
        public Training(string description,List<ISkills> skillsPrevious, List<ISkills> skillsFinal)
        {
            // if(colab!=null)
            //     _colaborator = colab;
            //     else
            // throw new ArgumentException("Invalid argument: colaborator must be non null");
 
            if (isValidDescription(description))
            {
                this.Description = description;
            }
            else
                throw new ArgumentException("Invalid description.");

            _skillsPrevious = skillsPrevious;
            _skillsFinal = skillsFinal;
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
        public void AddFinalSkills(ISkillsFactory cFactory, int nivel, string descricao)
        {
            ISkills skills = cFactory.NewSkills(nivel, descricao);

            _skillsFinal.Add(skills);
        }

        public void AddPreviousSkills(ISkillsFactory cFactory, int nivel, string descricao)
        {
            ISkills skills = cFactory.NewSkills(nivel, descricao);

            _skillsPrevious.Add(skills);
        }
    }
}