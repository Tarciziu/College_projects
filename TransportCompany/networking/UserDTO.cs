using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace network.dto
{
    [Serializable]
    public class UserDTO
    {
        public string Id { get; set; }
        public string password { get; set; }

        public UserDTO(string id, string password)
        {
            this.Id = id;
            this.password = password;
        }
    }
}
