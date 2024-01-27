namespace BackendGGJ.Models;

[System.Serializable]
public enum SessionState
{
    NotCreated = 0,
    WaitingUser = 1,
    Started = 2,
    Ended = 3
}