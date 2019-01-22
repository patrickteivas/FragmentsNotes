using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace App1
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public void CreateDatabaseWithTable()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myFragmentNotes.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Note>();
        }

        public List<Note> GetAllNotes()
        {
            return db.Table<Note>().ToList();
        }

        public Note GetOneNote(int id)
        {
            return db.Table<Note>().Where(x => x.Id == id).FirstOrDefault();
        }

        public void AddNote(string title, string content)
        {
            Note newNote = new Note();
            newNote.Title = title;
            newNote.Content = content;
            db.Insert(newNote);
        }

        public void EditNote(int id, string title, string content)
        {
            Note editNote = new Note();
            editNote.Id = id;
            editNote.Title = title;
            editNote.Content = content;
            db.Update(editNote);
        }

        public void DeleteNote(int id)
        {
            Note noteToDelete = new Note();
            noteToDelete.Id = id;
            db.Delete(noteToDelete);
        }
    }
}