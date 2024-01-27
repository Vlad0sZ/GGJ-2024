using BackendGGJ.Behaviours;
using BackendGGJ.Extensions;
using BackendGGJ.Models;
using Microsoft.AspNetCore.SignalR;

namespace BackendGGJ.Hubs;

/// <summary>
/// Эндпоинт для Unity-клиента.
/// Адрес /game
///
/// При подключении, можно менять gameState (1 - Wait User, 2 - Start, 3 - End)
/// При отключении state автоматически поменяется на 0 - NotCreated
///
/// invoke gameState
///
/// Обратные вызовы:
///     gameState (0 или 1), если 1 - то удалось изменить стейт на бэкенде
///     users (вызывается, только если state WaitUser) - изменяет кол-во подключенных пользователей
///     clickTeam (вызывается, только если state Start) - указывает за какую команду было отправлено кол-во кликов
///     actionTeam (вызывается, только если state Start) - указывает какой экшн, какой дамаг и от какой команды был нанесён
/// </summary>
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