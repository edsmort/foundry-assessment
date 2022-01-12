using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace foundry_assessment
{
    public static class ApiHelper
    {
        public static HttpClient hc { get; set; }

        public static void InitializeClient()
        {
            hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:5000/");
            hc.DefaultRequestHeaders.Accept.Clear();
        }
    }
}