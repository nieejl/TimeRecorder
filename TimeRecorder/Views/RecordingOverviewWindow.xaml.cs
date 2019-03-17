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
using TimeRecorder.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using TimeRecorder.ViewModels.Interfaces;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RecordingOverviewWindow : Page
    {
        private IRecordingOverviewVM vm;
        private IRecordingDetailPageVM detailVM;
        private bool secondWindowOpen;
        private Window secondWindow;

        public RecordingOverviewWindow()
        {
            InitializeComponent();
            vm = (Application.Current as App).Services.GetService<IRecordingOverviewVM>();
            detailVM = (Application.Current as App).Services.GetService<IRecordingDetailPageVM>();
            DataContext = vm;
        }

        private void Scroller_Loaded(object sender, RoutedEventArgs e)
        {
            RecordList.AddHandler(MouseWheelEvent, new RoutedEventHandler(Mouse_Wheel_Event_Handler), true);
        }

        private void Mouse_Wheel_Event_Handler(object sender, RoutedEventArgs e)
        {
            MouseWheelEventArgs wheelArgs = (MouseWheelEventArgs)e;
            if (e == null)
                return;
            double x = wheelArgs.Delta;
            double y = Scroller.VerticalOffset;
            Scroller.ScrollToVerticalOffset(y - x);
        }

        private void ListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null) {
                return;
            }
            var item = listView.SelectedItem as RecordingSummaryVM;
            listView.UnselectAll();
            if (item == null)
            {
                return;
            }
            ToggleWindow(item.Id);
        }

        private void ToggleWindow(int id)
        {
            if (!secondWindowOpen)
            {
                secondWindowOpen = true;
                SlideOut(id);
            } else
            {
                secondWindowOpen = false;
                SlideIn();
            }
        }

        private void SlideOut(int id)
        {
            secondWindow = new Window();
            var mainWindow = (Application.Current as App).MainWindow;
            secondWindow.Left = mainWindow.Left + mainWindow.Width - 8;
            secondWindow.Top = mainWindow.Top +1;
            secondWindow.Width = 250;
            secondWindow.Height = mainWindow.Height - 9;
            secondWindow.WindowStyle = WindowStyle.None;
            secondWindow.ResizeMode = ResizeMode.NoResize;
            var detailPage = new RecordDetailPage();
            detailPage.SetRecording(id);
            secondWindow.Content = detailPage;
            secondWindow.Show();
        }

        private void SlideIn()
        {
            secondWindow.Close();
        }

        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;
            object listViewItem = button.DataContext;
            if (listViewItem != null)
            {
                int index = RecordList.Items.IndexOf(listViewItem);
                vm.ContinueRecordingCommand.Execute(index);
            }
        }
    }
}
