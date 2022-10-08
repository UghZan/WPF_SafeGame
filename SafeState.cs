using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace test_safe
{
    internal class SafeState : INotifyPropertyChanged
    {
        private bool state;
        public bool State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
