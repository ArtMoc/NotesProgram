using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        Note _note;
        public Note SelectedNote
        {
            get { return _note; }
            set 
            {
                _note = value;
                Notify("SelectedNote");
            }
        }
        
        public MainViewModel()
        {
            //notesModel = new JsonNotes;
            notesModel = new DataBaseModel();
            if (notesModel.GetAllNotes() != null)
                Notes = new List<Note>(notesModel.GetAllNotes());
            else
                Notes = new List<Note>();
        }

        public ICommand UpdateNote
        {
            get
            {
                return new ButtonCommand(new Action(() =>
                {
                    if (_note != null)
                    {
                        Models.UpdateViewModel.Id = _note.Id;
                        Models.Update form = new Models.Update();
                        form.ShowDialog();
                        Notes = new List<Note>(notesModel.GetAllNotes());
                    }
                }));
            }
        }

        public ButtonCommand DeleteNote
        {
            get
            {
                return new ButtonCommand(new Action(() =>
                {
                    if(_note != null)
                    {

                   MessageBoxResult result = MessageBox.Show("Действительно удалить запись?",
                        "Удаление записи" + _note.Header,
                        MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                            notesModel.DeleteNote(_note);
                        Notes = new List<Note>(notesModel.GetAllNotes());
                    }
                }));
            }
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