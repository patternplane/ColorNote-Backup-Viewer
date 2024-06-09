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

namespace ColorNote_Backup_Viewer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModel.MainWindowViewModel dataContext)
        {
            InitializeComponent();

            this.DataContext = dataContext;

            this.SetBinding(IsMenuBarOutProperty, new Binding("isMenuBarOut") { Mode = BindingMode.TwoWay });
            this.SetBinding(ChangeViewTypeCommandProperty, new Binding("CChangeViewType"));
        }

        public static readonly DependencyProperty IsMenuBarOutProperty =
        DependencyProperty.Register(
            name: "IsMenuBarOut",
            propertyType: typeof(bool),
            ownerType: typeof(MainWindow));

        public bool IsMenuBarOut
        {
            get => (bool)GetValue(IsMenuBarOutProperty);
            set => SetValue(IsMenuBarOutProperty, value);
        }

        public static readonly DependencyProperty ChangeViewTypeCommandProperty =
        DependencyProperty.Register(
            name: "ChangeViewTypeCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(MainWindow));

        public ICommand ChangeViewTypeCommand
        {
            get => (ICommand)GetValue(ChangeViewTypeCommandProperty);
            set => SetValue(ChangeViewTypeCommandProperty, value);
        }

        private void EH_MenuBarOutButtonClick(object sender, RoutedEventArgs e)
        {
            IsMenuBarOut = true;
        }

        private void EH_MenuBarCloseButtonClick(object sender, RoutedEventArgs e)
        {
            IsMenuBarOut = false;
        }

        private void EH_ChangeViewTypeButtonClick(object sender, RoutedEventArgs e)
        {
            ChangeViewTypeCommand.Execute(null);
        }
    }
}
