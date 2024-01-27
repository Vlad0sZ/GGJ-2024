﻿using System.Diagnostics.CodeAnalysis;

namespace BackendGGJ.Behaviours;

public class Balancer
{
    private class Team
    {
        public readonly int Id;

        private readonly HashSet<Guid> _usersGuids = new();

        public Team(int id) =>
            Id = id;

        public bool IsUserInTeam(Guid guid) => _usersGuids.Contains(guid);
        public int UserCount => _usersGuids.Count;

        // public IEnumerable<Guid> Users => _usersGuids;

        public void AddUser(Guid guid) =>
            _usersGuids.Add(guid);

        public void RemoveUser(Guid guid) =>
            _usersGuids.Remove(guid);

        public void Clear() =>
            _usersGuids.Clear();
    }

    private class TeamCompetitive
    {
        private readonly Team _teamRed;
        private readonly Team _teamBlue;
        private readonly Dictionary<Guid, int> _userActivity;

        public TeamCompetitive()
        {
            _teamRed = new Team(0);
            _teamBlue = new Team(1);
            _userActivity = new Dictionary<Guid, int>(128);
        }

        public bool CheckIsUserInTeam(Guid userId, [MaybeNullWhen(false)] out Team team)
        {
            team = null;

            if (_teamRed.IsUserInTeam(userId))
            {
                team = _teamRed;
                return true;
            }

            if (_teamBlue.IsUserInTeam(userId))
            {
                team = _teamBlue;
                return true;
            }

            return false;
        }

        public Team GetTeamForUser()
        {
            // // Баланс по силе
            // if (_teamBlue.TeamForce > _teamRed.TeamForce)
            //     return _teamRed;
            //
            // if (_teamRed.TeamForce > _teamBlue.TeamForce)
            //     return _teamBlue;

            // Тогда баланс по кол-ву
            return _teamBlue.UserCount > _teamRed.UserCount ? _teamRed : _teamBlue;
        }

        public void IncrementUserActivity(Guid userId, int activity)
        {
            var force = GetUserActivity(userId) + activity;
            _userActivity[userId] = force;
        }

        public int GetUserActivity(Guid guid) =>
            _userActivity.TryGetValue(guid, out var activity) ? activity : 0;

        // public void ReBalance()
        // {
        //     var users = _teamRed.Users.ToHashSet();
        //     users.UnionWith(_teamBlue.Users);
        //
        //     _teamBlue.Clear();
        //     _teamRed.Clear();
        //
        //     foreach (var user in users)
        //     {
        //         var team = GetTeamForUser();
        //         team.AddUser(user);
        //     }
        // }

        public void ClearStatistics()
        {
            _teamBlue.Clear();
            _teamRed.Clear();
            _userActivity.Clear();
        }
    }

    private readonly TeamCompetitive _competitive;

    public Balancer()
    {
        _competitive = new TeamCompetitive();
    }

    public int Balance(Guid guid)
    {
        if (_competitive.CheckIsUserInTeam(guid, out var team))
            return team.Id;

        var teamForUser = _competitive.GetTeamForUser();
        teamForUser.AddUser(guid);
        return teamForUser.Id;
    }

    public void Remove(Guid guid)
    {
        if (!_competitive.CheckIsUserInTeam(guid, out var team))
            return;

        team.RemoveUser(guid);
    }

    public void ReBalance() => _competitive.ClearStatistics();

    public void UserSimpleAction(Guid guid) => _competitive.IncrementUserActivity(guid, 1);

    public void UserDamageAction(Guid guid) => _competitive.IncrementUserActivity(guid, 5);

    public void CloseSession() => _competitive.ClearStatistics();
}