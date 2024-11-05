using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.DAL.Parameters
{
    public class WorkOutsParameters : QueryStringParameters
    {
        public string Name { get; set; } 
        public int? TypeId { get; set; } 
        public decimal? MaxPrice { get; set; }
    }
}
