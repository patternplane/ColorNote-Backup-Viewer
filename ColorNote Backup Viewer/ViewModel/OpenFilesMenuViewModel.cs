using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class OpenFilesMenuViewModel
    {
        public ICommand CNewFileOpen { get; }
        public MemoFilesData memoFilesData { get; private set; }
        private int _selectedIdx;
        public int selectedIdx { get { return _selectedIdx; } set { _selectedIdx = value; memoFilesData.select(selectedIdx); } }

        public OpenFilesMenuViewModel(MemoFilesData memoFilesData)
        {
            this.memoFilesData = memoFilesData;
            CNewFileOpen = new RelayCommand(obj => openFile());
        }

        public void openFile()
        {
            Model.BackupFileData newFile = NewWindowGenerator.ShowOepnNewFileDialog();
            if (newFile != null)
                memoFilesData.addNewFile(newFile);
        }
    }
}
