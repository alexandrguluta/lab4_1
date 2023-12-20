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
    public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant>
    {
        public void Configure(EntityTypeBuilder<Consultant> builder)
        {
            builder.HasData
            (
            new Consultant
            {
                Id = new Guid("c9d4c053-49b6-430c-bc78-2d54a9991870"),
                Name = "Andrey",
                Address = "Saransk, Nevskogo 11",
                PhoneNumber = "85882507000",
                Number = "1"
            },
            new Consultant
            {
                Id = new Guid("3d490a70-94ce-4d45-9494-5248280c2ce3"),
                Name = "Genadiy",
                Address = "Saransk, Nevskogo 113",
                PhoneNumber = "82002769076",
                Number = "2"
            }
            );
        }
    }
}
