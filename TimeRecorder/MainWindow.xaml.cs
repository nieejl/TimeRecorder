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

namespace TimeRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = (Application.Current as App).Services.GetService<MainWindowViewModel>();
            DataContext = vm;
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            vm.StartTimer();
        }

        private void ListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null) {
                Debug.WriteLine("sender is not a listview");
                return;
            }
            var item = listView.SelectedItem as MainWindowViewModel.Recording;
            listView.UnselectAll();
            if (item == null)
            {
                Debug.WriteLine("item is not a recording");
                return;
            }
            Debug.WriteLine("items desc is : " + item.Description);
        }

    }
}
