using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Skills
    {
        public string Description;
        public int Level;


        public Skills(string pDescription, int pLevel){

            Description = DescriptionVerification(pDescription);
            Level = LevelRange(pLevel);
        }

        private string DescriptionVerification(string pDescription)
        {
            if(string.IsNullOrEmpty(pDescription) || pDescription.Any(char.IsDigit) )
            {
                throw new ArgumentException("Invalid arguments.");

            }else{
                return pDescription;
            }
            
        }

        private int LevelRange(int pLevel)
        {
            if(Enumerable.Range(1,5).Contains(pLevel)){
                return pLevel;
            }else{
             throw new ArgumentException(nameof(pLevel));
            }
       
        }


        

    }
}