using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _28082019_WPF_ExamQ2
{
    public class ViewModel : INotifyPropertyChanged
    {
        public DelegateCommand YourAnswerWrong { get; set; }
        public DelegateCommand WrightAnswer { get; set; }

        public TimerCount TimerCount { get; set; }
        public Brush _timerTextColor;

        public Brush TimerTextColor
        {
            get
            {
                return _timerTextColor;
            }
            set
            {
                this._timerTextColor = value;
                OnPropertyChanged("TimerTextColor");
            }
        }

        public bool IsEnable = false;
        private Brush _gridBackground;

        public Brush GridBackground
        {
            get
            {
                return _gridBackground;
            }
            set
            {
                this._gridBackground = value;
                OnPropertyChanged("GridBackground");
            }
        }
        public ViewModel()
        {
            TimerTextColor = Brushes.Black;
            GridBackground = Brushes.White;
            TimerCount = new TimerCount();
            TimerCount.TimerValue = 30;
          
            YourAnswerWrong = new DelegateCommand(ExecuteCommand, CanExecuteCommand);
            WrightAnswer = new DelegateCommand(ExecuteCommandWrightAnswer, CanExecuteMethodWrightAnswer);

            Task.Run(() =>
            {
                while (TimerCount.TimerValue != 15)
                {
                    CanExecuteCommand();
                    Thread.Sleep(500);
                }
                while(TimerCount.TimerValue != 0)
                {
                    CanExecuteCommand();
                    Thread.Sleep(500);
                    TimerTextColor = Brushes.Red;
                }
                TimerTextColor = Brushes.Black;
                GridBackground = Brushes.Red;
            });
        }
      

        private bool CanExecuteMethodWrightAnswer()
        {
            return !IsEnable;
        }

        private void ExecuteCommandWrightAnswer()
        {
            
            IsEnable = true;
            GridBackground = Brushes.Green;
        }

        private bool CanExecuteCommand()
        {
            if(TimerCount.TimerValue == 0 || IsEnable)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ExecuteCommand()
        {
            
            IsEnable = true;
            GridBackground = Brushes.Green;
        }

        private void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

       
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
