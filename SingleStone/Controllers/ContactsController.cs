using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SingleStone.Models.DTOs;
using SingleStone.Services;

namespace SingleStone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly ContactService _contactService;
        
        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contacts = await _contactService.GetContacts();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            try
            {
                var contact = await _contactService.GetContact(id);
                if (contact != null)
                {
                    return Ok(contact);
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDto newContact)
        {
            try
            {
                var contact = await _contactService.CreateContact(newContact);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }    
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] int id, [FromBody] ContactDto newContact)
        {
            try
            {
                var contact = await _contactService.UpdateContact(id, newContact);
                if (contact != null)
                {
                    return Ok(contact);
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            try
            {
                await _contactService.DeleteContact(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }
        
        
    }
}