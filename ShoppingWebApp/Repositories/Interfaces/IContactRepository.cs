using ShoppingWebApp.Entities;

namespace ShoppingWebApp.Repositories.Interfaces;

public interface IContactRepository
{
    Task<Contact> SendMessage(Contact contact);
    Task<Contact> Subscribe(string address);
}
