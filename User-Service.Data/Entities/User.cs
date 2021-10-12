using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Service.Data.Entities
{
    public class User : BaseEntity
    {

        //public Guid KeycloakGUID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }


    }
}

