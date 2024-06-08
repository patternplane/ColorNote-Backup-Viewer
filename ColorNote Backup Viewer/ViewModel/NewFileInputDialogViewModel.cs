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
    public class NewFileInputDialogViewModel : ViewModelBase
    {
        private Model.BackupFileReader fileReader;
        private System.Windows.Forms.OpenFileDialog FD_NewFile;
        public Model.BackupFileData lastResult { get; private set; }

        public ICommand CSetFilePath { get; }
        public ICommand COpenNewFile { get; }
        public string password { get; set; } = "";
        private string filePath;
        public string filePathDisplay { get; set; }

        public NewFileInputDialogViewModel(Model.BackupFileReader fileReader)
        {
            this.FD_NewFile = new System.Windows.Forms.OpenFileDialog();
            this.FD_NewFile.Multiselect = false;
            this.lastResult = null;
            this.fileReader = fileReader;

            this.CSetFilePath = new RelayCommand(obj => setFilePath());
            this.COpenNewFile = new RelayCommand(obj => openNewFile());
        }

        public bool openNewFile()
        {
            if (!fileReader.setFilePath(filePath))
            {
                System.Windows.MessageBox.Show(
                    "등록된 파일을 열 수 없습니다! 올바른 파일을 등록해주세요.",
                    "잘못된 파일",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
                return false;
            }

            if (!fileReader.setPassword(password))
            {
                System.Windows.MessageBox.Show(
                    "입력된 비밀번호가 올바르지 않습니다! 등록된 파일에 맞는 비밀번호를 입력해주세요.",
                    "잘못된 비밀번호",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
                return false;
            }

            lastResult = fileReader.getDataFromFile();
            NotifyPropertyChanged(nameof(lastResult));
            return true;
        }

        public void setFilePath()
        {
            while (FD_NewFile.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                try
                {
                    System.IO.File.OpenRead(FD_NewFile.FileName).Close();

                    filePath = FD_NewFile.FileName;
                    filePathDisplay = string.Format("{0} : ({1})", System.IO.Path.GetFileName(filePath), filePath);
                    NotifyPropertyChanged(nameof(filePathDisplay));

                    break;
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(
                        string.Format("파일을 열 수 없습니다! 올바른 파일을 등록해주세요 : \n({0})", e.Message),
                        "파일 열기 오류",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Error);
                }
            }
        }
    }
}
