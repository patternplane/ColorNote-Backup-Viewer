using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.Model
{
    public class BackupFileData
    {
        public MemoListManager memoList;
        public MemoCalendarManager memoCalendar;
        public string filePath;
        public long backupDate;
        public int memoCount;

        public BackupFileData(MemoData[] rawMemoData, string filePath, long backupDate)
        {
            this.filePath = filePath;
            this.backupDate = backupDate;
            this.memoCount = rawMemoData.Length;

            List<MemoData> memoList = new List<MemoData>();
            List<MemoData> memoCalendar = new List<MemoData>();
            foreach (MemoData m in rawMemoData)
            {
                if (m.isCalendar)
                    memoCalendar.Add(m);
                else
                    memoList.Add(m);
            }
            memoList.Sort((m1, m2) => m1.date.CompareTo(m2.date));
            memoCalendar.Sort((m1, m2) => m1.calendarDate.CompareTo(m2.calendarDate));

            this.memoList = new MemoListManager(memoList.ToArray());
            this.memoCalendar = new MemoCalendarManager(memoCalendar.ToArray());
        }
    }
}
