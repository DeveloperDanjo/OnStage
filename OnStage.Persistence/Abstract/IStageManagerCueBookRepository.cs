using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;

namespace OnStage.Persistence.Abstract
{
    public interface IStageManagerCueBookRepository
    {

        StageManagerCueBook GetStageManagerCueBook(int id);

        IEnumerable<StageManagerCueBook> GetAllStageManagerCueBooks();

        void AddStageManagerCueBook(StageManagerCueBook StageManagerCueBook);

        void DeleteStageManagerCueBook(int id);

        void SaveStageManagerCueBook(StageManagerCueBook StageManagerCueBook);

    }
}
