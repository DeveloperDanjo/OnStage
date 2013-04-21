using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;

namespace OnStage.Persistence.Abstract
{
    public interface ICueBookRepository
    {

        CueBook GetCueBook(int id);

        IEnumerable<CueBook> GetAllCueBooks();

        void AddCueBook(CueBook CueBook);

        void DeleteCueBook(int id);

        void SaveCueBook(CueBook CueBook);

    }
}
