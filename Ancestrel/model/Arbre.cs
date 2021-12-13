using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Arbre
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public Arbre(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

    }
}
