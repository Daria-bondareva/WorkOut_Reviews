using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.DAL.Parameters
{
    public class ReviewsParameters : QueryStringParameters
    {
        public int? UserId { get; set; } 
        public int? WorkOutId { get; set; } 
        public int? Rating { get; set; }
    }
}
