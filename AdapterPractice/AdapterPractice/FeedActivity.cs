using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Xml.Linq;
using System.Linq;

namespace PracticeFeedApp.Droid
{
    [Activity(Label = "Xamarin Feeds")]
    public class FeedActivity : ListActivity
    {
        private RssItem[] _items;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            /* this two line makes action bar clickable and it's necessary
            to override method 'OnOptionsItemSelected' to make action bar clickable. */
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            RequestWindowFeature(WindowFeatures.ActionBar);

            using (var client = new HttpClient())
            {
                var xmlFeed = await client.GetStringAsync("http://blog.xamarin.com/feed/");
                var doc = XDocument.Parse(xmlFeed);

                XNamespace dc = "http://purl.org/dc/elements/1.1/";
                _items = (from item in doc.Descendants("item")
                          select new RssItem
                          {
                              Title = item.Element("title").Value,
                              PubDate = item.Element("pubDate").Value,
                              Creator = item.Element(dc + "creator").Value,
                              Link = item.Element("link").Value
                          }).ToArray();

                ListAdapter = new FeedAdapter(this, _items);
            }
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            var second = new Intent(this, typeof(WebActivity));
            second.PutExtra("link", _items[position].Link);
            StartActivity(second);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}

