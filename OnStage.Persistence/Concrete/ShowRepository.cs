using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Persistence.Concrete
{
    public class ShowRepository : IShowRepository
    {

        private OnStageEntitiesContainer context;

        public ShowRepository(OnStageEntitiesContainer context)
        {
            this.context = context;
        }

        public Show GetShow(string title)
        {
            throw new NotImplementedException();
        }

        public Show GetShow(int id)
        {
            return context.Shows.Where(s => s.Id == id).First();
        }

        public IEnumerable<Show> GetAllShows()
        {
            return context.Shows;
        }

        public void AddShow(Show show)
        {
            context.Shows.AddObject(show);
            context.SaveChanges();
        }

        public void DeleteShow(int id)
        {
            context.Shows.DeleteObject(GetShow(id));
            context.SaveChanges();
        }

        public void SaveShow(Show show)
        {
            if (show.EntityState == System.Data.EntityState.Detached)
            {
                context.Shows.AddObject(show);
            }
            context.ObjectStateManager.ChangeObjectState(show, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

    }
}
