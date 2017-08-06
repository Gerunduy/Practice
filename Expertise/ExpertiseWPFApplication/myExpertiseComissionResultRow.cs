using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertiseWPFApplication
{
    class myExpertiseComissionResultRow
    {
        public myExpertiseComissionResultRow(int id_expert, string name, double[] content)
        {
            this.id_expert = id_expert;
            this.name = name;
            this.content = content;
        }

        public int id_expert { get; set; }
        public string name { get; set; }
        public double[] content { get; set; }
    }
}
