using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace NotesProgram
{
    class JsonNotes : INotesModel
    {
        List<Note> _notes;

        private void SaveFile(string filename)
        {
            string json = JsonConvert.SerializeObject(_notes);
            FileStream fileStream = new FileStream(filename, FileMode.Truncate);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(json);
            writer.Flush();
            writer.Close();
            fileStream.Close();
        }
        public JsonNotes()
        {
            GetAllNotes();
        }
        public void AddNote(Note note)
        {
            if(_notes == null)
            {
                _notes = new List<Note>();
            }
            _notes.Add(note);
            SaveFile("notes.txt");
        }

        public void DeleteNote(Note note)
        {
            _notes.Remove(note);
            SaveFile("notes.txt");
        }

        public List<Note> GetActualNotes()
        {
            List<Note> result = new List<Note>();
            foreach(Note note in _notes)
            {
                if(note.DeadLine > DateTime.Now)
                {
                    result.Add(note);
                }
            }
            return result;
        }

        public List<Note> GetAllNotes()
        {
            FileStream fileStream = new FileStream("notes.txt", FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(fileStream);
            string json = reader.ReadToEnd();
            _notes = JsonConvert.DeserializeObject<List<Note>>(json);
            reader.Close();
            fileStream.Close();
            return _notes;
        }

        public Note GetNote(int id)
        {
            return _notes.Find((note) => note.Id == id);
        }

        public void UpdateNote(Note note, Note result)
        {
            Note findNote = _notes.Find((n)=>n.Id==note.Id);
            findNote.Header = result.Header;
            findNote.Text = result.Text;
            findNote.ImageUri = result.ImageUri;
            findNote.DeadLine = result.DeadLine;
            SaveFile("notes.txt");
        }
    }
}
