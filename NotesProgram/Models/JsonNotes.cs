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
            FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(json);
            writer.Flush();
            writer.Close();
            fileStream.Close();
        }
        public JsonNotes()
        {
            FileStream fileStream = new FileStream("notes.txt", FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(fileStream);
            string json = reader.ReadToEnd();
            _notes = JsonConvert.DeserializeObject<List<Note>>(json);
            reader.Close();
            fileStream.Close();
            /*_notes = new List<Note>()
            {
                new Note()
                {
                    Id=1,
                    Header = "Купить молоко",
                    Text = "Нужно сходить за молоком СРОЧНО!",
                    ImageUri="d:\\1\\cat.jpg",
                    DeadLine = DateTime.Now},

                new Note()
                {
                    Id=2,
                    Header="Проверить ДЗ",
                    Text="Не забывай проверять ДЗ!",
                    ImageUri="d:\\1\\dog.jpeg",
                    DeadLine = DateTime.Now
                }
            };
            SaveFile("notes.txt");*/

        }
        public void AddNote(Note note)
        {
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
            return _notes;
        }

        public Note GetNote(int id)
        {
            return _notes.Find((note) => note.Id == id);
        }

        public void UodateNote(Note note, Note result)
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
