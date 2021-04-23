using DevCars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Brand)
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100);

            builder
                .Property(c => c.ProductionDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
