using CosmosDb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB.EF.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasPartitionKey(x => x.Isbn);
            builder.Property(x => x.Id).ToJsonProperty("id");
            builder.HasKey(x => x.Id);            
            builder.OwnsOne(x => x.PublishingCompany);
            builder.OwnsMany(x => x.Authors);
            builder.HasNoDiscriminator();            
            builder.ToContainer("Books");
        }
    }
}
