using System.ComponentModel;
using Newtonsoft.Json;

namespace Bowel_Client
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string? _result;

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5000/api/values");
            var content = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<string>>(content);
            Result = string.Join(", ", values);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
