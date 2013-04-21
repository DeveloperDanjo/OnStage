using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;

namespace OnStage.Business.Abstract
{
    public interface ICueBookHandler
    {

        CueBook GetCueBook(int id);

    }
}
