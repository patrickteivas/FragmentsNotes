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
        bool showingTwoFragments;
        int selectedShowId;
        public List<Note> notes;

        public TitlesFragment()
        {
            // Being explicit about the requirement for a default constructor.
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var databaseService = new DatabaseService();
            databaseService.CreateDatabaseWithTable();
            notes = databaseService.GetAllNotes();

            ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemActivated1, notes.Select(x => x.Title).ToArray());

            if (savedInstanceState != null)
            {
                selectedShowId = savedInstanceState.GetInt("current_id", 0);
            }

            var quoteContainer = Activity.FindViewById(Resource.Id.playquote_container);
            showingTwoFragments = quoteContainer != null &&
                                  quoteContainer.Visibility == ViewStates.Visible;
            if (showingTwoFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                ShowNoteContent(selectedShowId);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_id", selectedShowId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowNoteContent(position);
        }

        void ShowNoteContent(int showId)
        {

            if (showingTwoFragments)
            {
                selectedShowId = showId;

                ListView.SetItemChecked(selectedShowId, true);

                var showNoteFragment = FragmentManager.FindFragmentById(Resource.Id.playquote_container) as NoteFragment;

                if (showNoteFragment == null || showNoteFragment.ShowId != showId)
                {
                    //var container = Activity.FindViewById(Resource.Id.playquote_container);
                    var quoteFrag = NoteFragment.NewInstance(selectedShowId);

                    FragmentTransaction ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.playquote_container, quoteFrag);
                    ft.Commit();
                }
            }
            else
            {
                var intent = new Intent(Activity, typeof(NoteActivity));
                intent.PutExtra("current_id", showId);
                StartActivity(intent);
            }
        }
    }
}