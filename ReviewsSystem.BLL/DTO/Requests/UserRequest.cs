using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.BLL.DTO.Requests
{
        public class UserRequest
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

}
