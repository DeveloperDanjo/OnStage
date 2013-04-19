using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;

namespace OnStage.Persistence.Abstract
{
    public interface IShowRepository
    {

        Show GetShow(string title);

        Show GetShow(int id);

        IEnumerable<Show> GetAllShows();

        void AddShow(Show show);

        void DeleteShow(int id);

        void SaveShow(Show show);

    }
}
