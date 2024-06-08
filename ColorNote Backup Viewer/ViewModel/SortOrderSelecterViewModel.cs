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
        private bool _isChecked = false;
        public bool isChecked { get { return _isChecked; } set { if (_isChecked != value && value) sortFunc(sortOrder); _isChecked = value; } }
        private SortType sortOrder;
        private sort sortFunc;

        public delegate void sort(SortType sortOrder);

        public SortOrderSelecterViewModel(string title, SortType sortOrder, sort sortFunc , bool isChecked)
        {
            this.sortOrder = sortOrder;
            this.sortFunc = sortFunc;
            this.title = title;
            this.isChecked = isChecked;
        }
    }
}
