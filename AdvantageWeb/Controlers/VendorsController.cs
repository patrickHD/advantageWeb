using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvantageAPISVC;
using AdvantageWeb.Models;
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
            var staticVendors = _tmdb.PacingVendor.ToList().Select(i => i.Vendor);
            var sDate = DateTime.Now.AddDays(-1);
            var eDate = DateTime.Now;
            var advVendors = (await Client.LoadMediaOrdersAsync(ServerName, DatabaseName, 0, UserName, Password, "A", sDate, sDate.Month, sDate.Year, eDate, eDate.Month, eDate.Year, true, true, true, true, true, true, "")).Select(i => i.VendorName).Distinct();
            var res = advVendors.Except(staticVendors).OrderBy(o=>o);
            return string.Join('\n', res);
        }

        public string Test()
        {
            return "Test Complete - " + Guid.NewGuid().ToString();
        }
    }
}
