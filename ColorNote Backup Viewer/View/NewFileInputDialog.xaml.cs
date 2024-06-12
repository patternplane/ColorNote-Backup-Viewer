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
using System.Windows.Shapes;

namespace ColorNote_Backup_Viewer.View
{
    /// <summary>
    /// NewFileInputDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewFileInputDialog : Window
    {
        public NewFileInputDialog()
        {
            InitializeComponent();

            this.SetBinding(SetFilePathCommandProperty, new Binding("CSetFilePath"));
            this.SetBinding(OpenNewFileCommandProperty, new Binding("COpenNewFile"));
            this.SetBinding(ResultProperty, new Binding("lastResult") { Mode = BindingMode.OneWay });
        }

        public static readonly DependencyProperty SetFilePathCommandProperty =
        DependencyProperty.Register(
            name: "SetFilePathCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(NewFileInputDialog));

        public ICommand SetFilePathCommand
        {
            get => (ICommand)GetValue(SetFilePathCommandProperty);
            set => SetValue(SetFilePathCommandProperty, value);
        }

        public static readonly DependencyProperty OpenNewFileCommandProperty =
        DependencyProperty.Register(
            name: "OpenNewFileCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(NewFileInputDialog));

        public ICommand OpenNewFileCommand
        {
            get => (ICommand)GetValue(OpenNewFileCommandProperty);
            set => SetValue(OpenNewFileCommandProperty, value);
        }

        public static readonly DependencyProperty ResultProperty =
        DependencyProperty.Register(
            name: "Result",
            propertyType: typeof(Model.BackupFileData),
            ownerType: typeof(NewFileInputDialog));

        public Model.BackupFileData Result
        {
            get => (Model.BackupFileData)GetValue(ResultProperty);
            set => SetValue(ResultProperty, value);
        }

        private void EH_fileAddButtonClick(object sender, RoutedEventArgs e)
        {
            SetFilePathCommand.Execute(null);
        }

        private void EH_OKButtonClick(object sender, RoutedEventArgs e)
        {
            OpenNewFileCommand.Execute(null);
            if (Result != null)
                this.DialogResult = true;
        }

        private void EH_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
