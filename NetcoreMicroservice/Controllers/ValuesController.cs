using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace NetcoreMicroservice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return $"访问到的服务器地址是：{Configuration["ip"]},{Configuration["port"]}" + DateTime.Now;
        }


        [HttpGet("~/api/health")] //一定要有健康检查不然Consul注册失败
        public ActionResult CheckHealth()
        {
            Console.WriteLine($"{Configuration["ip"]},{Configuration["port"]},健康检查中" + DateTime.Now);
            return Ok();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
