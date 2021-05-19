using System;
using System.Collections.Generic;

namespace CosmosDb.Domain
{
    public class Book
    {
        public Book()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Isbn { get; set; }
        public decimal Price { get; set; }
        public int Edition { get; set; }
        public string Title { get; set; }
        public PublishingCompany PublishingCompany { get; set; }
        public IEnumerable<Author> Authors { get; set; }

        public override string ToString()
        {
            return $"Isbn: {Isbn} | Edition: {Edition} | Title: {Title}";
        }
    }
}
