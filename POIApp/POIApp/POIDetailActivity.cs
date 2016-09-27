using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Android.Content.PM;

namespace POIApp
{
    [Activity(Label = "POIDetailActivity")]
    public class POIDetailActivity : Activity, ILocationListener
    {
        EditText _nameEditText;
        EditText _descrEditText;
        EditText _addrEditText;
        EditText _latEditText;
        EditText _longEditText;
        ImageView _poiImageView;
        PointOfInterest _poi;
        LocationManager _locMgr;
        ImageButton _locationImageButton;
        ImageButton _mapImageButton;
        ProgressDialog _progressDialog;
        bool _obtainingLocation = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.POIDetail);

            _locMgr = GetSystemService(Context.LocationService) as LocationManager;

            // bind the controls contained in POIDetail view, this binding should be done after
            // setcontentview is called since this call will only create components accessible for this activity

            _nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            _descrEditText = FindViewById<EditText>(Resource.Id.descrEditText);
            _addrEditText = FindViewById<EditText>(Resource.Id.addrEditText);
            _latEditText = FindViewById<EditText>(Resource.Id.latEditText);
            _longEditText = FindViewById<EditText>(Resource.Id.longEditText);
            _poiImageView = FindViewById<ImageView>(Resource.Id.poiImageView);
            _locationImageButton = FindViewById<ImageButton>(Resource.Id.locationImageButton);
            _mapImageButton = FindViewById<ImageButton>(Resource.Id.mapImageButton);

            _locationImageButton.Click += GetLocationClicked;

            _mapImageButton.Click += GetMapClicked;

