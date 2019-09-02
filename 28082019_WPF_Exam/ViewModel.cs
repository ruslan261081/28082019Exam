using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _28082019_WPF_Exam
{
   public class ViewModel : INotifyPropertyChanged
   {
        private double _width;
        private double _height;
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                this._width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                this._height = value;
                OnPropertyChanged("Height");
            }
        }
        private string text;
        public string MyText
        {
            get
            {
                return this.text;
            }
            set
            {
                text = value;
                OnPropertyChanged("MyTxtBx");
            }
        }

        public DelegateCommand MyCommand { get; set; }

        public ViewModel()
        {
            Width = 30;
            Height = 30;
            MyCommand = new DelegateCommand(ExecuteCommand, CanExecuteMethod);
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecuteCommand()
        {

        }
        private bool CanExecuteMethod()
        {
            return true;

        }
   }
}
