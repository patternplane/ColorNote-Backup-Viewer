using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class MemoContentWindowViewModel
    {
        public string title { get; }
        public string content { get; }
        //public bool isReadOnly { get; }

        public MemoContentWindowViewModel(Model.MemoData data)
        {
            this.title = data.title;
            this.content = data.text;
            //this.isReadOnly = false;
        }
    }
}
