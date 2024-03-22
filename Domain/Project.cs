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
                throw new ArgumentNullException(nameof(colaborator));
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

            List<DateOnly> datas = [projectStartDate, projectEndDate];



            return datas;
        }
        public bool IsColaboratorInProject(IColaborator colaborator)
        {          
            var colaboratorInProject = false;
            // Iterate through the list of associations
            foreach (var associate in _associations)
            {
                // Check if the collaborator matches the one in the current association
                colaboratorInProject = associate.IsColaboratorInProject(colaborator);
                if(true)
                break;
            }           
            return colaboratorInProject;
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

        // public int GetDaysOfHolidayFromProjectOfAll(DateOnly DateStart, DateOnly DateEnd)
        // {
        //     int totalResult = 0;
        //     List<DateOnly> datas = GetPeriodInsideProject(DateStart,DateEnd);
        //     var projectStartDate = datas[0];
        //     var projectEndDate = datas[1];
        //     List<IColaborator> colaborators = GetAssociations();
        //     foreach (IColaborator colaborator in colaborators)
        //     {
        //         List<IHoliday> holidayList = GetHolidaysOfColaborator(colaborator);

        //         foreach (var holiday in holidayList)
        //         {
        //             totalResult += holiday.GetDaysOfHolidayInsidePeriod(projectStartDate, projectEndDate);
        //         }    
        //     }
        //     return totalResult;
        // }


    }



}