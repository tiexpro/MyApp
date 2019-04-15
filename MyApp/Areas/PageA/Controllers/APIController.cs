using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Areas.PageA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        [HttpPost]
        [Route("testapi")]
        public string testapi([FromBody]string data)
        {
            return $"Welcome {data}, I say hi to you from the api";
        }
    }
}