using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;

namespace MaterialApp
{
    [Activity(Label="MaterialApp", MainLauncher=true, Icon="@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        Toolbar toolbar;
        NavigationView navView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.main);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);

            // Toolbar
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_list);
            // Navigation
            navView = FindViewById<NavigationView>(Resource.Id.navLayout);
            navView.NavigationItemSelected += NavView_NavigationItemSelected;
            // If first time
            if (bundle == null)
            {

            }
        }

        private void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_map:
                    break;
                case Resource.Id.nav_home_2:
                    break;
            }
            Snackbar.Make(drawerLayout, "You selected: " + e.MenuItem.TitleFormatted, Snackbar.LengthLong).Show();
            drawerLayout.CloseDrawers();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}