using System;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LoginSystem
{
    [Activity(Label = "LoginSystem", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUP;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            mBtnSignUP = FindViewById<Button>(Resource.Id.btnSignUp);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            mBtnSignUP.Click += (Object sender, EventArgs args) =>
            {
                // Pull up dialog
                // fragment transaction are use to control adding and removing of fragments
                // in proper order.
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");
                signUpDialog.mOnsignUpComplete += SignUpDialog_mOnsignUpComplete;
            };
        }

        void SignUpDialog_mOnsignUpComplete(object sender, OnSignUpEventArgs e)
        {
            // this section will be in UI thread so no need to specify runonuithread.
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
        }

        void ActLikeRequest()
        {
            Thread.Sleep(3000);
            //This function is on another thread so explicitly specify to run on UI thread.
            RunOnUiThread(() => { mProgressBar.Visibility = ViewStates.Invisible; });
        }
    }
}
