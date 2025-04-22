using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Domain.Core.Entities.Configs
{
    public class RedisConfiguration
    {
        public string ConnectionString { get; set; }
        public string Password { get; set; }
        public int DefaultDatabase { get; set; }
        public int ConnectTimeout { get; set; }
    }
}

