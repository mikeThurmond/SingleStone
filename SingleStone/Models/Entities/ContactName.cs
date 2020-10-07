using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleStone.Models.Entities
{
    public class ContactName
    {
        public Guid Id { get; set; }
        
        public int ContactId { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}