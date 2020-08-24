using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvantageWeb.Pages.AdvantageTool
{
    public class ResultModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public ResultModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        //public void OnGet()
        //{

        //}

        public ActionResult OnGetCheckStatus(string id)
        {
            return Content(System.IO.File.Exists(_env.WebRootPath + "/fileResults/" + id).ToString());
        }
    }
}