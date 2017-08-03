using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertiseWPFApplication
{
    class myExpertiseComissionResultRow
    {
        public myExpertiseComissionResultRow(string name, double[] content)
        {
            this.name = name;
            this.content = content;
        }

        public string name { get; set; }
        public double[] content { get; set; }
    }
}
