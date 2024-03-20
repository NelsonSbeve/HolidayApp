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
 
        public DateOnly _dateStart{ get; private set; }
    
        public DateOnly _dateEnd{ get; private set; }
    
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
            Associate newAssociate = new Associate(colaborator, this, dateStart, dateEnd);
            
            // Add the associate to the list of associations
            _associations.Add(newAssociate);
            return _associations;
        }
        
        public List<IAssociate> GetAssociations()
        {
            return _associations;
        }

        public DateOnly DateStart
        {
            get { return _dateStart; }
        }

        // Getter for dateEnd
        public DateOnly? DateEnd
        {
            get { return _dateEnd; }
        }


    }



}