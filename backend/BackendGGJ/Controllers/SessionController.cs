using BackendGGJ.Behaviours;
using BackendGGJ.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendGGJ.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionController
{
    private readonly ILogger<SessionController> _logger;
    private readonly SessionManager _sessionManager;

    public SessionController(ILogger<SessionController> logger, SessionManager sessionManager)
    {
        _logger = logger;
        _sessionManager = sessionManager;
    }
    
}