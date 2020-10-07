using System.Collections.Generic;
using SingleStone.Models.Enums;

namespace SingleStone.Models.DTOs
{
    public class ContactDto
    {
        public int? Id { get; set; }
        public NameDto Name { get; set; }
        public AddressDto Address { get; set; }
        public List<PhoneDto> Phone { get; set; }
        public string Email { get; set; }
    }
    
    public class NameDto
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }
    
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    
    public class PhoneDto
    {
        public string Number { get; set; }
        
        public PhoneType Type { get; set; }
    }
    
}