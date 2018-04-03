namespace FCWeb.Core.Extensions.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Model;
    using ViewModels;

    public static class EntityLinkViewModelExtensions
    {
        public static void FromPerson(this EntityLinkViewModel entityLink, Person person)
        {
            if(entityLink == null) { return; }

            entityLink.id = person.Id.ToString();
            entityLink.text = person.NameDefault();
            entityLink.title = person.NameDefault();
        }
    }
}
