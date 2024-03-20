using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Associate : IAssociate
    {
        private IColaborator colaborator;
        private IProject project;
        public DateOnly DateStart { get; private set; }
        public DateOnly DateEnd{ get; private set; }

        public Associate( IColaborator pColaborator, IProject pProject, DateOnly pDateStart, DateOnly pDateEnd ){

            colaborator = pColaborator ?? throw new ArgumentNullException(nameof(pColaborator));
            project = pProject ?? throw new ArgumentNullException(nameof(pProject));
            DateStart = pDateStart;
            DateEnd = pDateEnd;
        }

        public IColaborator Colaborator
        {
        get { return colaborator; }
        }



        public (IColaborator, IProject, DateOnly, DateOnly) GetAssociation()
        {
            return (colaborator, project, DateStart, DateEnd);
        }
    }
}