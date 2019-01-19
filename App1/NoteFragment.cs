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
        public DatabaseService databaseService = new DatabaseService();

        public static NoteFragment NewInstance(int playId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_id", playId);
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

            textView.TextChanged += TextView_TextChanged;

            var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
            textView.SetPadding(padding, padding, padding, padding);
            textView.TextSize = 24;
            textView.Text = notes[ShowId].Content;

            var scroller = new ScrollView(Activity);
            scroller.AddView(textView);

            return scroller;
        }

        private void TextView_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var textView = (EditText)sender;
            databaseService.EditNote((int)textView.Tag, textView.Text);
            Task.Delay(2000);
        }
    }
}