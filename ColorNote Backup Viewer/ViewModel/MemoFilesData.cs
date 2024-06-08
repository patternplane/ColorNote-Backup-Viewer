using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class MemoFilesData
    {
        public BindingList<BackupFileViewModel> openFilesList { get; }
        public BackupFileViewModel selectedFile { get; private set; } = null;
        public BindingList<MemoViewViewModel> viewList { get; }
        public MemoViewViewModel selectedView { get; private set; } = null;
        private int selectionIdx = -1;

        public delegate void FileSelectionChanged();
        public FileSelectionChanged fileSelectionChangedHandler;
        private MemoViewViewModelFactory memoViewFactory;

        public MemoFilesData(MemoViewViewModelFactory memoViewFactory)
        {
            this.openFilesList = new BindingList<BackupFileViewModel>();
            this.viewList = new BindingList<MemoViewViewModel>();
            select(-1);

            this.memoViewFactory = memoViewFactory;
        }

        public void select(int idx)
        {
            if (idx != selectionIdx)
            {
                if (idx >= 0 && idx < openFilesList.Count)
                {
                    selectionIdx = idx;
                    this.selectedFile = openFilesList[idx];
                    this.selectedView = viewList[idx];
                    fileSelectionChangedHandler();
                }
                else
                {
                    selectionIdx = -1;
                    this.selectedFile = null;
                    this.selectedView = null;
                }
            }
        }

        public void addNewFile(Model.BackupFileData newFileData)
        {
            openFilesList.Add(new BackupFileViewModel(newFileData));
            viewList.Add(memoViewFactory.newMemoViewViewModel(newFileData));
        }
    }
}
