namespace FCDAL.Implementations
{
    using FCCore.Abstractions.Dal;
    using Model;

    public class DalBase : IDalBase
    {
        protected const int LimitEntitiesSelections = 200;

        private FCDBContext context;
        protected FCDBContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new FCDBContext();
                }

                return context;
            }
        }

        public object GetContext()
        {
            return Context;
        }

        public void SetContext(object context)
        {
            this.context = (FCDBContext)context;
        }
    }
}
