using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class BackupFileViewModel
    {
        public string fileName { get; set; }
        public string metaInfo { get; set; }
        public MemoViewViewModel memoViewViewModel;
        
        private string filePath;

        public BackupFileViewModel(Model.BackupFileData data)
        {
            this.filePath = data.filePath;
            this.fileName = System.IO.Path.GetFileName(filePath);

            DateTime t = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(data.backupDate).ToLocalTime();
            string date = string.Format("{0}/{1}/{2} {3}:{4}", (t.Year / 100).ToString("D2"), t.Month.ToString("D2"), t.Day.ToString("D2"), t.Hour.ToString("D2"), t.Minute.ToString("D2"));
            this.metaInfo = date + " / " + data.memoCount + "개";
        }
    }
}
