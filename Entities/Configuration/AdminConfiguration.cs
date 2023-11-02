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
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData
            (
            new Admin
            {
                Id = new Guid("1d017d9c-79b8-11ee-b962-0242ac120002"),
                Name = "name    ",
                Address = "adress",
                Number = "0908080",
                EmployeeNumber = 27272
            },
            new Admin
            {
                Id = new Guid("7aea132a-79bc-11ee-b962-0242ac120002"),
                Name = "namename",
                Address = "kllklkl",
                Number = "1221122112",
                EmployeeNumber = 27272
            }
            );;
        }
    }
}
