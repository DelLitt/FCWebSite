namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonRoleBll : IPersonRoleBll
    {
        private IPersonRoleDal dalPersonRole;
        private IPersonRoleDal DALPersonRole
        {
            get
            {
                if (dalPersonRole == null)
                {
                    dalPersonRole = DALFactory.Create<IPersonRoleDal>();
                }

                return dalPersonRole;
            }
        }

        public PersonRole GetPersonRole(int id)
        {
            return DALPersonRole.GetPersonRole(id);
        }

        IEnumerable<PersonRole> IPersonRoleBll.GetAll()
        {
            return DALPersonRole.GetAll();
        }

        public IEnumerable<PersonRole> SearchByNameFull(string text)
        {
            return DALPersonRole.SearchByNameFull(text);
        }
    }
}
