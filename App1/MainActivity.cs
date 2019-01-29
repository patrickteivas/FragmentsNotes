using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Distribute;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AppCenter.Start("b3c9dd2b-93d3-4579-8712-15f4a6ec8e2a", typeof(Distribute));

            SetContentView(Resource.Layout.activity_main);
        }
    }
}