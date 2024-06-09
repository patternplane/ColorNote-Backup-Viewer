using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class ListViewViewModel
    {
        private Model.BackupFileData currentFile;
        private Model.MemoExporter memoExporter;

        public BindingList<Model.MemoData> memoListData { get; }
        public BindingList<SortOrderSelecterViewModel> sortOrderSelectTable { get; }

        public ICommand COpenMemo { get; }

        public ListViewViewModel(Model.BackupFileData fileData, Model.MemoExporter memoExporter)
        {
            this.COpenMemo = new RelayCommand(obj => openMemo((Model.MemoData)obj));

            this.currentFile = fileData;
            this.memoExporter = memoExporter;
            this.memoListData = currentFile.memoList.getMemoList();

            this.sortOrderSelectTable = new BindingList<SortOrderSelecterViewModel>();
            sortOrderSelectTable.Add(new SortOrderSelecterViewModel("제목", SortType.Title, reOrder, true));
            sortOrderSelectTable.Add(new SortOrderSelecterViewModel("날짜", SortType.Date, reOrder, false));
            sortOrderSelectTable.Add(new SortOrderSelecterViewModel("색", SortType.Color, reOrder, false));
        }

        private void reOrder(SortType sortOrder)
        {
            currentFile.memoList.sortList(memoListData, sortOrder);
        }

        private void openMemo(Model.MemoData data)
        {
            NewWindowGenerator.ShowMemoContentWindow(data);
        }
    }
}
