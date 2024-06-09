using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer
{
    public class NewWindowGenerator
    {
        static public Model.BackupFileData ShowOepnNewFileDialog()
        {
            ViewModel.NewFileInputDialogViewModel viewModel = new ViewModel.NewFileInputDialogViewModel(new Model.BackupFileReader());
            View.NewFileInputDialog view = new View.NewFileInputDialog();

            view.DataContext = viewModel;
            view.ShowDialog();
            return viewModel.lastResult;
        }

        static public void ShowMemoContentWindow(Model.MemoData data)
        {
            ViewModel.MemoContentWindowViewModel viewModel = new ViewModel.MemoContentWindowViewModel(data);
            View.MemoContentWindow view = new View.MemoContentWindow();

            view.DataContext = viewModel;
            view.Show();
        }
    }
}
