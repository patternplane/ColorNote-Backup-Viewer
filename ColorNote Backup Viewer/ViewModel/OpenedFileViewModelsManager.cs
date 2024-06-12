using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class OpenedFileViewModelsManager
    {
        public BindingList<BackupFileViewModel> VM_OpenFilesList { get; }
        public BackupFileViewModel VM_SelectedFile { get; private set; } = null;
        public BindingList<MemoViewViewModel> VM_ViewList { get; }
        public MemoViewViewModel VM_SelectedView { get; private set; } = null;
        private int selectionIdx = -1;

        public delegate void FileSelectionChanged();
        public FileSelectionChanged EH_fileSelectionChanged;
        private Model.MemoExporter memoExporter;

        public OpenedFileViewModelsManager(Model.MemoExporter memoExporter)
        {
            this.VM_OpenFilesList = new BindingList<BackupFileViewModel>();
            this.VM_ViewList = new BindingList<MemoViewViewModel>();
            select(-1);

            this.memoExporter = memoExporter;
        }

        public void select(int idx)
        {
            if (idx != selectionIdx)
            {
                if (idx >= 0 && idx < VM_OpenFilesList.Count)
                {
                    selectionIdx = idx;
                    this.VM_SelectedFile = VM_OpenFilesList[idx];
                    this.VM_SelectedView = VM_ViewList[idx];
                    EH_fileSelectionChanged();
                }
                else
                {
                    selectionIdx = -1;
                    this.VM_SelectedFile = null;
                    this.VM_SelectedView = null;
                }
            }
        }

        public void addNewFile(Model.BackupFileData newFileData)
        {
            VM_OpenFilesList.Add(new BackupFileViewModel(newFileData));
            VM_ViewList.Add(new MemoViewViewModel(newFileData, memoExporter));
        }
    }
}
