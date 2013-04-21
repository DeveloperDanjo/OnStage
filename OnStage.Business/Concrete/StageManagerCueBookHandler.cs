using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Business.Abstract;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Business.Concrete
{
    public class StageManagerCueBookHandler : IStageManagerCueBookHandler
    {

        public IStageManagerCueBookRepository repository;

        public StageManagerCueBookHandler(IStageManagerCueBookRepository repository)
        {
            this.repository = repository;
        }

        public StageManagerCueBook GetStageManagerCueBook(int id)
        {
            return repository.GetStageManagerCueBook(id);
        }

        public IEnumerable<StageManagerCueBook> GetAllStageManagerCueBooks()
        {
            return repository.GetAllStageManagerCueBooks();
        }

        public void AddStageManagerCueBook(StageManagerCueBook StageManagerCueBook)
        {
            repository.AddStageManagerCueBook(StageManagerCueBook);
        }

        public void DeleteStageManagerCueBook(int id)
        {
            repository.DeleteStageManagerCueBook(id);
        }

        public void SaveStageManagerCueBook(StageManagerCueBook StageManagerCueBook)
        {
            repository.SaveStageManagerCueBook(StageManagerCueBook);
        }

    }
}
