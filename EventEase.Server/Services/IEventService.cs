using EventEase.Server.Models;

namespace EventEase.Server.Services;

public interface IEventService
{
    Task<List<Event>> GetEventsAsync();
    Task<Event?> GetEventAsync(int id);
    Task<bool> RegisterAsync(Registration reg);
}
