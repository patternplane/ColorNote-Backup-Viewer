using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class CalendarViewViewModel : ViewModelBase
    {
        private Model.MemoExporter memoExporter;
        private System.Windows.Forms.FolderBrowserDialog FD_OutputDir;

        public BindingList<Model.MemoData> memoCalendar { get; }

        private Dictionary<Int64, List<Model.MemoData>> memoCalendarFinder;
        private DateTime calendarMonth;
        public string calendarDate { get { return calendarMonth.Year + " / " + calendarMonth.Month; } }

        public MonthDayViewModel[] VM_MonthDays { get; }
        public MonthDayViewModel VM_SelectedDay { get; set; }

        public ICommand CGoNextMonth { get; }
        public ICommand CGoPreviousMonth { get; }
        public ICommand COpenMemo { get; }
        public ICommand CExportMemo { get; }

        public CalendarViewViewModel(Model.BackupFileData fileData, Model.MemoExporter memoExporter)
        {
            CGoNextMonth = new RelayCommand(obj => goNextMonth());
            CGoPreviousMonth = new RelayCommand(obj => goPreviousMonth());
            COpenMemo = new RelayCommand(obj => showMemo());
            CExportMemo = new RelayCommand(obj => exportMemo((ExportType)obj));

            this.FD_OutputDir = new System.Windows.Forms.FolderBrowserDialog();

            this.memoExporter = memoExporter;
            this.memoCalendar = fileData.memoCalendar.getMemoCalendar();

            this.memoCalendarFinder = new Dictionary<long, List<Model.MemoData>>();
            foreach (Model.MemoData m in memoCalendar)
            {
                if (memoCalendarFinder.ContainsKey(m.calendarDate / 1000 / 86400))
                    memoCalendarFinder[m.calendarDate / 1000 / 86400].Add(m);
                else
                {
                    List<Model.MemoData> list = new List<Model.MemoData>();
                    list.Add(m);
                    memoCalendarFinder.Add(m.calendarDate / 1000 / 86400, list);
                }
            }
            this.calendarMonth = DateTime.Now;
            this.VM_MonthDays = new MonthDayViewModel[42];
            for (int i = 0; i < 42; i++)
                VM_MonthDays[i] = new MonthDayViewModel();
            assignMonth(calendarMonth);
        }

        private void assignMonth(DateTime calendarTime)
        {
            calendarTime = new DateTime(calendarTime.Year, calendarTime.Month, 1, 0, 0, 0);
            int dayCount = DateTime.DaysInMonth(calendarTime.Year, calendarTime.Month);
            Int64 startTime = ((DateTimeOffset)calendarTime).ToUnixTimeSeconds() / 86400;
            int startDay = ((int)calendarTime.DayOfWeek == 0 ? 7 : (int)calendarTime.DayOfWeek);

            int i = 0;
            for (; i < startDay; i++)
                VM_MonthDays[i].setData(null, -1, -1, true, false);
            for (; i < startDay + dayCount; i++)
            {
                if (memoCalendarFinder.ContainsKey(startTime + i - startDay + 1))
                    VM_MonthDays[i].setData(memoCalendarFinder[startTime + i - startDay + 1], i - startDay + 1, i % 7, false, true);
                else
                    VM_MonthDays[i].setData(null, i - startDay + 1, i % 7, false, false);
            }
            for (; i < VM_MonthDays.Length; i++)
                VM_MonthDays[i].setData(null, -1, -1, true, false);
        }

        private void goNextMonth()
        {
            calendarMonth = calendarMonth.AddMonths(1);
            NotifyPropertyChanged(nameof(calendarDate));
            assignMonth(calendarMonth);
        }

        private void goPreviousMonth()
        {
            calendarMonth = calendarMonth.AddMonths(-1);
            NotifyPropertyChanged(nameof(calendarDate));
            assignMonth(calendarMonth);
        }

        private void showMemo()
        {
            if (VM_SelectedDay != null && VM_SelectedDay.data != null)
                foreach (Model.MemoData d in VM_SelectedDay.data)
                    NewWindowGenerator.ShowMemoContentWindow(d);
        }

        private void exportMemo(ExportType type)
        {
            if (FD_OutputDir.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            int fileNumber = 1;
            if (VM_SelectedDay != null)
                foreach (Model.MemoData d in VM_SelectedDay.data)
                    memoExporter.exportFile(d, FD_OutputDir.SelectedPath + "\\memo" + fileNumber++, type);
        }
    }
}
