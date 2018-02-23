using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
		List<Page> myNavigationStack;

		public MainPage()
        {
            InitializeComponent();
			myNavigationStack = new List<Page>();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            var page = (MainPageDetail)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
			page.LabelPage.Text = item.Title;

            Detail = new NavigationPage(page);
			myNavigationStack.Add(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

		protected override bool OnBackButtonPressed()
		{
			if (myNavigationStack != null && myNavigationStack.Count > 1)
			{
				myNavigationStack.RemoveAt(myNavigationStack.Count - 1);
				Detail = new NavigationPage(myNavigationStack.LastOrDefault());
			}
			return true;
		}
	}
}