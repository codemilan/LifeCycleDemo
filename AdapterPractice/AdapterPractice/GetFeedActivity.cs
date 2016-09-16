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

namespace PracticeFeedApp.Droid
{
    [Activity(Label = "Get Feed From Xamarin", MainLauncher = true, Icon = "@drawable/icon")]
    public class GetFeedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Button getBtn = FindViewById<Button> (Resource.Id.GetFeed);

            getBtn.Click += delegate {
                Intent callIntent = new Intent(this, typeof(FeedActivity));
                StartActivity(callIntent);
            };
        }
    }
}