using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AppTimeRecorder
{
    class MainViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string PropertyName)
        {
            var e = new PropertyChangedEventArgs(PropertyName);
            PropertyChanged?.Invoke(this, e);
        }

        private string _BindText = "初期値";
        public string BindText { 
            get => _BindText;
            set {
                _BindText = value;
                NotifyPropertyChanged(nameof(BindText));
            }
        }

        private string _BindAppNameText = "初期値";
        public string BindAppNameText
        {
            get => _BindAppNameText;
            set
            {
                _BindAppNameText = value;
                NotifyPropertyChanged(nameof(BindAppNameText));
            }
        }
    }
}
