using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Skills
    {
        private string Description;
        private int Level;


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

        public int LevelRange(int pLevel)
        {
            if(Enumerable.Range(1,5).Contains(pLevel)){
                return pLevel;
            }else{
             throw new ArgumentException("Invalid arguments.");
            }
       
        }

        public string getDescription(){
            string descript = Description;
            return descript;
        }



        

    }
}