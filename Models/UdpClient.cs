using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class UdpClient : Client
    {
        public UdpClient(string firstName, string lastName, string password) : base(firstName, lastName, password) 
        {

        }
    }
}
