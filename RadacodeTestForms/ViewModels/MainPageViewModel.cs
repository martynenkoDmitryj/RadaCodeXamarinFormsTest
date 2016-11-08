using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RadacodeTestForms
{
	public class MainPageViewModel:INotifyPropertyChanged
	{
		VKClient vk;
		private List<string> _countrySource;
		private List<string> _citySource;
		private List<string> _univSource;

		private string _firstName;
		private string _lastName;
		private string _country;
		private string _city;
		private string _univ;

		private bool _isFirstNameEnabled;
		private bool _isLastNameEnabled;
		private bool _isCountryEnabled; 
		private bool _isCityEnabled;
		private bool _isUnivEnabled;
		private INavigation _navigation;
		public ICommand Fill
		{
			get
			{
				return new Command(async() =>
				{
					if (FirstName != "" && LastName != "" &&
					   Country != "" && City != "" && Univ != "" &&
					    FirstName != null && LastName != null && Country != null && 
					    City != null && Univ != null)

						await _navigation.PushAsync(new BlancDataPageView() { BindingContext = new BlancDataViewModel() { FirstName = this.FirstName, LastName = this.LastName, Country = this.Country, City = this.City, Univ = this.Univ } });
					else {
						MessagingCenter.Send<MainPageViewModel>(this, "FillAllFields");
					}
				});
			}
		}

		public List<string> CountrySource
		{
			get
			{
				return _countrySource;
			}

			set
			{
				_countrySource = value;
				OnPropertyChanged("CountrySource");
			}
		}

		public List<string> CitySource
		{
			get
			{
				return _citySource;
			}

			set
			{
				_citySource = value;
				OnPropertyChanged("CitySource");
			}
		}

		public List<string> UnivSource
		{
			get
			{
				return _univSource;
			}

			set
			{
				_univSource = value;
				OnPropertyChanged("UnivSource");
			}
		}

		public bool IsFirstNameEnabled
		{
			get
			{
				return _isFirstNameEnabled;
			}

			set
			{
				_isFirstNameEnabled = value;
				OnPropertyChanged("IsFirstNameEnabled");
			}
		}

		public bool IsLastNameEnabled
		{
			get
			{
				return _isLastNameEnabled;
			}

			set
			{
				_isLastNameEnabled = value;
				OnPropertyChanged("IsLastNameEnabled");

			}
		}
	
		public bool IsCountryEnabled
		{
			get
			{
				return _isCountryEnabled;
			}

			set
			{
				_isCountryEnabled = value;
				OnPropertyChanged("IsCountryEnabled");

			}
		}


		public bool IsCityEnabled
		{
			get
			{
				return _isCityEnabled;
			}

			set
			{
				_isCityEnabled = value;
				OnPropertyChanged("IsCityEnabled");

			}
		}

		public bool IsUnivEnabled
		{
			get
			{
				return _isUnivEnabled;
			}

			set
			{
				_isUnivEnabled = value;
				OnPropertyChanged("IsUnivEnabled");

			}
		}



		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				_firstName = value;
				OnPropertyChanged("Name");
				if (value != "" && value != null)
					IsLastNameEnabled = true;
			}
		}

		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				_lastName = value;
				OnPropertyChanged("LastName");
				if (value != "" && value != null)
					IsCountryEnabled = true;
			}
		}

		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				_country = value;
				OnPropertyChanged("Country");
				if (value != "" && value != null)
					IsCityEnabled = true;
			}
		}

		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				LoadCitiesAsync(Country, value);

				_city = value;
				OnPropertyChanged("City");
				if (value != "" && value != null)
					IsUnivEnabled = true;
			}
		}

		public string Univ
		{
			get
			{
				return _univ;
			}
			set
			{
				LoadUnivsAsync(Country, City, value);
				_univ = value;
				OnPropertyChanged("Univ");
			}
		}





		public MainPageViewModel(INavigation navigation)
		{
			_navigation = navigation;
			IsFirstNameEnabled = true;
			IsLastNameEnabled = false;
			IsCountryEnabled = false;
			IsCityEnabled = false;
			IsUnivEnabled = false;
			vk = new VKClient();
			LoadCountriesAsync();

		}
		public async void LoadCountriesAsync()
		{
			await vk.LoadCountries();
			List<string> items = new List<string>();
			foreach (var item in vk.Countries)
			{
				items.Add(item.Value);
			}
			CountrySource = items;

		}
		public async void LoadCitiesAsync(string countryName, string toFind)
		{
			await vk.LoadCities(countryName, toFind);
			List<string> items = new List<string>();
			foreach (var item in vk.Cities)
			{
				items.Add(item.Value);
			}
			CitySource = items;

		}
		public async void LoadUnivsAsync(string country, string city, string toFind)
		{
			await vk.LoadUniversities(country, city, toFind);
			List<string> items = new List<string>();
			foreach (var item in vk.Universities)
			{
				items.Add(item.Value);
			}
			UnivSource = items;

		}
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
