using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class NoteFragment : Fragment
    {
        public int ShowId => Arguments.GetInt("current_id", 0);
        private DatabaseService databaseService = new DatabaseService();

        public static NoteFragment NewInstance(int showId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_id", showId);
            return new NoteFragment { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
                return null;

            databaseService.CreateDatabaseWithTable();
            var notes = databaseService.GetAllNotes();

            var textView = new EditText(Activity);
            textView.Tag = notes[ShowId].Id;
            textView.TextSize = 24;
            textView.Text = notes[ShowId].Content;
            textView.TextChanged += TextView_TextChanged;

            var titleTextView = new EditText(Activity);
            titleTextView.Tag = notes[ShowId].Id;
            titleTextView.TextSize = 24;
            titleTextView.Text = notes[ShowId].Title;
            titleTextView.TextChanged += TitleTextView_TextChanged;

            var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
            var layout = new LinearLayout(Activity);
            layout.Orientation = Orientation.Vertical;
            layout.SetPadding(padding, padding, padding, padding);

            layout.AddView(titleTextView); 
            layout.AddView(textView);

            var scroller = new ScrollView(Activity);
            scroller.AddView(layout);

            return scroller;
        }

        private void TitleTextView_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var textView = (EditText)sender;
            int id = (int)textView.Tag;
            databaseService.EditNote(id, textView.Text, databaseService.GetOneNote(id).Content);
            Task.Delay(2000);
        }

        private void TextView_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var textView = (EditText)sender;
            int id = (int)textView.Tag;
            databaseService.EditNote(id, databaseService.GetOneNote(id).Title, textView.Text);
            Task.Delay(2000);
        }
    }
}