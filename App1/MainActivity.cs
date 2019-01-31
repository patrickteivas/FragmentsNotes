using Android.App;
using Android.OS;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AppCenter.Start("b3c9dd2b-93d3-4579-8712-15f4a6ec8e2a",
                typeof(Distribute), typeof(Analytics), typeof(Crashes));

            SetContentView(Resource.Layout.activity_main);
        }
    }
}