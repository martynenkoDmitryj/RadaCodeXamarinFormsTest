using System;
using System.ComponentModel;

namespace RadacodeTestForms
{
	public class BlancDataViewModel : INotifyPropertyChanged
	{
		private string _firstName;
		private string _lastName;
		private string _country;
		private string _city;
		private string _univ;

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

				_city = value;
				OnPropertyChanged("City");
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
				_univ = value;
				OnPropertyChanged("Univ");
			}
		}
		public BlancDataViewModel()
		{
			
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
