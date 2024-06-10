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
    /// CalendarView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();

            this.SetBinding(GoNextMonthCommandProperty, new Binding("CGoNextMonth"));
            this.SetBinding(GoPreviousMonthCommandProperty, new Binding("CGoPreviousMonth"));
            this.SetBinding(OpenMemoCommandProperty, new Binding("COpenMemo"));
            this.SetBinding(ExportMemoCommandProperty, new Binding("CExportMemo"));
            this.SetBinding(SelectedDayProperty, new Binding("selectedDay") { Mode = BindingMode.OneWayToSource });
        }

        public static readonly DependencyProperty GoNextMonthCommandProperty =
        DependencyProperty.Register(
            name: "goNextMonthCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(CalendarView));

        public ICommand goNextMonthCommand
        {
            get => (ICommand)GetValue(GoNextMonthCommandProperty);
            set => SetValue(GoNextMonthCommandProperty, value);
        }

        public static readonly DependencyProperty GoPreviousMonthCommandProperty =
        DependencyProperty.Register(
            name: "goPreviousMonthCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(CalendarView));

        public ICommand goPreviousMonthCommand
        {
            get => (ICommand)GetValue(GoPreviousMonthCommandProperty);
            set => SetValue(GoPreviousMonthCommandProperty, value);
        }

        public static readonly DependencyProperty OpenMemoCommandProperty =
        DependencyProperty.Register(
            name: "openMemoCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(CalendarView));

        public ICommand openMemoCommand
        {
            get => (ICommand)GetValue(OpenMemoCommandProperty);
            set => SetValue(OpenMemoCommandProperty, value);
        }

        public static readonly DependencyProperty SelectedDayProperty =
        DependencyProperty.Register(
            name: "selectedDay",
            propertyType: typeof(object),
            ownerType: typeof(CalendarView));

        public object selectedDay
        {
            get => GetValue(SelectedDayProperty);
            set => SetValue(SelectedDayProperty, value);
        }

        public static readonly DependencyProperty ExportMemoCommandProperty =
        DependencyProperty.Register(
            name: "exportMemoCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(CalendarView));

        public ICommand exportMemoCommand
        {
            get => (ICommand)GetValue(ExportMemoCommandProperty);
            set => SetValue(ExportMemoCommandProperty, value);
        }

        private void EH_goNextMonthButtonClick(object sender, RoutedEventArgs e)
        {
            goNextMonthCommand.Execute(null);
        }

        private void EH_goPreviousMonthButtonClick(object sender, RoutedEventArgs e)
        {
            goPreviousMonthCommand.Execute(null);
        }

        private void EH_ItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            selectedDay = ((FrameworkElement)sender).DataContext;
            openMemoCommand.Execute(null);
        }

        private void EH_MemoRightClick(object sender, MouseButtonEventArgs e)
        {
            selectedDay = ((FrameworkElement)sender).DataContext;
            if (((string)((FrameworkElement)sender).Tag).CompareTo("hasMemo") == 0)
                exportPopup.isShow = true;
        }
    }
}
