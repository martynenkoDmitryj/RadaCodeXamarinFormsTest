using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;

namespace RadacodeTestForms
{
	public partial class MainPageView : ContentPage
	{
		
		public MainPageView()
		{
			InitializeComponent();


			MainPageViewModel vm = new MainPageViewModel(this.Navigation);
			BindingContext = vm;
			MessagingCenter.Subscribe<MainPageViewModel>(this, "FillAllFields", (obj) => {
				DisplayAlert("Ошибка!", "Заполните все поля!", "Ok");
			});
		

		}
	}
}
