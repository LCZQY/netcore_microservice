using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetcoreMicroservice
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
    }
}
