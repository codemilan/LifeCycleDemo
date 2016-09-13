using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LifeCycleDemo
{
    [Activity(Label = "LifeCycleDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            Console.WriteLine("OnCreate called, Activity is becoming Active");
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
        }

        protected override void OnStart()
        {
            Console.WriteLine("OnStart called, App is Active");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Console.WriteLine("OnResume called, app is ready to interact with the user");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Console.WriteLine("OnPause called, App is moving to background");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Console.WriteLine("OnStop called, App is in the background");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Console.WriteLine("OnDestroy called, App is Terminating");
        }
    }
}

