using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryTest.Models
{
    public class BookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string AuthorLastName { get; set; }
        [Required]
        public string AuthorFirstName{ get; set; }
        [Required]
        [Range(1800, 2030, ErrorMessage = "Out of the range 0-2030")]
        public int ReleaseYear { get; set; }
        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }
        [Required]
        [MinLength(500)]
        [MaxLength(700)]
        public string Description { get; set; }
        public string ReservedByUserID { get; set; }
        public bool IsReserved { get; set; }

    }
}
