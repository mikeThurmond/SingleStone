using System.Collections.Generic;

namespace SingleStone.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public ContactName Name { get; set; }
        public ContactAddress Address { get; set; }
        public List<ContactPhone>  PhoneNumbers { get; set; }
        public string Email { get; set; }
    }
}