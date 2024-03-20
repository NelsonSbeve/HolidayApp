using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Domain
{
    public class Opinion : IOpinion
    {
        public DateTime Date;
        public bool Decision;
        public string Description;
        public IColaborator colaborator;    
        
        public Opinion(DateTime pDate, bool pDecision, string pDescription, IColaborator pColaborator){
        
                Date = pDate;
                Decision = pDecision;
                Description = string.IsNullOrEmpty(pDescription) ? throw new ArgumentNullException(nameof(pDescription)) : pDescription;
                colaborator = pColaborator ?? throw new ArgumentNullException(nameof(pColaborator));
        }

        // public enum DecisionType{
        //     Accepted,
        //     Denyed
        // }
            
        }
}


