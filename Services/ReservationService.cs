using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryTest.Models;

namespace LibraryTest.Services
{
    public interface IReservationService
    {
        void CreateNewReservation(string isbn, string userId);
     }

    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<ReservationModel> _reservations;
        private readonly IBookService _bookService;

        public ReservationService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _reservations = database.GetCollection<ReservationModel>("Reservations");
            _bookService = new BookService(settings);
        }

        public void CreateNewReservation(string isbn, string userId)
        {
             ReservationModel model = new ReservationModel(userId, isbn);
             _reservations.InsertOne(model);
            _bookService.ChangeReservTrueAndUserID(isbn, userId);
        }
        public async Task<IEnumerable<ReservationModel>> GetAllReservationForUser(string userId)
        {
            IEnumerable<ReservationModel> result = await _reservations.Find(reserv => reserv.OktaUserId == userId).ToListAsync();
            return result;
        }

        public string GetBookNameByISBN(string isbn)
        {
            string result  = _bookService.GetBookNameByISBN(isbn);
            return result;
        }

        public void DeleteReservation(string isbn, string userId)
        {
            _reservations.DeleteOne(reserv => reserv.OktaUserId == userId && reserv.ReservedBookISBN == isbn);
            _bookService.ChangeReservFalseAndUserID(isbn, userId);
        }
    }
}
