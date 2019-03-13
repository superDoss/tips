using System;
using System.Collections.Generic;

namespace Tips.api
{
    public class TipPerDay
    {
        public List<DateTime> Dates { get; set; }
        public List<int> Sum { get; set; }
    }
}