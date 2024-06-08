using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class MemoViewViewModelFactory
    {
        private Model.MemoExporter memoExporter;

        public MemoViewViewModelFactory(Model.MemoExporter memoExporter)
        {
            this.memoExporter = memoExporter;
        }

        public MemoViewViewModel newMemoViewViewModel(Model.BackupFileData fileData)
        {
            return new MemoViewViewModel(fileData, memoExporter);
        }
    }
}
