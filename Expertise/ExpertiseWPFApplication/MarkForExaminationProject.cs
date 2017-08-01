using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertiseWPFApplication
{
    public class MarkForExaminationProject
    {
        public MarkForExaminationProject(string fuzzyvalue, int numbervalue)
        {
            this.fuzzyvalue = fuzzyvalue;
            this.numbervalue = numbervalue;
        }

        public string fuzzyvalue { get; set; }
        public int numbervalue { get; set; }
    }
}
