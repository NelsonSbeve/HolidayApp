using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IProject
    {
        public List<DateOnly> GetPeriodInsideProject(DateOnly DateStart, DateOnly DateEnd);
        public bool IsColaboratorInProject(IColaborator colaborator);

        List<IAssociate> AddColaborator(IColaborator colaborator, DateOnly initialDate, DateOnly finalDate);
        public List<IColaborator> GetColabortorInPeriod(DateOnly startDate, DateOnly endDate);
 
    }
}