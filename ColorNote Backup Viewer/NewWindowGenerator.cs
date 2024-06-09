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
            View.NewFileInputDialog view = new View.NewFileInputDialog(viewModel);
            view.ShowDialog();
            return viewModel.lastResult;
        }
    }
}
