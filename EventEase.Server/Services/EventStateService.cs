using System.Text.Json;
using Microsoft.JSInterop;

namespace EventEase.Server.Services;

public class EventStateService
{
    public int? SelectedEventId { get; set; }
    public bool IsRegistered { get; set; }

    private readonly IJSRuntime _js;
    public EventStateService(IJSRuntime js) { _js = js; }

    public async Task SaveAsync()
    {
        var data = JsonSerializer.Serialize(new { SelectedEventId, IsRegistered });
        await _js.InvokeVoidAsync("eventEase.saveState", data);
    }

    public async Task LoadAsync()
    {
        var json = await _js.InvokeAsync<string>("eventEase.loadState");
        if (string.IsNullOrEmpty(json)) return;
        try
        {
            var obj = JsonSerializer.Deserialize<JsonElement>(json);
            if (obj.TryGetProperty("SelectedEventId", out var se)) SelectedEventId = se.ValueKind == JsonValueKind.Null ? null : se.GetInt32();
            if (obj.TryGetProperty("IsRegistered", out var ir)) IsRegistered = ir.GetBoolean();
        }
        catch { }
    }
}
