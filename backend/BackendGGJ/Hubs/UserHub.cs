﻿using BackendGGJ.Behaviours;
using BackendGGJ.Database;
using BackendGGJ.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace BackendGGJ.Hubs;

public class UserHub : Hub
{
    private readonly SessionManager _sessionManager;
    private readonly ActionDatabase _database;

    public UserHub(SessionManager sessionManager, ActionDatabase database)
    {
        _sessionManager = sessionManager;
        _database = database;
    }

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

        var actionData = _database.GetAllData();
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
        var actionData = _database.GetAllData();
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