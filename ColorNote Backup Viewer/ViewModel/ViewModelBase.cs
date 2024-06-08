using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        // WPF 기술의 Binding 기법 중 INotifyPropertyChanged 인터페이스에 관한 기본 이벤트와 메서드

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
