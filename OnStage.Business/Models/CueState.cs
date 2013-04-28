﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Business.Models
{
    public class CueState
    {

        public int CueId { get; set; }

        public string Number { get; set; }

        public string Status { get; set; }

        public CueState(int cueId, string number, string status)
        {
            CueId = cueId;

            Number = number;
            
            Status = status;
        }

    }
}
