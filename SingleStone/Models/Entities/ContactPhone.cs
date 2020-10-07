using System;
using System.ComponentModel.DataAnnotations.Schema;
using SingleStone.Models.Enums;

namespace SingleStone.Models.Entities
{
    public class ContactPhone
    {
        public Guid Id { get; set; }
        
        public int ContactId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        
        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }
    }
}