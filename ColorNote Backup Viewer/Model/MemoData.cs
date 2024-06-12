using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.Model
{
    public class MemoData
    {
        public string title { get; }
        public string text;
        public Int64 date;
        public string date_display { get; }
        public bool isCalendar;
        public Int64 calendarDate;
        public bool isDeleted;
        public int color { get; }

        public MemoData(string title, string text, long date, bool isCalendar, Int64 calendarDate, bool isDeleted, int color)
        {
            this.title = title;
            this.text = text;

            this.date = date;
            DateTime t = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(date).ToLocalTime();
            this.date_display = string.Format("{0}/{1}/{2} {3}:{4}", (t.Year % 100).ToString("D2"), t.Month.ToString("D2"), t.Day.ToString("D2"), t.Hour.ToString("D2"), t.Minute.ToString("D2"));
            this.isCalendar = isCalendar;
            this.calendarDate = calendarDate;
            this.isDeleted = isDeleted;
            this.color = color;
        }
    }
}
