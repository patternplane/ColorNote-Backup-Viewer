using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class MemoViewViewModel
    {
        private Model.BackupFileData currrentFileData;
        private Model.MemoExporter memoExporter;
        public BindingList<Model.MemoData> memoListData { get; }
        public BindingList<Model.MemoData> memoCalendar { get; }
        public bool isCalendar { get; set; }

        public BindingList<SortOrderSelecterViewModel> sortOrderSelectTable { get; }

        public MemoViewViewModel(Model.BackupFileData fileData, Model.MemoExporter memoExporter)
        {
            this.currrentFileData = fileData;
            this.memoExporter = memoExporter;
            this.isCalendar = false;

            this.memoListData = fileData.memoList.getMemoList();
            this.memoCalendar = fileData.memoCalendar.getMemoCalendar();

            sortOrderSelectTable = new BindingList<SortOrderSelecterViewModel>();
            sortOrderSelectTable.Add(new SortOrderSelecterViewModel("제목", SortType.Title, reOrder, true));
            sortOrderSelectTable.Add(new SortOrderSelecterViewModel("날짜", SortType.Date, reOrder, false));
            sortOrderSelectTable.Add(new SortOrderSelecterViewModel("색", SortType.Color, reOrder, false));
        }

        private void reOrder(SortType sortOrder)
        {
            currrentFileData.memoList.sortList(memoListData, sortOrder);
        }
    }
}
