using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ColorNote_Backup_Viewer
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ViewModel.MemoFilesData memoFilesData = 
                new ViewModel.MemoFilesData(
                    new ViewModel.MemoViewViewModelFactory(
                        new Model.MemoExporter(
                            new Model.HTMLDocumentManager())));
            ViewModel.OpenFilesMenuViewModel openFileMenu = new ViewModel.OpenFilesMenuViewModel(memoFilesData);

            new MainWindow(
                new ViewModel.MainWindowViewModel(
                    openFileMenu,
                    memoFilesData))
                .Show();
        }
    }
}
