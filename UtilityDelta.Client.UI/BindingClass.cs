using System.ComponentModel;
using System.Runtime.CompilerServices;
using UtilityDelta.Client.ServiceLocator;
using UtilityDelta.Client.UI.Annotations;

namespace UtilityDelta.Client.UI
{
    public class BindingClass : IServer, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _results;
        public bool IsAdd { get; set; }
        public bool IsSubtract { get; set; }
        public string Password { get; set; }

        public string Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged(nameof(Results));
            }
        }

        public string Server { get; set; }
        public string Username { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}