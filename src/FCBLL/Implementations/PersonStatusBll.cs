namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonStatusBll : IPersonStatusBll
    {
        private IPersonStatusDal dalPersonStatus;
        private IPersonStatusDal DALPersonStatus
        {
            get
            {
                if (dalPersonStatus == null)
                {
                    dalPersonStatus = DALFactory.Create<IPersonStatusDal>();
                }

                return dalPersonStatus;
            }
        }

        public PersonStatus GetPersonStatus(int id)
        {
            return DALPersonStatus.GetPersonStatus(id);
        }

        public IEnumerable<PersonStatus> GetAll()
        {
            return DALPersonStatus.GetAll();
        }

        public IEnumerable<PersonStatus> SearchByNameFull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new PersonStatus[0]; }

            return DALPersonStatus.SearchByNameFull(text);
        }
    }
}
