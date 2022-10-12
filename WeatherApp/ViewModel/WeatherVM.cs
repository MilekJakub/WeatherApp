using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel;

public class WeatherVM : INotifyPropertyChanged
{
    private string _query;
    public string Query
    {
        get { return _query; }
        set
        {
            _query = value;
            OnPropertyChanged(nameof(Query));
        }
    }

    public ObservableCollection<City> Cities { get; set; }

    private CurrentConditions _currentConditions;
    public CurrentConditions CurrentConditions
    {
        get { return _currentConditions; }
        set
        {
            _currentConditions = value;
            OnPropertyChanged(nameof(CurrentConditions));
        }
    }

    private City _selectedCity;

    public City SelectedCity
    {
        get { return _selectedCity; }
        set
        {
            _selectedCity = value;
            if (SelectedCity != null)
            {
                OnPropertyChanged(nameof(SelectedCity));
                GetCurrentConditions();
            }
        }
    }

    public SearchCommand SearchCommand { get; set; }

    public WeatherVM()
    {
        if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
        {
            SelectedCity = new City
            {
                LocalizedName = "New York"
            };
            CurrentConditions = new CurrentConditions
            {
                WeatherText = "Partly cloudy",
                HasPrecipitation = true,
                Temperature = new Temperature
                {
                    Metric = new Units
                    {
                        Value = "21"
                    }
                }
            };
        }


        SearchCommand = new SearchCommand(this);
        Cities = new ObservableCollection<City>();
    }

    public async void MakeQuery()
    {
        var cities = await AccuWeatherHelper.GetCities(Query);

        Cities.Clear();

        foreach (var city in cities)
        {
            Cities.Add(city);
        }
    }

    private async void GetCurrentConditions()
    {
        Query = string.Empty;
        CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        Cities.Clear();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
