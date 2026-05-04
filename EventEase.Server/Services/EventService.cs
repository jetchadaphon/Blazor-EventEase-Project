using EventEase.Server.Data;
using EventEase.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Server.Services;

public class EventService : IEventService
{
    private readonly AppDbContext _ctx;
    public EventService(AppDbContext ctx) { _ctx = ctx; }

    public async Task<List<Event>> GetEventsAsync() => await _ctx.Events.OrderBy(e => e.Date).ToListAsync();

    public async Task<Event?> GetEventAsync(int id) => await _ctx.Events.FindAsync(id);

    public async Task<bool> RegisterAsync(Registration reg)
    {
        var ev = await _ctx.Events.FindAsync(reg.EventId);
        if (ev == null) return false;

        var current = await _ctx.Registrations.Where(r => r.EventId == reg.EventId).SumAsync(r => (int?)r.Tickets) ?? 0;
        if (current + reg.Tickets > ev.Capacity) return false; // capacity exceeded

        _ctx.Registrations.Add(reg);
        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetRemainingSeatsAsync(int eventId)
    {
        var ev = await _ctx.Events.FindAsync(eventId);
        if (ev == null) return 0;
        var current = await _ctx.Registrations.Where(r => r.EventId == eventId).SumAsync(r => (int?)r.Tickets) ?? 0;
        return Math.Max(0, ev.Capacity - current);
    }
}
