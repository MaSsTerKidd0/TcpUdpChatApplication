using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Client
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _password;

        #endregion

        #region Properties
        public string fullName { get { return _firstName + " " + _lastName; } }

        #endregion
        public Client(string firstName, string lastName, string password) 
        {
            _firstName = firstName;
            _lastName = lastName;
            _password = password;
        }

    }
}
