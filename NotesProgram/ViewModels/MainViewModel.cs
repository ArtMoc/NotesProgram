using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesProgram
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        List<Note> _notes;
        INotesModel notesModel;
        public MainViewModel()
        {
            notesModel = new JsonNotes();
            Notes = new List<Note>(notesModel.GetAllNotes());
        }
        public ICommand NewNote
        {
            get
            {
                return new ButtonCommand(new Action(() =>
                {
                    AddNote addNote = new AddNote();
                    addNote.ShowDialog();
                    Notes = new List<Note>(notesModel.GetAllNotes());
                }));
            }
        }
        public List<Note> Notes
        {
            get { return _notes; }
            set 
            {
                _notes = value;
                Notify("Notes");
            }
        }
    }
}