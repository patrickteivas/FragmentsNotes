using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Layout.toolbar);

            SetActionBar(toolbar);

            var addButton = FindViewById<Button>(Resource.Id.addButton);

            if (addButton != null)
                addButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, System.EventArgs e)
        {
            var newNoteEditText = FindViewById<EditText>(Resource.Id.newTitleEditText);
            if (!string.IsNullOrEmpty(newNoteEditText.Text) || !string.IsNullOrWhiteSpace(newNoteEditText.Text))
            {
                var databaseService = new DatabaseService();
                databaseService.CreateDatabaseWithTable();

                databaseService.AddNote(newNoteEditText.Text, "");
                newNoteEditText.Text = "";
            }
        }
    }
}