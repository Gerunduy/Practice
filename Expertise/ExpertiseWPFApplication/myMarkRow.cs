using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertiseWPFApplication
{
    public class myMarkRow
    {
        public myMarkRow(string name, int[] content)
        {
            this.name = name;
            this.content = content;
        }

        public string name { get; set; }
        public int[] content { get; set; }
    }
}
