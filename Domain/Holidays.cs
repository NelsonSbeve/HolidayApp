using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Domain
{
    public class Holidays
    {
        private List<IHoliday> holidays;
        private IFactoryHoliday _factoryHoliday;


        public Holidays(IFactoryHoliday fHoliday)
        {
            holidays = new List<IHoliday>();
            _factoryHoliday = fHoliday;
        }

        public int CountHolidays(){
            int count = 0;
            foreach(var holiday in holidays){
                count++;
            }
            return count;
        }




        public IHoliday addHoliday(IColaborator colaborator)
        {
            var holiday = _factoryHoliday.newHoliday(colaborator);
            holidays.Add(holiday);
            return holiday;
        }

 
        public List<IHoliday> GetHolidaysOfColaborator(IColaborator colaborator)
        {
            List<IHoliday> holidaysOfColaborator = new List<IHoliday>();

            foreach (var holiday in holidays)
            {
                if (holiday.IsColaboradorInHoliday(colaborator))
                {
                    holidaysOfColaborator.Add(holiday);
                }
            }

            return holidaysOfColaborator;
        }


        //need fix
        public int GetDaysOfHolidayFromProjectOfColaborator(DateOnly DateStart, DateOnly DateEnd, IColaborator colaborator, IProject project)
        {
            int result = 0;
            List<DateOnly> datas = project.GetPeriodInsideProject(DateStart,DateEnd);
            if (datas != null && datas.Count >= 2)
            {
                var projectStartDate = datas[0];
                var projectEndDate = datas[1];

                bool isInProject = project.IsColaboratorInProject(colaborator);
                
                if (isInProject)
                {
                    
                    foreach (var holiday in holidays)
                    {  
                        if(holiday.IsColaboradorInHoliday(colaborator)){

                            result += holiday.GetDaysOfHolidayInsidePeriod(projectStartDate,projectEndDate);

                        }
                    }  
                    
                }
            }
            return result;
        }
        public int  GetDaysOfHolidayFromProjectOfAll(DateOnly startDate, DateOnly endDate, IProject project)
        {
            int result = 0;
 
            List<IColaborator> colaborators = project.GetColabortorInPeriod(startDate, endDate);
 
            foreach (IColaborator colaborator in colaborators)
            {
                result += GetDaysOfHolidayFromProjectOfColaborator(startDate, endDate, colaborator, project);
            }
           
            return result;
        }



        public List<IColaborator> GetColaboratorsWithMoreThen(int XDays){
            List<IColaborator> colaboratorsWithMoreThanXDays = new List<IColaborator>();
            foreach(var holiday in holidays)
            {
                var colab= holiday.GetColaboratorwithMoreThen(XDays);
                if(colab != null){
                colaboratorsWithMoreThanXDays.Add(colab);
                }
            }
            return colaboratorsWithMoreThanXDays;
        }

        public List<HolidayPeriod>GetPeriodsOfHolidaysOfColaboratorInPeriod(IColaborator colaborator, DateOnly startDate, DateOnly endDate){

            List<HolidayPeriod> holidayPeriods = [];

            List<IHoliday> holidayList = GetHolidaysOfColaborator(colaborator);
            foreach(var holiday in holidayList){
                List<HolidayPeriod> allHolidayPeriods = holiday.GetHolidayPeriod(startDate,startDate );

                // Filter holiday periods that fall within the specified time period
                foreach (var period in allHolidayPeriods)
                {
                    if (period._startDate >= startDate && period._endDate <= endDate)
                    {
                        holidayPeriods.Add(period);
                    }
                }
            }

            return holidayPeriods;

        }

    }


}