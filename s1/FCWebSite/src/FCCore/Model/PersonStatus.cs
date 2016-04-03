using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class PersonStatus
    {
        public PersonStatus()
        {
            Person = new HashSet<Person>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
