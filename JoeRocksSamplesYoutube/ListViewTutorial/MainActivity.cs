using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace ListViewTutorial
{
    [Activity(Label = "ListViewTutorial", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<Person> nItems; //this will hold Person Objects as a list generic type.
        private ListView mListView; //this will hold reference to Listview widget.

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            mListView = FindViewById<ListView>(Resource.Id.myListView);
            // create Person objects and push or add into List type with datas as person type.
            nItems = new List<Person>();
            nItems.Add(new Person() { Age = "20", FirstName = "Milan", LastName = "Rawal", Gender = "Male" });
            nItems.Add(new Person() { Age = "22", FirstName = "Deepak", LastName = "Rawal", Gender = "Male" });
            nItems.Add(new Person() { Age = "25", FirstName = "Shivesh", LastName = "Singh", Gender = "Male" });
            nItems.Add(new Person() { Age = "30", FirstName = "Prerana", LastName = "Poudel", Gender = "Female" });

            // this will hold refrence to custom adapter inherited from baseadapter.
            MyListViewAdapter adapter = new MyListViewAdapter(this, nItems);

            // this will attach custom adapter to ListView widget for data access.
            mListView.Adapter = adapter;

            // this will trigger listeners defined when mentioned event is generated in widget.
            mListView.ItemClick += mListView_ItemClickListener;
            mListView.ItemLongClick += mListView_ItemLongClickListener;
        }

        private void mListView_ItemClickListener(object sender, AdapterView.ItemClickEventArgs e)
        {
            Console.WriteLine("An Item has been clicked....");
        }

        private void mListView_ItemLongClickListener(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Console.WriteLine("An Item has been long clicked....");
        }
    }
}

