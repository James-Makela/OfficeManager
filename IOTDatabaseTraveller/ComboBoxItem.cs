using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller
{
    public class ComboBoxItem
    {
        string? Name { get; set; }
        int Id { get; set; }

        public ComboBoxItem() { }

        public ComboBoxItem(string name, int id)
        {
            Name = name; 
            Id = id;
        }

        public int GetID()
        {
            return Id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
