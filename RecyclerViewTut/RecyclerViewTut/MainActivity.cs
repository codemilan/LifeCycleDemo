using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;

namespace RecyclerViewTut
{
    [Activity(Label = "RecyclerViewTutorial", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        private List<Email> mEmails;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mEmails = new List<Email>();
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });

            //Create our layout manager
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(mEmails);
            mRecyclerView.SetAdapter(mAdapter);

        }
    }

    // NOTICE:- Take care this class isn't inside MainActivity class but inside namespace,
    // you can make different file inside adapter directory to organize codes for
    // readability and organization.
    public class RecyclerAdapter : RecyclerView.Adapter
    {
        private List<Email> mEmails;

        public RecyclerAdapter(List<Email> emails)
        {
            mEmails = emails;
        }

        // NOTICE:- this class is inside another class so it's called innerclass
        // purpose of this class is to create and attach view to adapter.
        public class MyView : RecyclerView.ViewHolder
        {
            public View mMainView { get; set; }
            public TextView mName { get; set; }
            public TextView mSubject { get; set; }
            public TextView mMessage { get; set; }

            public MyView(View view) : base(view)
            {
                mMainView = view;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row, parent, false);

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            TextView txtSubject = row.FindViewById<TextView>(Resource.Id.txtSubject);
            TextView txtMessage = row.FindViewById<TextView>(Resource.Id.txtMessage);

            MyView view = new MyView(row) { mName = txtName, mSubject = txtSubject, mMessage = txtMessage };
            return view;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyView myHolder = holder as MyView;
            myHolder.mName.Text = mEmails[position].Name;
            myHolder.mSubject.Text = mEmails[position].Subject;
            myHolder.mMessage.Text = mEmails[position].Message;
        }

        public override int ItemCount
        {
            get { return mEmails.Count; }
        }
    }
}