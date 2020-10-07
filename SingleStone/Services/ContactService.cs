using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SingleStone.Data;
using SingleStone.Models.DTOs;
using SingleStone.Models.Entities;
using SingleStone.Models.Enums;

namespace SingleStone.Services
{
    public class ContactService
    {
        private readonly AppDbContext _context;
        
        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ContactDto> CreateContact(ContactDto contactDto)
        {
            var contact = new Contact
            {
                Email = contactDto.Email,
                Name = new ContactName
                {
                    Id = Guid.NewGuid(),
                    First = contactDto.Name.First,
                    Middle = contactDto.Name.Middle,
                    Last = contactDto.Name.Last,
                },
                Address = new ContactAddress
                {
                    Id = Guid.NewGuid(),
                    Street = contactDto.Address.Street,
                    City = contactDto.Address.City,
                    State = contactDto.Address.State,
                    Zip = contactDto.Address.Zip
                },
                PhoneNumbers = contactDto.Phone
                    .Select(number => new ContactPhone
                    {
                        Id = Guid.NewGuid(), 
                        Number = number.Number, 
                        Type = number.Type.ToString()
                    }).ToList()
            };
            
            var result = await _context.Contact.AddAsync(contact);
            await _context.SaveChangesAsync();

            contactDto.Id = result.Entity.Id;
            return contactDto;
        }
        
        public async Task DeleteContact(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            
            if (contact == null)
            {
                throw new Exception("Movie Not Found");
            }
            
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }
        
        public async Task<ContactDto> GetContact(int id)
        {
            var contact = await _context.Contact
                .Include(x => x.Name)
                .Include(x => x.Address)
                .Include(x => x.PhoneNumbers)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (contact == null) return null;
            
            var contactDto = new ContactDto
            {
                Id = contact.Id,
                Name = new NameDto
                {
                    First = contact.Name.First,
                    Middle = contact.Name.Middle,
                    Last = contact.Name.Last
                },
                Address = new AddressDto
                {
                    Street = contact.Address.Street,
                    City = contact.Address.City,
                    State = contact.Address.State,
                    Zip = contact.Address.Zip
                },
                Phone = contact.PhoneNumbers.Select(n => new PhoneDto
                {
                    Number = n.Number,
                    Type = (PhoneType) Enum.Parse(typeof(PhoneType), n.Type)
                }).ToList(),
                Email = contact.Email
            };
            
            return contactDto;
        }
        
        public async Task<List<ContactDto>> GetContacts()
        {
            var result = await _context.Contact
                .Select(x => new ContactDto
                    {
                        Id = x.Id,
                        Name = new NameDto
                        {
                            First = x.Name.First,
                            Middle = x.Name.Middle,
                            Last = x.Name.Last
                        },
                        Address = new AddressDto
                        {
                            Street = x.Address.Street,
                            City = x.Address.City,
                            State = x.Address.State,
                            Zip = x.Address.Zip
                        },
                        Phone = x.PhoneNumbers.Select(n => new PhoneDto
                        {
                            Number = n.Number,
                            Type = (PhoneType) Enum.Parse(typeof(PhoneType), n.Type, true)
                        }).ToList(),
                        Email = x.Email
                    }
                ).ToListAsync();

            return result;
        }


        public async Task<ContactDto> UpdateContact(int id, ContactDto contactDto)
        {
            var contact = await _context.Contact
                .Include(x => x.Name)
                .Include(x => x.Address)
                .Include(x => x.PhoneNumbers)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (contact == null) return null;

            contact.Email = contactDto.Email;
            contact.Name.First = contactDto.Name.First;
            contact.Name.Middle = contactDto.Name.Middle;
            contact.Name.Last = contactDto.Name.Last;
            contact.Address.Street = contactDto.Address.Street;
            contact.Address.City = contactDto.Address.City;
            contact.Address.State = contactDto.Address.State;
            contact.Address.Zip = contactDto.Address.Zip;

            await DeleteContactPhone(contact.Id);
            
            var phoneNumbers = contactDto.Phone
                .Select(number => new ContactPhone
                {
                    Id = Guid.NewGuid(),
                    ContactId = contact.Id,
                    Number = number.Number, 
                    Type = number.Type.ToString()
                }).ToList();
            
            await _context.ContactPhone.AddRangeAsync(phoneNumbers);
            await _context.SaveChangesAsync();

            contactDto.Id = id;
            return contactDto;
        }

        private async Task DeleteContactPhone(int contactId)
        {
            var contactPhones = await _context.ContactPhone
                .Where(x => x.ContactId == contactId).ToListAsync();

            _context.ContactPhone.RemoveRange(contactPhones);
            await _context.SaveChangesAsync();
        }
        
    }
}