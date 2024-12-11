using System.ComponentModel;
using System.Globalization;
using Widgets.Common;

namespace Clock
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        private readonly Schedule schedule = new();
        private string scheduleID = "";
        private string _clock = "";
        private string _date = "";

        public string Clock
        {
            get => _clock;
            set
            {
                _clock = value;
                OnPropertyChanged(nameof(Clock));
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public void Start()
        {
            Timer_Tick();
            scheduleID = schedule.Secondly(Timer_Tick, 1);
        }

        private void Timer_Tick()
        {
            Clock = DateTime.Now.ToString("HH:mm:ss");
            DateTime now = DateTime.Now;

            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            Date = now.ToString("dddd, MMMM d, MM yyyy", currentCulture);
        }

        public void Despose()
        {
            schedule.Stop(scheduleID);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
