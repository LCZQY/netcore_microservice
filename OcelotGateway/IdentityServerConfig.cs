using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotGateway
{
    /// <summary>
    /// IdentityServer4 注册使用
    /// </summary>
    public class IdentityServerConfig
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string IdentityScheme { get; set; }
        public string ResourceName { get; set; }

        public List<APIResource> Resources { get; set; }
    }

    /// <summary>
    /// 指的是Ocelot网关支持哪些服务的认证，Name就是服务的名称，同时会唯一对应一个Key。
    /// </summary>
    public class APIResource
    {
        public string Key { get; set; }
        public string Name { get; set; }
    }

    public class LeaderAdvancedDependency
    {
        public string GetOpinion()
        {
            return "good";
        }
    }
}
