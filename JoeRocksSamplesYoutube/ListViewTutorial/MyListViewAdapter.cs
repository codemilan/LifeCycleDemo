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

namespace ListViewTutorial
{
    class MyListViewAdapter : BaseAdapter<Person>
    {
        public List<Person> nItems; // will hold collection of Person objects in list.
        private Context mContext; // this will hold activity in which to show the list contents or from
                                  // from the activity where the adapter object is created.

        // constructor to set context activity and List of person objects for specific adapter object. 
        public MyListViewAdapter(Context context, List<Person> items)
        {
            this.nItems = items;
            this.mContext = context;
        }

        // base class instance or object property method overridden to get count of collected objects.
        public override int Count
        {
            get
            {
                return this.nItems.Count;
            }
        }

        // base class instance method overridden to get position or index of specified object.
        public override long GetItemId(int position)
        {
            return position;
        }

        // HIT_N_TRIAL:- research on this type of syntax and this will return an object in collection
        // or object of type person in the list based on position or index.
        public override Person this[int position]
        {
            get
            {
                return nItems[position];
            }
        }

        // this is important method to get the view, for this case the view will be a row.
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = nItems[position].FirstName;

            TextView txtLastName = row.FindViewById<TextView>(Resource.Id.txtLastName);
            txtLastName.Text = nItems[position].LastName;

            TextView txtAge = row.FindViewById<TextView>(Resource.Id.txtAge);
            txtAge.Text = nItems[position].Age;

            TextView txtGender = row.FindViewById<TextView>(Resource.Id.txtGender);
            txtGender.Text = nItems[position].Gender;

            return row;
        }
    }
}