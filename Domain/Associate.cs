using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Domain
{
    public class Associate : IAssociate
    {
        private IColaborator _colaborator;
        public DateOnly DateStart;
        public DateOnly DateEnd;

        public Associate(IColaborator pColaborator, DateOnly pDateStart, DateOnly pDateEnd ){

            if (pColaborator != null)
            {
                _colaborator = pColaborator;
            }else 
                throw new ArgumentNullException();
            DateStart = pDateStart;
            DateEnd = pDateEnd;
        }

        public bool IsColaboratorInProject(IColaborator colaborator)
        {
            if (colaborator == _colaborator)
            {
                return true;
            }
            return false;
        }

        public List<IColaborator> AddColaboratorIfInPeriod(List<IColaborator> colaborators, DateOnly startDate, DateOnly endDate)
        {
            if (IsAssociateInPeriod(startDate, endDate))
            {
                IColaborator colab = _colaborator;
                colaborators.Add(colab);
            }
 
            return colaborators;
        }
 
 
        public bool IsAssociateInPeriod(DateOnly startDate, DateOnly endDate)
        {
            if (DateStart >= startDate && DateEnd <= endDate ||
            DateStart <= startDate && DateEnd >= startDate ||
            DateStart < endDate && DateEnd >= endDate)
            {
                return true;
            }
 
            return false;
        }




    }
}