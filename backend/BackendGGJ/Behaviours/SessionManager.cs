using BackendGGJ.Extensions;
using BackendGGJ.Hubs;
using BackendGGJ.Models;
using Microsoft.AspNetCore.SignalR;

namespace BackendGGJ.Behaviours;

public sealed class SessionManager
{
    private readonly Dictionary<Guid, User> _users;
    private readonly Dictionary<string, Guid> _connectedUsers;
    private readonly IHubContext<UserHub> _userHubContext;
    private readonly IHubContext<ClientHub> _unityConnectionContext;
    private readonly ILogger<SessionManager> _logger;
    private readonly Balancer _balancer;

    private string? _clientId;
    private SessionState _sessionState;

    private SessionState SessionState
    {
        get => _sessionState;

        set => SetSession(value);
    }


    public SessionManager(
        IHubContext<UserHub> userHubContext,
        IHubContext<ClientHub> unityConnectionContext,
        ILogger<SessionManager> logger
    )
    {
        _users = new Dictionary<Guid, User>(128);
        _connectedUsers = new Dictionary<string, Guid>(128);
        _userHubContext = userHubContext;
        _unityConnectionContext = unityConnectionContext;
        _logger = logger;
        _sessionState = SessionState.NotCreated;
        _balancer = new Balancer();
    }

    public Session GetSession() =>
        new Session(_sessionState);

    public void RegisterUser(string connection, Guid guid)
    {
        if (_connectedUsers.ContainsKey(connection))
            throw new Exception("Already Registered");

        _connectedUsers[connection] = guid;
        int userTeam = _balancer.Balance(guid);
        UpdateUserTeam(guid, userTeam);
    }

    public void UnregisterUser(string connection)
    {
        if (!_connectedUsers.ContainsKey(connection))
            return;

        var guid = _connectedUsers[connection];
        _balancer.Remove(guid);
        _connectedUsers.Remove(connection);
    }

    public User GetUser(Guid guid)
    {
        if (_users.TryGetValue(guid, out var user))
            return user;

        var newUser = new User(guid);
        _users[guid] = newUser;
        return newUser;
    }

    private void UpdateUserTeam(Guid guid, int team)
    {
        var user = GetUser(guid);
        user.Team = team;
        _users[user.Id] = user;
    }
    
    public void ConnectClient(string connection)
    {
        if (!string.IsNullOrEmpty(_clientId))
            return;

        _logger.LogInformation("Client with id {conn} is CONNECTED!", connection);
        _clientId = connection;
        SessionState = SessionState.WaitingUser;
    }

    public void OnClientDisconnected(string connection)
    {
        if (!string.Equals(connection, _clientId))
            return;


        // TODO time to reconnect?
        _logger.LogInformation("Client with id {conn} is DISCONNECTED!", connection);
        _balancer.CloseSession();
        SessionState = SessionState.NotCreated;
        _clientId = null;
    }

    public bool ChangeClientSession(SessionState state, string connectionId)
    {
        if (!string.Equals(connectionId, _clientId))
            return false;

        // NotCreated -> only disconnected;
        if (state == SessionState.NotCreated)
            return false;

        // Start -> after WaitUser;
        if (state == SessionState.Started && _sessionState == SessionState.WaitingUser)
        {
            SessionState = SessionState.Started;
            return true;
        }

        // End -> after Start

        if (state == SessionState.Ended && _sessionState == SessionState.Started)
        {
            _balancer.ReBalance();
            SessionState = SessionState.Ended;
            return true;
        }

        // Wait User -> after all states
        if (state == SessionState.WaitingUser)
        {
            SessionState = SessionState.WaitingUser;
            return true;
        }

        return false;
    }

    private void SetSession(SessionState state)
    {
        if (_sessionState == state)
            return;

        _logger.LogInformation("Session was changed to {state}", state);
        _sessionState = state;
        _userHubContext.Clients.All.SendAsync(SignalUserMethod.GameState, GetSession());
    }
}