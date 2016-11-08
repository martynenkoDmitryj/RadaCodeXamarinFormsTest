using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace RadacodeTestForms
{
	public partial class DropDownEntryView : ContentView
	{

		public string Placeholder
		{
			get
			{
				return TextBox.Placeholder;
			}
			set
			{
				TextBox.Placeholder = value;
			}
		}
		public bool IsToFind { get; set; }


		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<DropDownEntryView, List<string>>(x => x.ItemsSource, new List<string>() { "" });
		public static readonly BindableProperty TextProperty = BindableProperty.Create<DropDownEntryView, string>(x => x.Text, "");
		public List<string> ItemsSource
		{
			get
			{
				return (List<string>)GetValue(ItemsSourceProperty);
			}
			set
			{
				SetValue(ItemsSourceProperty, value);
				OnPropertyChanged("ItemsSource");

			}
		}

		public string Text
		{
			get
			{
				return (string)GetValue(TextProperty);
			}
			set
			{
				SetValue(TextProperty, value);

			}
		}

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);
			switch (propertyName)
			{
				case "ItemsSource":
					ListView.ItemsSource = this.ItemsSource;
					break;
				case "Text":
					TextBox.Text = this.Text;
				break;
			}
		}

		public DropDownEntryView()
		{

			InitializeComponent();
		
		

			ListView.ItemSelected += async(sender, e) =>
			{
				if (ListView.SelectedItem != null)
				{
					await Frame.FadeTo(0, 200, Easing.Linear);
					TextBox.Text = e.SelectedItem.ToString();
					TextBox.Unfocus();
					Frame.IsVisible = false;

				}
				ListView.SelectedItem = null;

			};
		}


		async void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			if (IsToFind == false)
			{
				var _searchSource = this.ItemsSource.Where(x => x.ToLower().Contains(TextBox.Text.ToLower())).ToList();
				ListView.ItemsSource = _searchSource;
			}
				Frame.IsVisible = true;
				Text = TextBox.Text;
				await Frame.FadeTo(1, 200, Easing.SpringIn);
			
		}

		async void Handle_UnFocused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			if (ItemsSource.Contains(TextBox.Text) == false)
			{
				TextBox.Text = "";
			}
			await Frame.FadeTo(0, 200, Easing.SinOut);
			Frame.IsVisible = false;

		}
	}
}
