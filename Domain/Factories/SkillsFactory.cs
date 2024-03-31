using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain
{
    public class SkillsFactory: ISkillsFactory
    {
        public ISkills NewSkills(int nivel, string descricao)
        {
            return new Skills(nivel, descricao);
        }
    }
}