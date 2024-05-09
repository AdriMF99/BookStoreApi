using BookStoreApi.Models;
using BookStoreApi.StaticClasses;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book>? _booksCollection;

        public async Task<List<Book>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Book?> GetAsync(string autor) =>
            await _booksCollection.Find(x => x.Author == autor).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) =>
            await DBCollections.bookCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Book updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
