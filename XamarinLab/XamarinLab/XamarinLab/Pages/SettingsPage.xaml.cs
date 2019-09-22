using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLab.Models;

namespace XamarinLab.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            if (AppState.IsDarkTheme)
            {
                BackgroundColor = Color.FromHex("#595959");
            }
            else
            {
                BackgroundColor = Color.FromHex("#fbf6d9");
            }
        }

        private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
	     {
	          var realmDb = Realm.GetInstance();
	          var settings = realmDb.All<Setting>().FirstOrDefault(b => b.SettingId == AppState.SettingId);

               if (AppState.IsDarkTheme)
	          {
	               if (settings == null)
	               {
	                    realmDb.Write(() =>
	                    {
	                         realmDb.Add(new Setting { SettingId = AppState.SettingId, IsDarkTheme = false});
	                    });
	               }
	               else
	               {
	                    using (var db = realmDb.BeginWrite())
	                    {
	                         settings.IsDarkTheme = false;
	                         db.Commit();
	                    }
                    }

                    AppState.CriticalCell = "LightPink";
	               AppState.NormalCell = "White";
	               AppState.IsDarkTheme = false;
	               return;
	          }

	          if (settings == null)
	          {
	               realmDb.Write(() =>
	               {
	                    realmDb.Add(new Setting { SettingId = AppState.SettingId, IsDarkTheme = true });
	               });
	          }
	          else
	          {
	               using (var db = realmDb.BeginWrite())
	               {
	                    settings.IsDarkTheme = true;
	                    db.Commit();
	               }
	          }

               AppState.CriticalCell = "LIGHTpink";
	          AppState.NormalCell = "DARKGRAY";
            


            AppState.IsDarkTheme = true;
          }
	}
}