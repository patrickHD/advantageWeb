using System;
using Task = System.Threading.Tasks.Task;

namespace AdvantageWeb.Pages.Data
{
    public class DataTask
    {
        public Task Task { get; set; }
        public DateTime StartTime { get; }

        public DataTask(Task _task)
        {
            this.Task = _task;
            this.StartTime = DateTime.Now;
        }
    }
}