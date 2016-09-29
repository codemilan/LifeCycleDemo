using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace DesignerWalkthrough
{
    [Activity(Label = "DesignerWalkthrough", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        List<ColorItem> colorItems = new List<ColorItem>();
        ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            listView = FindViewById<ListView>(Resource.Id.myListView);

            colorItems.Add(new ColorItem() { Color = Android.Graphics.Color.DarkRed, ColorName = "Dark Red", Code = "8B0000" });
            colorItems.Add(new ColorItem() { Color = Android.Graphics.Color.SlateBlue, ColorName = "Slate Blue", Code = "6A5ACD" });
            colorItems.Add(new ColorItem() { Color = Android.Graphics.Color.ForestGreen, ColorName = "Forest Green", Code = "228B22" });

            listView.Adapter = new ColorAdapter(this, colorItems);
        }
    }
}

