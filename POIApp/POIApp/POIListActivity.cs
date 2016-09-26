using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Locations;
using Android.Runtime;

namespace POIApp
{
    [Activity(Label = "POIs", MainLauncher = true, Icon = "@drawable/icon")]
    public class POIListActivity : Activity, ILocationListener
    {
        ListView _poiListView;
        POIListViewAdapter _adapter;
        LocationManager _locMgr;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.POIList);

            _locMgr = GetSystemService(Context.LocationService) as LocationManager;

            _poiListView = FindViewById<ListView>(Resource.Id.poiListView);
            _adapter = new POIListViewAdapter(this);
            _poiListView.Adapter = _adapter;
            _poiListView.ItemClick += POIClicked;
        }

        protected override void OnResume()
        {
            base.OnResume();

            Criteria criteria = new Criteria();
            criteria.Accuracy = Accuracy.NoRequirement;
            criteria.PowerRequirement = Power.NoRequirement;
            string provider = _locMgr.GetBestProvider(criteria, true);
            _locMgr.RequestLocationUpdates(provider, 20000, 100, this);

            _adapter.NotifyDataSetChanged();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locMgr.RemoveUpdates(this);
        }

        protected void POIClicked(object sender, ListView.ItemClickEventArgs e)
        {
            PointOfInterest poi = POIData.Service.GetPOI((int)e.Id);
            Intent poiDetailIntent = new Intent(this, typeof(POIDetailActivity));
            poiDetailIntent.PutExtra("poiId", poi.Id.Value);
            StartActivity(poiDetailIntent);
        }

        // HIT_N_TRAIL: this method creates clickable menu in actionbar ie. below the notification bar when this activity is onCreate callback(research when called) state or viewable state.
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.POIListViewMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        // this method is called when an action defined on OnCreateOptionsMenu is clicked.
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionNew:
                    StartActivity(typeof(POIDetailActivity));
                    return true;
                case Resource.Id.actionRefresh:
                    POIData.Service.RefreshCache();
                    _adapter.NotifyDataSetChanged();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        // implement methods from ILocationServices interface
        public void OnLocationChanged(Location location)
        {
            _adapter.CurrentLocation = location;
            _adapter.NotifyDataSetChanged();
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}
