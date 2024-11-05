using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.BLL.DTO.Requests
{
    public class ReviewRequest
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int WorkOutId { get; set; }
    }

}
