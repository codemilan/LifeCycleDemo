using Android.App;
using Android.OS;

namespace CustomViewDroid
{
    [Activity(Label = "CustomViewDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var customview = FindViewById<CustomView>(Resource.Id.custom_view_1);
            customview.names = new[] { "Bob", "John", "Paul", "Wasi", "Mark" };
        }
    }
}

