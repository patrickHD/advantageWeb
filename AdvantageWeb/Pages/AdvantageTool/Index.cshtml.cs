using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AdvantageAPISVC;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Task = System.Threading.Tasks.Task;
using System.ComponentModel.DataAnnotations;

namespace AdvantageWeb.Pages.Data
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly string ServerName;
        private readonly string DatabaseName;
        private readonly string DatabaseNameCA;
        private readonly string UserName;
        private readonly string Password;
        private readonly IWebHostEnvironment _env;
        private static APIServiceClient Client;

        public IndexModel(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
            ServerName = _configuration["ServerName"];
            DatabaseName = _configuration["DatabaseName"];
            DatabaseNameCA = _configuration["DatabaseNameCA"];
            UserName = _configuration["UserNameADV"];
            Password = _configuration["Password"];
            Client = new APIServiceClient();
        }

        public void OnGet()
        {
            GC.Collect();
        }

        [Required]
        public string OrderStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IncludeInternet { get; set; }
        public bool IncludeMagazine { get; set; }
        public bool IncludeNewspaper { get; set; }
        public bool IncludeOutOfHome { get; set; }
        public bool IncludeRadio { get; set; }
        public bool IncludeTV { get; set; }
        public string OrderNumbers { get; set; }
        public bool RemoveDuplicates { get; set; }
        public bool USData { get; set; }
        public bool CAData { get; set; }
        public MediaOrder MediaOrder { get; set; }

        public static readonly Dictionary<string, DataTask> DataTasks = new Dictionary<string, DataTask>();

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ActionResult> OnGetAdvantageData(string OrderStatus, DateTime StartDate, DateTime EndDate, bool IncludeInternet, bool IncludeMagazine,
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            bool IncludeNewspaper, bool IncludeOutOfHome, bool IncludeRadio, bool IncludeTV, string OrderNumbers, bool USData, bool CAData, string Columns, string Filters, bool RemoveDuplicates)
        {
            string filename;
            if (USData && !CAData)
            {
                filename = "USData_" + Guid.NewGuid().ToString().Split('-')[0] + ".csv";
            }
            else if (CAData && !USData)
            {
                filename = "CAData_" + Guid.NewGuid().ToString().Split('-')[0] + ".csv";
            }
            else if (CAData && USData)
            {
                filename = "USCAData_" + Guid.NewGuid().ToString().Split('-')[0] + ".zip";
            }
            else
            {
                return Content("400 - Bad Request. Make sure reqired fields are filled out correctly");
            }
            Task t = Task.Run(() => CreateData(filename, OrderStatus, StartDate, StartDate.Month, StartDate.Year, EndDate, EndDate.Month, EndDate.Year, IncludeInternet, IncludeMagazine,
            IncludeNewspaper, IncludeOutOfHome, IncludeRadio, IncludeTV, OrderNumbers, USData, CAData, Columns, Filters, RemoveDuplicates));
            DataTasks.Add(filename, new DataTask(t));
            return new RedirectResult($"/AdvantageTool/Result?id={filename}", false);
        }

        public async Task CreateData(string filename, string OrderStatus, DateTime StartDate, int StartMonth, int StartYear, DateTime EndDate, int EndMonth, int EndYear, bool IncludeInternet, bool IncludeMagazine,
            bool IncludeNewspaper, bool IncludeOutOfHome, bool IncludeRadio, bool IncludeTV, string OrderNumbers, bool USData, bool CAData, string Columns, string Filters, bool RemoveDuplicates)
        {
            byte[] usStr = null;
            byte[] caStr = null;
            if (USData)
            {
                MediaOrder[] trumd = await Client.LoadMediaOrdersAsync(ServerName, DatabaseName, 0, UserName, Password, OrderStatus, StartDate, StartMonth, StartYear, EndDate, EndMonth, EndYear, IncludeInternet, IncludeMagazine, IncludeNewspaper, IncludeOutOfHome, IncludeRadio, IncludeTV, OrderNumbers);
                if(trumd.Length == 0)
                {
                    DataTasks[filename].Extra += "No US entries found ";
                }
                else
                {
                    usStr = MakeCSV(trumd, Columns, RemoveDuplicates);
                }
            }
            if (CAData)
            {
                MediaOrder[] truca = await Client.LoadMediaOrdersAsync(ServerName, DatabaseNameCA, 0, UserName, Password, OrderStatus, StartDate, StartMonth, StartYear, EndDate, EndMonth, EndYear, IncludeInternet, IncludeMagazine, IncludeNewspaper, IncludeOutOfHome, IncludeRadio, IncludeTV, OrderNumbers);
                if(truca.Length == 0)
                {
                    DataTasks[filename].Extra += "No CA entries found ";
                }
                else
                {
                    caStr = MakeCSV(truca, Columns, RemoveDuplicates);
                }
            }
            if (USData && !CAData)
            {
                string path = Path.Combine(_env.WebRootPath + "/fileResults", filename);
                System.IO.File.WriteAllBytes(path, usStr);
            }
            else if (CAData && !USData)
            {
                string path = Path.Combine(_env.WebRootPath + "/fileResults", filename);
                System.IO.File.WriteAllBytes(path, caStr);
            }
            else if (CAData && USData)
            {
                using MemoryStream archiveStream = new MemoryStream();
                using (ZipArchive archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    using (Stream zipStream = archive.CreateEntry("USData.csv", CompressionLevel.Fastest).Open())
                    {
                        if(usStr != null)
                            zipStream.Write(usStr, 0, usStr.Length);
                    }
                    using (Stream zipStream = archive.CreateEntry("CAData.csv", CompressionLevel.Fastest).Open())
                    {
                        if(caStr != null)
                            zipStream.Write(caStr, 0, caStr.Length);
                    }
                }
                string path = Path.Combine(_env.WebRootPath + "/fileResults", filename);
                System.IO.File.WriteAllBytes(path, archiveStream.ToArray());
            }
            else
            {
                throw new Exception("400 - Bad Request. Make sure reqired fields are filled out correctly");
            }
        }

        public ActionResult OnGetCheckStatus(string id)
        {
            bool fileStatus = System.IO.File.Exists(_env.WebRootPath + "/fileResults/" + id);
            string taskStatus;
            string taskTime;
            string taskError = null;
            try
            {
                taskStatus = " | --"+ DataTasks[id].Extra + "-- | Status: " + DataTasks[id].Task.Status.ToString();
                taskTime = " | Elapsed Time: " + (DateTime.Now - DataTasks[id].StartTime).ToString().Split(".")[0];
            }
            catch (Exception e)
            {
                return Content("!!! - ERROR: Task does not exist - !!! Please re-reun query. " + e.Message);
            }
            if (DataTasks[id].Task.Status == TaskStatus.Faulted)
            {
                taskError = " | ERROR: " + DataTasks[id].Task.Exception.Message;
            }
            if (fileStatus)
            {
                DataTasks.Remove(id);
                GC.Collect();
                return Content(fileStatus.ToString());
            }
            return Content(fileStatus.ToString() + taskStatus + taskError + taskTime);
        }

        static byte[] MakeCSV(object[] items, string Columns, bool RemoveDuplicates)
        {
            string output = "";
            var delimiter = ',';
            using (var sw = new StringWriter())
            {
                var properties = items.First().GetType().GetProperties().ToList();

                if (Columns != null)
                {
                    properties = properties.Where(i => Columns.Split(',').ToList().Contains(i.Name)).ToList();
                }
                var header = properties
                .Select(n => n.Name)
                .Aggregate((a, b) => a + delimiter + b);

                sw.WriteLine(header);
                foreach (var item in items)
                {
                    var row = string.Join(",", properties.Select(n => n.GetValue(item, null)).Select(n =>
                    {
                        if (n == null)
                        {
                            return "null";
                        }
                        else
                        {
                            return n.ToString().Replace("\n", " _ ").Replace("\r", " _ ").Replace(",", ";");

                        }
                    }));
                    sw.WriteLine(row.Replace("\n", " ").Replace("\r", "_"));
                }
                if(RemoveDuplicates)
                    output = string.Join("\n", sw.ToString().Split("\n").Distinct());
                else
                    output = sw.ToString();
            }
            return Encoding.ASCII.GetBytes(output);
        }
    }
}