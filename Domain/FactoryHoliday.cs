using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class FactoryHoliday
    {

        public IHoliday newHoliday(IColaborator colaborator)
        {
            return new Holiday(colaborator);
        }
    
    }
}