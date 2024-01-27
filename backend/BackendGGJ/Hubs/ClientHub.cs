using BackendGGJ.Behaviours;
using BackendGGJ.Extensions;
using BackendGGJ.Models;
using Microsoft.AspNetCore.SignalR;

namespace BackendGGJ.Hubs;

public class ClientHub : Hub
{
    private readonly SessionManager _sessionManager;

    public ClientHub(SessionManager sessionManager) =>
        _sessionManager = sessionManager;

    private string ConnectionId => this.Context.ConnectionId;

    public override Task OnConnectedAsync()
    {
        _sessionManager.ConnectClient(this.Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _sessionManager.OnClientDisconnected(this.Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }


    public async Task GameState(int enumValue)
    {
        var state = (SessionState) enumValue;
        var isSessionChanged = _sessionManager.ChangeClientSession(state, ConnectionId);
        await this.Clients.Caller.SendAsync(SignalUserMethod.GameState, isSessionChanged ? 1 : 0);
    }
}