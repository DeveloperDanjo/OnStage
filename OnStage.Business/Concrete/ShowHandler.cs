using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Business.Abstract;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Business.Concrete
{
    public class ShowHandler : IShowHandler
    {

        public IShowRepository repository;

        public ShowHandler(IShowRepository repository)
        {
            this.repository = repository;
        }

        public Show GetShow(string title)
        {
            return repository.GetShow(title);
        }

        public Show GetShow(int id)
        {
            return repository.GetShow(id);
        }

        public IEnumerable<Show> GetAllShows()
        {
            return repository.GetAllShows();
        }

        public void AddShow(Show show)
        {
            repository.AddShow(show);
        }

        public void DeleteShow(int id)
        {
            repository.DeleteShow(id);
        }

        public void SaveShow(Show show)
        {
            repository.SaveShow(show);
        }

    }
}
