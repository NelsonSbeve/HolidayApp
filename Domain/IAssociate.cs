using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IAssociate
    {
        IColaborator Colaborator { get; }
        DateOnly DateStart { get; }
        DateOnly DateEnd { get; }
    }
}