using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleStone.Models.Entities
{
    public class ContactAddress
    {
        public Guid Id { get; set; }
        
        public int ContactId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}