            if (Intent.HasExtra("poiId"))
            {
                int poiId = Intent.GetIntExtra("poiId", -1);
                _poi = POIData.Service.GetPOI(poiId);
            }
            else
            {
                _poi = new PointOfInterest();
            }
            UpadateUi();
        }

        protected void GetLocationClicked(object s, EventArgs e)
        {
            _obtainingLocation = true;
            _progressDialog = ProgressDialog.Show(this, "", "Obtaining location...");
            Criteria criteria = new Criteria();
            criteria.Accuracy = Accuracy.NoRequirement;
            criteria.PowerRequirement = Power.NoRequirement;
            _locMgr.RequestSingleUpdate(criteria, this, null);
        }

        protected void GetMapClicked(object s, EventArgs e)
        {
            Android.Net.Uri geoUri;
            if (String.IsNullOrEmpty(_addrEditText.Text))
            {
                geoUri = Android.Net.Uri.Parse(String.Format("geo:{0},{1}", _poi.Latitude, _poi.Longitude));
            }
            else
            {
                geoUri = Android.Net.Uri.Parse(String.Format("geo:0,0?q={0}", _addrEditText.Text));
            }
            Intent mapIntent = new Intent(Intent.ActionView, geoUri);

            PackageManager packageManager = PackageManager;
            IList<ResolveInfo> activities = packageManager.QueryIntentActivities(mapIntent, 0);
            if (activities.Count == 0)
            {
                AlertDialog.Builder alertConfirm = new AlertDialog.Builder(this);
                alertConfirm.SetCancelable(false);
                alertConfirm.SetPositiveButton("OK", delegate { });
                alertConfirm.SetMessage("No map app available.");
                alertConfirm.Show();
            }
            else
                StartActivity(mapIntent);
        }


        protected void UpadateUi()
        {
            _nameEditText.Text = _poi.Name;
            _addrEditText.Text = _poi.Address;
            _descrEditText.Text = _poi.Description;
            _latEditText.Text = _poi.Latitude.ToString();
            _longEditText.Text = _poi.Longitude.ToString();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.POIDetailMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionSave:
                    ValidateAndSavePOI();
                    return true;
                case Resource.Id.actionDelete:
                    ValidateAndDeletePOI();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);
            // disable delete for a new POI
            if (!_poi.Id.HasValue)
            {
                IMenuItem item = menu.FindItem(Resource.Id.actionDelete);
                item.SetEnabled(false);
            }
            return true;
        }

        protected void ValidateAndSavePOI()
        {
            bool error = false;
            double? tempLatitude = null;
            double? tempLongitude = null;

            if (String.IsNullOrEmpty(_nameEditText.Text))
            {
                _nameEditText.Error = "Name can't be blank";
                error = true;
            }
            else
            {
                _nameEditText.Error = null;
            }

            if (!String.IsNullOrEmpty(_latEditText.Text))
            {
                try
                {
                    tempLatitude = Double.Parse(_latEditText.Text);
                    if ((tempLatitude > 90) | (tempLatitude < -90))
                    {
                        _latEditText.Error = "Latitude must be a decimal value between -90 and 90";
                        error = true;
                    }
                    else
                        _latEditText.Error = null;
                }
                catch
                {
                    _latEditText.Error = "Latitude must be valid decimal number";
                    error = true;
                }
            }

            if (!String.IsNullOrEmpty(_longEditText.Text))
            {
                try
                {
                    tempLongitude = Double.Parse(_longEditText.Text);
                    if ((tempLongitude > 90) | (tempLongitude < -90))
                    {
                        _longEditText.Error = "Longitude must be a decimal value between -90 and 90";
                        error = true;
                    }
                    else
                        _longEditText.Error = null;
                }
                catch
                {
                    _longEditText.Error = "Longitude must be valid decimal number";
                    error = true;
                }
            }

            if (!error)
            {
                _poi.Name = _nameEditText.Text;
                _poi.Description = _descrEditText.Text;
                _poi.Address = _addrEditText.Text;
                _poi.Latitude = Double.Parse(_latEditText.Text);
                _poi.Longitude = Double.Parse(_longEditText.Text);
                POIData.Service.SavePOI(_poi);
                Toast toast = Toast.MakeText(this, String.Format("{0} saved.", _poi.Name), ToastLength.Short);
                toast.Show();
                Finish();
            }
        }

        protected void ValidateAndDeletePOI()
        {
            AlertDialog.Builder alertConfirm = new AlertDialog.Builder(this);
            alertConfirm.SetCancelable(false);
            alertConfirm.SetPositiveButton("OK", ConfirmDelete);
            alertConfirm.SetNegativeButton("Cancel", delegate { });
            alertConfirm.SetMessage(String.Format("Are you sure you want to delete {0}?", _poi.Name));
            alertConfirm.Show();
        }

        protected void ConfirmDelete(object sender, EventArgs e)
        {
            POIData.Service.DeletePOI(_poi);
            Toast toast = Toast.MakeText(this, String.Format("{0} deleted.", _poi.Name), ToastLength.Short);
            toast.Show();
            Finish();
        }

        public void OnLocationChanged(Location location)
        {
            _latEditText.Text = location.Latitude.ToString();
            _longEditText.Text = location.Longitude.ToString();

            Geocoder geocdr = new Geocoder(this);
            IList<Address> addresses = geocdr.GetFromLocation(location.Latitude, location.Longitude, 5);

            if (addresses.Any())
            {
                UpdateAddressFields(addresses.First());
            }
            _obtainingLocation = false;
            _progressDialog.Cancel();
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

        protected void UpdateAddressFields(Address addr)
        {
            if (String.IsNullOrEmpty(_nameEditText.Text))
            {
                _nameEditText.Text = addr.FeatureName;

                if (String.IsNullOrEmpty(_addrEditText.Text))
                {
                    for (int i = 0; i < addr.MaxAddressLineIndex; i++)
                    {
                        if (!String.IsNullOrEmpty(_addrEditText.Text))
                        {
                            _addrEditText.Text += System.Environment.NewLine;
                            _addrEditText.Text += addr.GetAddressLine(i);
                        }
                    }
                }
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutBoolean("obtaininglocation", _obtainingLocation);
            // if we were waiting on location updates; cancel
            if (_obtainingLocation)
            {
                _locMgr.RemoveUpdates(this);
            }
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            _obtainingLocation = savedInstanceState.GetBoolean("obtaininglocation");
            // if we were waiting on location updates; restart
            if (_obtainingLocation)
                GetLocationClicked (this, new EventArgs ()); } 
        }
    }