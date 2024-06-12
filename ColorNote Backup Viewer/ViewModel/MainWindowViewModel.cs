using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public OpenFilesMenuViewModel openFilesView { get; }
        public OpenedFileViewModelsManager memoFilesData { get; }

        public MemoViewViewModel selectedView { get { return memoFilesData.selectedView; } }
        public string fileName { get { return memoFilesData.selectedFile?.fileName; } }
        public int viewState { get; set; } // 0 : none, 1 : list, 2 : calendar

        private bool _isMenuBarOut;
        public bool isMenuBarOut { get { return _isMenuBarOut; } set { _isMenuBarOut = value; NotifyPropertyChanged(nameof(isMenuBarOut)); } }

        public ICommand CChangeViewType { get; }

        public MainWindowViewModel(OpenFilesMenuViewModel openFilesMenuVM, OpenedFileViewModelsManager memoFilesData)
        {
            this.openFilesView = openFilesMenuVM;
            this.memoFilesData = memoFilesData;
            this.memoFilesData.fileSelectionChangedHandler += selectedFileChanged;
            this.viewState = 0;
            _isMenuBarOut = false;

            this.CChangeViewType = new RelayCommand(obj => changeViewType());
        }

        private void selectedFileChanged()
        {
            if (memoFilesData.selectedFile == null)
                viewState = 0;
            else if (memoFilesData.selectedView.isCalendar)
                viewState = 2;
            else
                viewState = 1;
            NotifyPropertyChanged(nameof(fileName));
            NotifyPropertyChanged(nameof(viewState));
            NotifyPropertyChanged(nameof(selectedView));
        }

        private void changeViewType()
        {
            if (viewState == 1)
            {
                viewState = 2;
                selectedView.isCalendar = true;
                NotifyPropertyChanged(nameof(viewState));
            }
            else if (viewState == 2)
            {
                viewState = 1;
                selectedView.isCalendar = false;
                NotifyPropertyChanged(nameof(viewState));
            }
        }
    }
}
