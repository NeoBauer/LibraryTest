using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibraryTest.Services
{
    public interface IBookService
    {
        public void Add(BookModel book);
        public Task<IEnumerable<BookModel>> GetAllBooks();
        public void ChangeReservTrueAndUserID(string isbn, string userId);
        public void ChangeReservFalseAndUserID(string isbn, string userId);
        public string GetBookNameByISBN(string isbn);
    }

    public class BookService : IBookService
    {
        private readonly IMongoCollection<BookModel> _books;
        public BookService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<BookModel>("Books");
        }

        public void Add(BookModel book)
        {
            _books.InsertOne(book);
        }

        public async Task<IEnumerable<BookModel>> GetAllBooks()
        {
            IEnumerable<BookModel> result = await _books.Find(a => true).ToListAsync();
            return result;
        }

        public void ChangeReservTrueAndUserID(string isbn, string userId)
        {
            string param = "{$set: {IsReserved: true, ReservedByUserID:" + '"' + userId + '"' + "}}";
            _books.UpdateOne(book => book.ISBN == isbn, BsonDocument.Parse(param));
        }
        public void ChangeReservFalseAndUserID(string isbn, string userId)
        {
            string param = "{$set: {IsReserved: false, ReservedByUserID:" + '"' + " " + '"' + "}}";
            _books.UpdateOne(book => book.ISBN == isbn, BsonDocument.Parse(param));
        }

        public string GetBookNameByISBN(string isbn)
        {
            string param = "{ISBN:" + '"' + isbn + '"' + "}";
            var result = _books.Find(param).FirstOrDefault();
            return result.BookName;
        }
    }


}
