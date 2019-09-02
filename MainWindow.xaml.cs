using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace _28082019_WPF_ExamQ3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel vm = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.MyStopwatch.Restart();
            string text = "";

            StartBtn.IsEnabled = false;
            string myUrl = UrlTxtBx.Text;

            Task.Run(() =>
            {
                using (WebClient client = new WebClient())
                {
                    text = client.DownloadString(myUrl);
                }

                int size = ASCIIEncoding.ASCII.GetByteCount(text);

                Action uiwork = () =>
                {
                    SizeTxtBlck.Text = $"Size Of Your Size Is: {size} Bytes.";
                    StartBtn.IsEnabled = true;
                };

                SafeInvoke(uiwork);
                vm.MyStopwatch.Stop();

            });
        }
        private void SafeInvoke(Action work)
        {
            if(Dispatcher.CheckAccess())
            {
                work.Invoke();
                return;
            }
            Dispatcher.Invoke(work);
        }
        private void Counter()
        {
            while(vm.MyStopwatch.IsRunning)
            {
                SafeInvoke(new Action(() => { TimeTxtBlck.Text = vm.StopWatchValue.ToString(); }));
                Thread.Sleep(10);
            }
            TimeTxtBlck.Text = $"You received the Respone At: {vm.StopWatchValue.ToString()} MilliSeconds";
        }
    }
}
