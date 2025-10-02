using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using EventProRegistration.DTOs;

namespace EventProRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AiController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        : ControllerBase
    {
        private readonly string _googleApiKey = configuration["GoogleApiKey"]
                                                ?? throw new ArgumentNullException(nameof(configuration),
                                                    "Google API Key is not configured.");
