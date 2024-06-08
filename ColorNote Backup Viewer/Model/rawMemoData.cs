using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.Model
{
    public class rawMemoData
    {
        public string title { get; set; }
        public string note { get; set; }
        public int encrypted { get; set; }
        public long modified_date { get; set; }
        public int active_state { get; set; }
        public int color_index { get; set; }
        public int folder_id { get; set; }
        public long reminder_base { get; set; }
    }
}
