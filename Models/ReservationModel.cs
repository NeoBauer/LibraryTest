using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibraryTest.Models
{
    public class ReservationModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string OktaUserId { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        public string ReservedBookISBN { get; set; }

        public ReservationModel(string userId, string bookISBN)
        {   
            OktaUserId = userId;
            ReservedBookISBN = bookISBN;
            ReservationDate = DateTime.Now;
        }
}
}

    
