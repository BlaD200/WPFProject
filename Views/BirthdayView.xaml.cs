using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFProject.ViewModels;

namespace WPFProject.Views
{
    /// <summary>
    /// Interaction logic for BirthdayView.xaml
    /// </summary>
    public partial class BirthdayView : UserControl
    {
        private BirthdayViewModel _viewModel;

        public BirthdayView()
        {
            InitializeComponent();
            DataContext = _viewModel = new BirthdayViewModel();
        }
    }
}