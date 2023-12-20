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
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasData
            (
            new Buyer
            {
                Id = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "Nikita",
                Address = "Saransk, Veselovskogo20k2",
                Country = "Russia",
                PhoneNumber = "89530309776"
            },
            new Buyer
            {
                Id = new Guid("4d490a70-94ce-4d15-9494-5248280c2ce3"),
                Name = "Stas",
                Address = "Saransk, Veselovskogo20k1",
                Country = "Russia",
                PhoneNumber = "86743643904"
            }
            ); ;
        }
    }
}
