using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class PersonRole
    {
        public PersonRole()
        {
            Person = new HashSet<Person>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }
        public short personRoleGroupId { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        public virtual PersonRoleGroup personRoleGroup { get; set; }
    }
}
