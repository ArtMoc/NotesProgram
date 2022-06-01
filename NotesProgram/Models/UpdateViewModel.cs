using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NotesProgram.Models
{
    class UpdateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static int Id;
        INotesModel _model;
        Note _first, _second;
        public UpdateViewModel()
        {
            //_model = new JsonNotes();
            _model = new DataBaseModel();
            _first = _model.GetNote(Id);
            _second = new Note()
            {
                Id = _first.Id,
                Header = _first.Header,
                Text = _first.Text,
                DeadLine = _first.DeadLine,
                ImageUri = _first.ImageUri
            };
        }

        public Note AddNote
        {
            get {return _second; }
            set
            {
                _second = value;
                Notify("AddNote");
            }
        }
        public ICommand UpdateClick
        {
            get 
            {
                return new ButtonCommand(new Action(() =>
                {
                    _model.UpdateNote(_first, _second);
                    MessageBox.Show("Изменено");
                }));
            }
        }
        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
