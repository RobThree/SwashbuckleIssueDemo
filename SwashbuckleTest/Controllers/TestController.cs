using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwashbuckleTest.Infrastructure.ObjectId;
using SwashbuckleTest.Models;
using System.Runtime.CompilerServices;

namespace SwashbuckleTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
            => _logger = logger;


        // Argument "id" is not picked up 
        [HttpGet("testgetquerystring1")]
        public string GetQuerystring1(ObjectId id, string name)
            => BuildResult(id, name);

        // Shows "IsNew" as argument name
        [HttpGet("testgetquerystring2")]
        public string GetQuerystring2([FromQuery] ObjectId id, string name)
            => BuildResult(id, name);

        // Same as above, now with name specified but is ignored
        [HttpGet("testgetquerystring3")]
        public string GetQuerystring3([FromQuery(Name = "idalias")] ObjectId id, string name)
            => BuildResult(id, name);


        // == These work fine ==
        [HttpGet("testgetroute/{id}")]
        public string GetRoute(ObjectId id, [FromQuery] string name)
            => BuildResult(id, name);

        [HttpPost("testpost")]
        public string Post(TestDTO testdto)
            => BuildResult(testdto.Id, testdto.Name);


        private string BuildResult(ObjectId id, string name, [CallerMemberName] string caller = "")
            => $"Hello, test from [{caller}]: {id}, {name}";
    }
}