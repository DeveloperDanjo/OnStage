using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Business.Abstract;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Business.Concrete
{
    public class CueBookHandler : ICueBookHandler
    {

        public ICueBookRepository repository;

        public CueBookHandler(ICueBookRepository repository)
        {
            this.repository = repository;
        }

        public CueBook GetCueBook(int id)
        {
            return repository.GetCueBook(id);
        }

        public IEnumerable<CueBook> GetAllCueBooks()
        {
            return repository.GetAllCueBooks();
        }

        public void AddCueBook(CueBook CueBook)
        {
            repository.AddCueBook(CueBook);
        }

        public void DeleteCueBook(int id)
        {
            repository.DeleteCueBook(id);
        }

        public void SaveCueBook(CueBook CueBook)
        {
            repository.SaveCueBook(CueBook);
        }

    }
}
