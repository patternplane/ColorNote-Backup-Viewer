using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorNote_Backup_Viewer.View
{
    /// <summary>
    /// OpenFilesMenu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OpenFilesMenu : UserControl
    {
        public OpenFilesMenu()
        {
            InitializeComponent();

            this.SetBinding(NewFileOpenCommandProperty, new Binding("CNewFileOpen"));
        }

        private void EH_NewFileOpenButtonClick(object sender, RoutedEventArgs e)
        {
            ((ViewModel.OpenFilesMenuViewModel)this.DataContext).openFile();
        }

        public static readonly DependencyProperty NewFileOpenCommandProperty =
        DependencyProperty.Register(
            name: "NewFileOpenCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(OpenFilesMenu));

        public ICommand NewFileOpenCommand
        {
            get => (ICommand)GetValue(NewFileOpenCommandProperty);
            set => SetValue(NewFileOpenCommandProperty, value);
        }
    }
}
