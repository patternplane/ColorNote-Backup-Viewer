using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class OpenFilesMenuViewModel : ViewModelBase
    {
        public ICommand CNewFileOpen { get; }
        public OpenedFileViewModelsManager memoFilesData { get; private set; }
        private int _selectedIdx;
        public int selectedIdx { get { return _selectedIdx; } set { _selectedIdx = value; memoFilesData.select(selectedIdx); } }
        public bool addButtonEnable { get; set; }

        public OpenFilesMenuViewModel(OpenedFileViewModelsManager memoFilesData)
        {
            this.memoFilesData = memoFilesData;
            this.addButtonEnable = true;

            CNewFileOpen = new RelayCommand(obj => openFile());
        }

        public void openFile()
        {
            Model.BackupFileData newFile = NewWindowGenerator.ShowOepnNewFileDialog();
            if (newFile != null)
            {
                memoFilesData.addNewFile(newFile);
                if (memoFilesData.openFilesList.Count >= 100)
                {
                    System.Windows.MessageBox.Show("백업 파일을 열 수 있는 최대 횟수 도달 : " + memoFilesData.openFilesList.Count + "/100)", "파일 수 최대", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    addButtonEnable = false;
                    NotifyPropertyChanged(nameof(addButtonEnable));
                }
            }
        }
    }
}
