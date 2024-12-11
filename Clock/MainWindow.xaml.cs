using System.Windows;
using Widgets.Common;

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window,IWidgetWindow
    {
        public readonly static string WidgetName = "Clock";
        public readonly static string SettingsFile = "settings.clock.json";
        private readonly Config config = new(SettingsFile);

        public ClockViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ClockViewModel();
            LoadSettings();
            DataContext = ViewModel;
            ViewModel.Start();
            Logger.Info($"{WidgetName} is opened");
        }

        public void LoadSettings()
        {
            try
            {
                ClockTextBlock.FontSize = PropertyParser.ToFloat(config.GetValue("clock_font_size"));
                ClockTextBlock.Foreground = PropertyParser.ToColorBrush(config.GetValue("clock_foreground"));
                DateTextBlock.FontSize = PropertyParser.ToFloat(config.GetValue("date_font_size"));
                DateTextBlock.Foreground = PropertyParser.ToColorBrush(config.GetValue("date_foreground"));
            }
            catch (Exception)
            {
                config.Add("clock_font_size", ClockTextBlock.FontSize);
                config.Add("clock_foreground", ClockTextBlock.Foreground);
                config.Add("date_font_size", DateTextBlock.FontSize);
                config.Add("date_foreground", DateTextBlock.Foreground);
                config.Save();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ViewModel.Despose();
            Logger.Info($"{WidgetName} is closed");
        }

        public WidgetWindow WidgetWindow()
        {
            return new WidgetWindow(this, WidgetDefaultStruct());
        }

        public static WidgetDefaultStruct WidgetDefaultStruct()
        {
            return new()
            {
                Padding = new Thickness(10)
            };
        }
    }
}