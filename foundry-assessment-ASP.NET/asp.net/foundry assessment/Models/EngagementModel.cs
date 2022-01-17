using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace foundry_assessment.Models
{
    public class EngagementModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
        public string Employee { get; set; }
        public string Description { get; set; }
        public DateTime started { get; set; }
        public Nullable<DateTime> ended { get; set; }
    }
}