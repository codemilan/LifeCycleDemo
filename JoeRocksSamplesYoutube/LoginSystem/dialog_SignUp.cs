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

namespace LoginSystem
{
    // this is our custom eventhandler.
    public class OnSignUpEventArgs : EventArgs
    {
        private string mFirstName;
        private string mEmail;
        private string mPassword;

        public string FirstName
        {
           get { return mFirstName; }
           set { mFirstName = value; }
        }
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public OnSignUpEventArgs(string firstname, string email, string password) : base()
        {
            mFirstName = firstname;
            mEmail = email;
            mPassword = password;
        }
    }

    class dialog_SignUp: DialogFragment
    {
        private EditText mTxtFirstName;
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignUp;

        // this will hold reference to our custom EventHadler object.
        public event EventHandler<OnSignUpEventArgs> mOnsignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            mTxtFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);

            mBtnSignUp.Click += MBtnSignUp_Click;

            return view;
        }

        private void MBtnSignUp_Click(object sender, EventArgs e)
        {
            //user clicks the signup button in signup dialog and our custom eventHandler will be called.
            mOnsignUpComplete.Invoke(this, new OnSignUpEventArgs(mTxtFirstName.Text, mTxtEmail.Text, mTxtPassword.Text));
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            // make titlebar invisible.
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            //set animation for dialog.
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}