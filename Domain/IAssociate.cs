using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IAssociate
    {
        public bool IsColaboratorInProject(IColaborator colaborator);
        public List<IColaborator> AddColaboratorIfInPeriod(List<IColaborator> colaborators, DateOnly startDate, DateOnly endDate);
        
    }
}