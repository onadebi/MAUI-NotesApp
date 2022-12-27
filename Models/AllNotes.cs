using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnaNotes.Models
{
    internal class AllNotes
    {
        public ObservableCollection<Models.Note> AllNotez { get; set; } = new ObservableCollection<Note> { };

        public AllNotes() => LoadNotes();

        public void LoadNotes()
        {
            AllNotez.Clear();

            //get the folder where the notes are stored
            string appDataPath = FileSystem.AppDataDirectory;

            // use LINQ extenstions to load the *.onasonic_notes.txt files
            IEnumerable<Models.Note> notes = Directory
                                            .EnumerateFiles(appDataPath, searchPattern: "*.onasonic_notes.txt")
                                            .Select(fileData => new Note
                                            {
                                                Text = File.ReadAllText(fileData),
                                                Date = File.GetCreationTimeUtc(fileData),
                                                FileName = fileData
                                            })
                                            .OrderBy(note => note.Date);
            
            // Add each note into the ObservableCollection
            foreach (var note in notes)
            {
                note.Text = note.Text.Length > 50 ? $"{note.Text.Substring(0, 50)}...": note.Text;
                AllNotez.Add(note);
            }
            
        }
    }
}
