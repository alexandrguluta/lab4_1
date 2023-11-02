using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData
            (
            new ApplicationUser
            {
                Id = new Guid("c9dcb048-79b5-11ee-b962-0242ac120002"),
                Name = "example name1",
                Address = "example addr",
                Country = "uk",
                PhoneNumber = "9999999999"
            },
            new ApplicationUser
            {
                Id = new Guid("2efef7d8-79b6-11ee-b962-0242ac120002"),
                Name = "example name2",
                Address = "example addr",
                Country = "uk",
                PhoneNumber = "9999999999"
            }
            ); ;
        }
    }
}
