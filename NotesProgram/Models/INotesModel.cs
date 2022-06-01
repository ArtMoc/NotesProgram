using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProgram
{
    interface INotesModel
    {
        List<Note> GetAllNotes();
        List<Note> GetActualNotes();
        Note GetNote(int id);
        void AddNote(Note note);
        void DeleteNote(Note note);
        void UodateNote(Note note, Note result);
    }
}
