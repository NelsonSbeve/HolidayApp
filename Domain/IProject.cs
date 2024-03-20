using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IProject
    {
        DateOnly _dateStart { get; }
        DateOnly _dateEnd { get; }

        List<IAssociate> AddColaborator(IColaborator colaborator, DateOnly initialDate, DateOnly finalDate);
        List<IAssociate> GetAssociations();
    }
}