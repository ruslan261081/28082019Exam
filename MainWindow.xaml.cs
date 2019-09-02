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
using System.Windows.Threading;

namespace _28082019_WPF_ExamQ2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel vm = new ViewModel();
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender,EventArgs e)
        {
            if(vm.TimerCount.TimerValue == 0 || vm.GridBackground != Brushes.White)
            {
                timer.Stop();
                return;
            }
            vm.TimerCount.TimerValue = vm.TimerCount.TimerValue - 1;
        }
    }
}
