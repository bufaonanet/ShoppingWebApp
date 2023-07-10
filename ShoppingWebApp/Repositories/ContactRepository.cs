using ShoppingWebApp.Data;
using ShoppingWebApp.Entities;
using ShoppingWebApp.Repositories.Interfaces;

namespace ShoppingWebApp.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ShoppingContext _dbContext;

    public ContactRepository(ShoppingContext context)
    {
        _dbContext = context;
    }

    public async Task<Contact> SendMessage(Contact contact)
    {
        _dbContext.Contacts.Add(contact);
        await _dbContext.SaveChangesAsync();
        return contact;
    }

    public async Task<Contact> Subscribe(string address)
    {
        // implement your business logic
        var newContact = new Contact();
        newContact.Email = address;
        newContact.Message = address;
        newContact.Name = address;

        _dbContext.Contacts.Add(newContact);
        await _dbContext.SaveChangesAsync();

        return newContact;
    }
}