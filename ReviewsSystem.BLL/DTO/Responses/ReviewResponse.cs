using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.BLL.DTO.Responses
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime DatePosted { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int WorkOutId { get; set; }
    }

}
