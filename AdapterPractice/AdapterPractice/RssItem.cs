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
    public class RssItem
    {
        public string Title { get; set; }
        public string PubDate { get; set; }
        public string Creator { get; set; }
        public string Link { get; set; }
    }
}