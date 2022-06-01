using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProgram
{
    class Note
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text {get; set;}
        public string ImageUri { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
