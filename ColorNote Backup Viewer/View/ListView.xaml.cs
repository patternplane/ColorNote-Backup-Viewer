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

        private void EH_ItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            openMemoCommand.Execute(((ListBoxItem)sender).DataContext);
        }
    }
}
