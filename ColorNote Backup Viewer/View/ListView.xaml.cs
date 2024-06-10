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
    /// ListView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ListView : UserControl
    {
        public ListView()
        {
            InitializeComponent();

            this.SetBinding(OpenMemoCommandProperty, new Binding("COpenMemo"));
            this.SetBinding(ExportCommandProperty, new Binding("CExportMemo"));
        }

        public static readonly DependencyProperty OpenMemoCommandProperty =
        DependencyProperty.Register(
            name: "openMemoCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(ListView));

        public ICommand openMemoCommand
        {
            get => (ICommand)GetValue(OpenMemoCommandProperty);
            set => SetValue(OpenMemoCommandProperty, value);
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

        private void EH_ItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            openMemoCommand.Execute(((ListBoxItem)sender).DataContext);
        }

        private void EH_ExportMouseRightClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem viewItem = (ListBoxItem)sender;
            Border viewItemBorder = (Border)VisualTreeHelper.GetChild(viewItem, 0);
            ContentPresenter viewItemCP = (ContentPresenter)VisualTreeHelper.GetChild(viewItemBorder, 0);

            System.Windows.Controls.Primitives.Popup exportMenu = (System.Windows.Controls.Primitives.Popup)viewItemCP.ContentTemplate.FindName("ExportPopup", viewItemCP);

            exportMenu.IsOpen = true;
            //exportCommand.Execute(((ListBox)((ListBoxItem)sender).Parent).SelectedItems);
        }
    }
}
