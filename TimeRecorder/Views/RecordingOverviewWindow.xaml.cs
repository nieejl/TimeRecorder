﻿using System;
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

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            vm.ToggleTimerCommand.Execute(null);
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
                Debug.WriteLine("sender is not a listview");
                return;
            }
            var item = listView.SelectedItem as RecordingOverviewVM.Recording;
            listView.UnselectAll();
            if (item == null)
            {
                Debug.WriteLine("item is not a recording");
                return;
            }
            ToggleWindow();
            Debug.WriteLine("items desc is : " + item.Description);
        }

        private void ToggleWindow()
        {
            if (!secondWindowOpen)
            {
                secondWindowOpen = true;
                SlideOut();
            } else
            {
                secondWindowOpen = false;
                SlideIn();
            }
        }

        private void SlideOut()
        {
            secondWindow = new Window();
            var mainWindow = (Application.Current as App).MainWindow;
            secondWindow.Left = mainWindow.Left + mainWindow.Width - 8;
            secondWindow.Top = mainWindow.Top +1;
            secondWindow.Content = new RecordDetailPage();
            secondWindow.Width = 350;
            secondWindow.Height = mainWindow.Height - 9;
            secondWindow.WindowStyle = WindowStyle.None;
            secondWindow.ResizeMode = ResizeMode.NoResize;
            secondWindow.Show();
        }

        private void SlideIn()
        {
            secondWindow.Close();
        }
    }
}
