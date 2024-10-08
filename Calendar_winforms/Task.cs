using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_winforms
{
    public class Task
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public Button Button { get; set; }

        public Task(string title, DateTime date, Button button)
        {
            Title = title;
            Date = date;
            Button = button;
        }
    }
}

