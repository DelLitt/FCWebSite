using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class PersonRoleGroup
    {
        public PersonRoleGroup()
        {
            PersonRole = new HashSet<PersonRole>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<PersonRole> PersonRole { get; set; }
    }
}
