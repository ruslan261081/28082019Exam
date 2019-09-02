using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _28082019_WPF_ExamQ3
{
    public class ViewModel : INotifyPropertyChanged
    {
        public string urlStart;
        private string myUrl;
        public string MyUrl { get { return this.myUrl; } set { this.myUrl = value; OnPropertyChanged("MyUrl"); } }

        private string urlSize;
        public string UrlSize { get { return urlSize; } set { urlSize = value; OnPropertyChanged("UrlSize"); } }

        public string StopWatchValue
        {
            get
            {
                if (myStopwatch.ElapsedMilliseconds == 0) return "0";
                if (MyStopwatch.IsRunning == false && myStopwatch.ElapsedMilliseconds != 0) return $"You received The Respone At: {MyStopwatch.ElapsedMilliseconds.ToString()} MilliSeconds";
                return MyStopwatch.ElapsedMilliseconds.ToString();


            }
            set
            {
                OnPropertyChanged("StopWatchValue");
            }
        }

        private Stopwatch myStopwatch;
        public Stopwatch MyStopwatch { get { return this.myStopwatch; } set { this.myStopwatch = value; } }

        public DelegateCommand StartCommand { get; set; }
        public bool ProccessIsStart { get; set; } = true;
        public object Thrad { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            urlStart = "http";
            myUrl = "";
            urlSize = "Please Wait";
            myStopwatch = new Stopwatch();

            StartCommand = new DelegateCommand(() =>
            {
                CheckUrl(MyUrl);
            },
            () =>
            {
                return MyUrl.StartsWith(urlStart);

            });

            Task.Run(() =>
            {
                while(true)
                {
                    StartCommand.RaiseCanExecuteChanged();
                    Thread.Sleep(500);
                }

            });

            Task.Run(() =>
            {
                while (true)
                {
                    StopWatchValue = MyStopwatch.ElapsedMilliseconds.ToString();
                }
            });

        }

        public async void CheckUrl(string url)
        {
            ProccessIsStart = false;
            MyStopwatch.Restart();
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse response = await webRequest.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string sizeOfCharters = await reader.ReadToEndAsync();
                UrlSize = $"Size of Your Size Is: {sizeOfCharters.Length.ToString()} Bytes.";
            }
            ProccessIsStart = true;
            MyStopwatch.Stop();

        }

        private void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

}
