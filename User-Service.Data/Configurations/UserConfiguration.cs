
using User_Service.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace User_Service.Data.Configurations
{
   
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            // base values
            var time = DateTime.UtcNow;

            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Surname).IsRequired();

            builder.HasData(new User
            {
                Id = Guid.Parse("9c3b5697-e4ba-4c4a-b4c9-0dacd3b634e2"),
                LastUpdatedAt = time,
                Name = "harry",
                Surname = "janssen",
                CreatedAt = time,


            });



        }
    }
}