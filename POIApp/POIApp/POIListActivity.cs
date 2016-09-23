using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;

namespace POIApp
{
    [Activity(Label = "POIs", MainLauncher = true, Icon = "@drawable/icon")]
    public class POIListActivity : Activity
    {
        ListView _poiListView;
        POIListViewAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.POIList);

            _poiListView = FindViewById<ListView>(Resource.Id.poiListView);
            _adapter = new POIListViewAdapter(this);
            _poiListView.Adapter = _adapter;
            _poiListView.ItemClick += POIClicked;
        }

        protected override void OnResume()
        {
            base.OnResume();
            _adapter.NotifyDataSetChanged();
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
    }
}
