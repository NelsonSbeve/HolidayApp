using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Domain
{
    public class Project : IProject
    {
        private string _strName;
 
        private DateOnly _dateStart;
    
        private DateOnly _dateEnd;
    
        private List<IAssociate> _associations = new List<IAssociate>();  


        public Project(string _strName, DateOnly _dateStart, DateOnly _dateEnd){

            this._strName = _strName;
            this._dateStart = _dateStart;
            this._dateEnd = _dateEnd;
    
        }

  
        public List<IAssociate> AddColaborator(IColaborator colaborator, DateOnly dateStart, DateOnly dateEnd)
        {
            if (colaborator == null)
            {
                throw new ArgumentException("colaborator cant be null.");
            }

            // Create a new associate with the provided colaborator, project, dateStart, and dateEnd
            Associate newAssociate = new Associate(colaborator, dateStart, dateEnd);
            
            // Add the associate to the list of associations
            _associations.Add(newAssociate);
            return _associations;
        }
        


        public List<DateOnly> GetPeriodInsideProject(DateOnly DateStart, DateOnly DateEnd){
            
            DateOnly projectStartDate = _dateStart > DateStart ? _dateStart : DateStart;
            DateOnly projectEndDate =  _dateEnd < DateEnd ? _dateEnd : DateEnd;

            List<DateOnly> dates = [projectStartDate, projectEndDate];



            return dates;
        }
        public bool IsColaboratorInProject(IColaborator colaborator)
        {          
            
            // Iterate through the list of associations
            foreach (var associate in _associations)
            {
                // Check if the collaborator matches the one in the current association
                
                if(associate.IsColaboratorInProject(colaborator))
                return true;
            }           
            return false;
        }

        public List<IColaborator> GetColabortorInPeriod(DateOnly startDate, DateOnly endDate)
        {
            List<IColaborator> colaboradores = new List<IColaborator>();
 
            foreach(IAssociate a in _associations)
            {
               colaboradores = a.AddColaboratorIfInPeriod(colaboradores, startDate, endDate);
            }
 
            return colaboradores;
        }




    }



}