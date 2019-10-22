using Consul;
using RestTemplateCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SelectConsul
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                RestTemplate rest = new RestTemplate(httpClient, "http://127.0.0.1:8500");

                Console.WriteLine("---querying---------");
                var headers = new HttpRequestMessage().Headers;
                headers.Add("aa", "666");
                var ret1 = rest.GetForEntityAsync<Product[]>("http://ProductService/api/Product/", headers).Result;
                Console.WriteLine(ret1.StatusCode);
                if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (var p in ret1.Body)
                    {
                        Console.WriteLine($"id={p.Id},name={p.Name}");
                    }
                }

                Console.WriteLine("---add new---------");
                Product newP = new Product();
                newP.Id = 888;
                newP.Name = "xinzeng";
                newP.Price = 88.8;
                var ret = rest.PostAsync("http://ProductService/api/Product/", newP).Result;
                Console.WriteLine(ret.StatusCode);
            }
            Console.ReadKey();

            ////?????  Consul 对刚启动的服务可以发现，但是过一段时间后就不见了？？？？
            //using (ConsulClient consulClient = new ConsulClient(c => c.Address = new Uri("http://127.0.0.1:8500")))
            //{
            //    //consulClient.Agent.Services()获取consul中注册的所有的服务
            //    Dictionary<String, AgentService> services = consulClient.Agent.Services().Result.Response;
            //    foreach (KeyValuePair<String, AgentService> kv in services)
            //    {
            //        Console.WriteLine($"所有的服务：key={kv.Key},{kv.Value.Address},{kv.Value.ID},{kv.Value.Service},{kv.Value.Port}");
            //    }


            //    //获取所有服务名字是"MicroService1"所有的服务
            //    var agentServices = services.Where(s => s.Value.Service.Equals("MicroService1", StringComparison.CurrentCultureIgnoreCase))
            //       .Select(s => s.Value);

            //    //根据当前TickCount对服务器个数取模，“随机”取一个机器出来，避免“轮询”的负载均衡策略需要计数加锁问题
            //    var agentService = agentServices.ElementAt(Environment.TickCount % agentServices.Count());
            //    Console.WriteLine($"随机取出：{agentService.Address},{agentService.ID},{agentService.Service},{agentService.Port}");
            //}

            //Console.ReadKey();
        }
    }
}
