using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertiseWPFApplication
{
    public class myCompareRow
    {
        public myCompareRow(string name, double[] content)
        {
            this.name = name;
            this.content = content;
            double s = 0;
            foreach (double d in content)
            {
                s = s + d;
            }
            this.ownvectorcomponent = Math.Pow(s, 1 / content.Count());
        }

        public string name { get; set; }
        public double[] content { get; set; }
        public double ownvectorcomponent { get; set; }
        public double ownvector { get; set; }
    }
}
