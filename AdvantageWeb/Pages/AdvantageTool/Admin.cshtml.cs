using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvantageWeb.Pages.AdvantageTool
{
    public class AdminModel : PageModel
    {
        public string s;
        public void OnGet()
        {
            s = string.Join(" ", Data.IndexModel.DataTasks.Select(i => i.Key + "-" + i.Value.StartTime + "-" + i.Value.Task.Status + "-" + i.Value.Task.Exception));
        }

        //public async Task OnGetStopTask(string id)
        //{
        //    //Data.IndexModel.DataTasks[id].Task.cenc
        //}
    }
}