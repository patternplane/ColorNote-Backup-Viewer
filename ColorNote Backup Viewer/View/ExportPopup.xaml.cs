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
    /// ExportPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ExportPopup : UserControl
    {
        public ExportPopup()
        {
            InitializeComponent();

            this.SetBinding(ExportCommandProperty, new Binding("CExportMemo"));
        }

        public static readonly DependencyProperty IsShowProperty =
        DependencyProperty.Register(
            name: "isShow",
            propertyType: typeof(bool),
            ownerType: typeof(ExportPopup));

        public bool isShow
        {
            get => (bool)GetValue(IsShowProperty);
            set { SetValue(IsShowProperty, value); exportPopup.IsOpen = value; }
        }

        public static readonly DependencyProperty ExportCommandProperty =
        DependencyProperty.Register(
            name: "exportCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(ListView));

        public ICommand exportCommand
        {
            get => (ICommand)GetValue(ExportCommandProperty);
            set => SetValue(ExportCommandProperty, value);
        }

        private void EH_ExportToTextClick(object sender, RoutedEventArgs e)
        {
            exportCommand.Execute(ExportType.TXT);
        }

        private void EH_ExportToHTMLClick(object sender, RoutedEventArgs e)
        {
            exportCommand.Execute(ExportType.HTML);
        }
    }
}
