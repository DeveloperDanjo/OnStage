using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Persistence.Concrete
{
    public class CueBookRepository : ICueBookRepository
    {

        private OnStageEntitiesContainer context;

        public CueBookRepository(OnStageEntitiesContainer context)
        {
            this.context = context;
        }

        public CueBook GetCueBook(string title)
        {
            throw new NotImplementedException();
        }

        public CueBook GetCueBook(int id)
        {
            return context.CueBooks.Where(s => s.Id == id).First();
        }

        public IEnumerable<CueBook> GetAllCueBooks()
        {
            return context.CueBooks;
        }

        public void AddCueBook(CueBook CueBook)
        {
            context.CueBooks.AddObject(CueBook);
            context.SaveChanges();
        }

        public void DeleteCueBook(int id)
        {
            context.CueBooks.DeleteObject(GetCueBook(id));
            context.SaveChanges();
        }

        public void SaveCueBook(CueBook CueBook)
        {
            if (CueBook.EntityState == System.Data.EntityState.Detached)
            {
                context.CueBooks.AddObject(CueBook);
            }
            context.ObjectStateManager.ChangeObjectState(CueBook, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

    }
}
