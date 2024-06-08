using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class SortOrderSelecterViewModel
    {
        public string title { get; set; }
        private bool _isChecked;
        public bool isChecked { get { return _isChecked; } 
            set { _isChecked = value; if (value) sortFunc(sortOrder); } }
        private SortType sortOrder;
        private sort sortFunc;

        public delegate void sort(SortType sortOrder);

        public SortOrderSelecterViewModel(string title, SortType sortOrder, sort sortFunc , bool isChecked)
        {
            this.title = title;
            this._isChecked = isChecked;
            this.sortOrder = sortOrder;
            this.sortFunc = sortFunc;
        }
    }
}
