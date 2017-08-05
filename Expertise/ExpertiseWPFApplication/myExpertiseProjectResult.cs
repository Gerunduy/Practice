using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertiseWPFApplication
{
    public class myExpertiseProjectResult
    {
        public myExpertiseProjectResult(string name, double rating)
        {
            this.name = name;
            this.rating = rating;
            this.status = "";       
        }

        public string name { get; set; }
        public double rating { get; set; }
        public string status { get; set; }
    }
}
