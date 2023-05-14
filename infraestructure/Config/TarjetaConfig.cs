using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infraestructure.Config
{
    public class TarjetaConfig : IEntityTypeConfiguration<Tarjeta>
    {
        public void Configure(EntityTypeBuilder<Tarjeta> entityBuilder)
        {
            entityBuilder.ToTable("Tarjeta");
            entityBuilder.HasKey(e => e.TarjetaId);
        }

       
        
    }
}
