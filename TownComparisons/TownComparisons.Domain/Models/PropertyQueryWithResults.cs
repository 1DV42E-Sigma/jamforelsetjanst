﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownComparisons.Domain.Entities;

namespace TownComparisons.Domain.Models
{
    public class PropertyQueryWithResults
    {
        public PropertyQueryInfo Query { get; set; }

        public List<PropertyQueryResult> Results { get; set; }


        //Constructors
        public PropertyQueryWithResults()
        {
            Results = new List<PropertyQueryResult>();
        }
        public PropertyQueryWithResults(PropertyQueryInfo query, List<PropertyQueryResult> results = null)
        {
            Query = query;
            Results = results ?? new List<PropertyQueryResult>();
        }
    }
}
