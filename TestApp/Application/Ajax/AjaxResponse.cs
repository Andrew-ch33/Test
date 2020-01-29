using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Ajax
{
    public class LowercaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLowerInvariant();
        }
    }

    public enum AjaxResultCode
    {
        Ok = 1,
        Error = -1,
    }

    public class AjaxResult
    {
        public int Code { get; set; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }
    }

    public class AjaxResponse
    {
        public JsonResult ToJson()
        {
            return new JsonResult(data, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new LowercaseNamingStrategy() }, });
        }

        private object data { get; set; }

        public static AjaxResponse Create(object obj)
        {
            var result = new AjaxResponse()
            {
                data = obj
            };

            return result;
        }

    }
}



