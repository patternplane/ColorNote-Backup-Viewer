using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class MemoViewViewModel : ViewModelBase
    {
        public CalendarViewViewModel VM_CalendarViewViewModel { get; }
        public ListViewViewModel VM_ListViewViewModel { get; }

        private bool _isCalendar;
        public bool isCalendar { get { return _isCalendar; } set { _isCalendar = value; NotifyPropertyChanged(nameof(isCalendar)); } }

        public MemoViewViewModel(Model.BackupFileData fileData, Model.MemoExporter memoExporter)
        {
            this.VM_CalendarViewViewModel = new CalendarViewViewModel(fileData);
            this.VM_ListViewViewModel = new ListViewViewModel(fileData, memoExporter);

            this.isCalendar = false;

        }
    }
}
