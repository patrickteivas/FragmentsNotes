using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class TitlesFragment : ListFragment
    {
        int selectedShowId;

        public TitlesFragment()
        {
            // Being explicit about the requirement for a default constructor.
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var databaseService = new DatabaseService();
            databaseService.CreateDatabaseWithTable();
            ListAdapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemActivated1, databaseService.GetAllNotes().Select(x=>x.Title).ToArray());

            if (savedInstanceState != null)
            {
                selectedShowId = savedInstanceState.GetInt("current_id", 0);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_id", selectedShowId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowPlayQuote(position);
        }

        void ShowPlayQuote(int playId)
        {
            var intent = new Intent(Activity, typeof(NoteActivity));
            intent.PutExtra("current_id", playId);
            StartActivity(intent);
        }
    }
}