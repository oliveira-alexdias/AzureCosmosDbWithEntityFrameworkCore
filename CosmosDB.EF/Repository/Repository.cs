using CosmosDb.Domain;
using CosmosDB.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB.EF.Repository
{
    public class BookRepository
    {
        private CosmosDbContext _db;

        public BookRepository()
        {
            _db = new CosmosDbContext();
            _db.Database.EnsureCreatedAsync();
        }

        public async Task Add(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
        }

        public Book GetByIsbn(string isbn)
        {
            return _db.Books.FirstOrDefault(c => c.Isbn == isbn);
        }

        public async Task Update(Book book)
        {            
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Book book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }
    }

}
