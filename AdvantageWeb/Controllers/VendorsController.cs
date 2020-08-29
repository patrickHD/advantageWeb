using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdvantageAPISVC;
using AdvantageWeb.Models;
using F23.StringSimilarity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Task = System.Threading.Tasks.Task;

namespace AdvantageWeb.Controlers
{
    [ApiController]
    [Route("API/[controller]/[action]")]
    public class VendorsController : ControllerBase
    {
        private readonly StaticdbContext _tmdb;
        private readonly IConfiguration _configuration;
        private readonly string ServerName;
        private readonly string DatabaseName;
        //private readonly string DatabaseNameCA;
        private readonly string UserName;
        private readonly string Password;
        //private readonly IWebHostEnvironment _env;
        private static APIServiceClient Client;

        public VendorsController(StaticdbContext TMDB, IConfiguration configuration/*, IWebHostEnvironment env*/)
        {
            _tmdb = TMDB;
            //_env = env;
            _configuration = configuration;
            ServerName = _configuration["ServerName"];
            DatabaseName = _configuration["DatabaseName"];
            //DatabaseNameCA = _configuration["DatabaseNameCA"];
            UserName = _configuration["UserNameADV"];
            Password = _configuration["Password"];
            Client = new APIServiceClient();
        }
        public async Task<string> Update()
        {
            var l = new NormalizedLevenshtein();
            var staticVendors = _tmdb.PacingVendor.ToList().Select(i => i.Vendor).OrderBy(o=>o).ToList();
            var sDate = DateTime.Now.AddDays(-30);
            var eDate = DateTime.Now;
            var advVendors = (await Client.LoadMediaOrdersAsync(ServerName, DatabaseName, 0, UserName, Password, "A", sDate, sDate.Month, sDate.Year, eDate, eDate.Month, eDate.Year, true, false, false, false, false, false, "")).Select(i => i.VendorName).Distinct();
            var same = advVendors.Intersect(staticVendors).OrderBy(o => o).ToList();
            var res = advVendors.Where(s => !staticVendors.Any(e => 
            {
                var ts = s.Replace(".com", "").Split("/")[0].ToLower();
                var te = e.Replace(".com", "").Split("/")[0].ToLower();
                return l.Distance(ts, te) < 0.4 || ts.Contains(te) || te.Contains(ts);
            })).OrderBy(o=>o).ToList();
            var res2 = staticVendors.Where(s => res.Any(e =>
            {
                var ts = s.Replace(".com", "").Split("/")[0].ToLower();
                var te = e.Replace(".com", "").Split("/")[0].ToLower();
                return l.Distance(ts, te) < 0.4 || ts.Contains(te) || te.Contains(ts);
            })).OrderBy(o => o).ToList();
            var ao = string.Join("\n", res);
            var to = string.Join("\n", res2);
            return string.Join('\n', res);
        }

        public string Test()
        {
            return "Test Complete - " + Guid.NewGuid().ToString();
        }
    }
}
