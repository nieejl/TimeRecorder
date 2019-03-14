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
using TimeRecorder.ViewModels.Interfaces;
using TimeRecorder.Models.DTOs;
using System.Diagnostics;

namespace TimeRecorder.Views
{
    /// <summary>
    /// Interaction logic for RecordDetailPage.xaml
    /// </summary>
    public partial class RecordDetailPage : Page
    {
        IRecordingDetailPageVM vm;
        public RecordDetailPage()
        {
            InitializeComponent();
            vm = (Application.Current as App).Services.GetService<IRecordingDetailPageVM>();
            DataContext = vm;
        }

        public void SetRecording(int dtoId)
        {
            vm.UpdateFromDTO(dtoId);
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button == null)
                return;
            vm.ChooseColor((SolidColorBrush)button.Background);
        }
    }
}
