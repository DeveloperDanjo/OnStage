using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Persistence.Concrete
{
    public class StageManagerCueBookRepository : IStageManagerCueBookRepository
    {

        private OnStageEntitiesContainer context;

        public StageManagerCueBookRepository(OnStageEntitiesContainer context)
        {
            this.context = context;
        }

        public StageManagerCueBook GetStageManagerCueBook(string title)
        {
            throw new NotImplementedException();
        }

        public StageManagerCueBook GetStageManagerCueBook(int id)
        {
            return context.StageManagerCueBooks.Where(s => s.Id == id).First();
        }

        public IEnumerable<StageManagerCueBook> GetAllStageManagerCueBooks()
        {
            return context.StageManagerCueBooks;
        }

        public void AddStageManagerCueBook(StageManagerCueBook StageManagerCueBook)
        {
            context.StageManagerCueBooks.AddObject(StageManagerCueBook);
            context.SaveChanges();
        }

        public void DeleteStageManagerCueBook(int id)
        {
            context.StageManagerCueBooks.DeleteObject(GetStageManagerCueBook(id));
            context.SaveChanges();
        }

        public void SaveStageManagerCueBook(StageManagerCueBook StageManagerCueBook)
        {
            if (StageManagerCueBook.EntityState == System.Data.EntityState.Detached)
            {
                context.StageManagerCueBooks.AddObject(StageManagerCueBook);
            }
            context.ObjectStateManager.ChangeObjectState(StageManagerCueBook, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

    }
}
