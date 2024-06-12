using System;
using System.Collections;
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
        private System.Windows.Forms.FolderBrowserDialog FD_OutputDir;

        public BindingList<Model.MemoData> memoListData { get; }
        public BindingList<SortOrderSelecterViewModel> VM_SortOrderSelectTable { get; }

        public IList selectedItems { get; set; }

        public ICommand COpenMemo { get; }
        public ICommand CExportMemo { get; }

        public ListViewViewModel(Model.BackupFileData fileData, Model.MemoExporter memoExporter)
        {
            this.COpenMemo = new RelayCommand(obj => openMemo((Model.MemoData)obj));
            this.CExportMemo = new RelayCommand(obj => exportMemo((ExportType)obj));

            this.currentFile = fileData;
            this.memoExporter = memoExporter;
            this.FD_OutputDir = new System.Windows.Forms.FolderBrowserDialog();

            this.memoListData = currentFile.memoList.getMemoList();

            this.VM_SortOrderSelectTable = new BindingList<SortOrderSelecterViewModel>();
            VM_SortOrderSelectTable.Add(new SortOrderSelecterViewModel("제목", SortType.Title, reOrder, true));
            VM_SortOrderSelectTable.Add(new SortOrderSelecterViewModel("날짜", SortType.Date, reOrder, false));
            VM_SortOrderSelectTable.Add(new SortOrderSelecterViewModel("색", SortType.Color, reOrder, false));
        }

        private void reOrder(SortType sortOrder)
        {
            currentFile.memoList.sortList(memoListData, sortOrder);
        }

        private void openMemo(Model.MemoData data)
        {
            NewWindowGenerator.ShowMemoContentWindow(data);
        }

        private void exportMemo(ExportType type)
        {
            if (FD_OutputDir.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            int fileNumber = 1;
            if (selectedItems != null)
                foreach(Model.MemoData d in selectedItems)
                    memoExporter.exportFile(d, FD_OutputDir.SelectedPath + "\\memo" + fileNumber++, type);
        }
    }
}
