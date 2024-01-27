using BackendGGJ.Behaviours;
using BackendGGJ.Extensions;
using BackendGGJ.Models;
using Microsoft.AspNetCore.SignalR;

namespace BackendGGJ.Hubs;

public class UserHub : Hub
{
    private readonly SessionManager _sessionManager;

    private readonly ActionData[] actionData = new ActionData[]
    {
        new ActionData(1, 10, 1),
        new ActionData(2, 20, 2),
        new ActionData(3, 50, 4),
        new ActionData(4, 100, 15),
    };

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

        var actions = actionData.Select(t => t.CoastData).ToArray();
        await Clients.Caller.SendAsync(SignalUserMethod.Actions, actions);
    }

    public async Task GetStats(string guid)
    {
        if (!Guid.TryParse(guid, out var userId))
            return;

        _sessionManager.Balance(userId);
        var user = _sessionManager.GetUser(userId);
        await Clients.Caller.SendAsync(SignalUserMethod.Stats, user);
    }

    public async Task ClickPull(string guid, int clickCount)
    {
        if (!Guid.TryParse(guid, out var userId))
            return;

        var user = _sessionManager.UpdateUserClick(userId, clickCount);
        await Clients.Caller.SendAsync(SignalUserMethod.Stats, user);
    }

    public async Task ActionPull(string guid, int actionId)
    {
        if (!Guid.TryParse(guid, out var userId))
            return;

        // TODO logic to session manager
        var actionById = actionData.SingleOrDefault(t => t.Id == actionId);
        if (actionById.Coast == 0)
            return;

        var userStats = _sessionManager.OnUserAction(userId, actionById);
        await Clients.Caller.SendAsync(SignalUserMethod.Stats, userStats);

    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _sessionManager.UnregisterUser(ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}