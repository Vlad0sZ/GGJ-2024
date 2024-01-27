using BackendGGJ.Behaviours;
using BackendGGJ.Extensions;
using BackendGGJ.Models;
using Microsoft.AspNetCore.SignalR;

namespace BackendGGJ.Hubs;

public class UserHub : Hub
{
    private readonly SessionManager _sessionManager;

    public UserHub(SessionManager sessionManager) =>
        _sessionManager = sessionManager;

    private string ConnectionId => this.Context.ConnectionId;

    // login
    public async Task Login(string stringGuid)
    {
        if (!Guid.TryParse(stringGuid, out var guid))
            throw new Exception("Is not a GUID!");

        // register user first, if OK -
        _sessionManager.RegisterUser(ConnectionId, guid);

        // - get state for current game and return
        var state = _sessionManager.GetSession();
        await Clients.Caller.SendAsync(SignalUserMethod.GameState, state);
    }

    // getActions
    public async Task GetActions()
    {
        // TODO configs
        var actionData = new ActionData[]
        {
            new ActionData(0, 10, 1),
            new ActionData(1, 20, 2),
            new ActionData(2, 50, 4),
            new ActionData(3, 100, 15),
        };

        var actions = actionData.Select(t => t.CoastData).ToArray();
        await Clients.Caller.SendAsync(SignalUserMethod.Actions, actions);
    }

    public async Task GetStats(string guid)
    {
        if (!Guid.TryParse(guid, out var userId))
            return;

        var user = _sessionManager.GetUser(userId);
        await Clients.Caller.SendAsync(SignalUserMethod.Stats, user);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _sessionManager.UnregisterUser(ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}