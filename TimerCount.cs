using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _28082019_WPF_ExamQ2
{
    public class TimerCount : INotifyPropertyChanged
    {
        private int _timerCount;

        public int TimerValue
        {
            get
            {
                return this._timerCount;
            }
            set
            {
                this._timerCount = value;
                OnpropetyChanged("TimerValue");
            }
        }

        private void OnpropetyChanged(string property)
        {
           if(PropertyChanged != null)
           {
                PropertyChanged(this, new PropertyChangedEventArgs(property)); 
           }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
