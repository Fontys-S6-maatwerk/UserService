﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Service.Data.Entities
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Id = Id,
                FirstName = Name,
                LastName = Surname,
            };
        }

    }
}

