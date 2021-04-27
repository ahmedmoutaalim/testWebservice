using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAuth
{
    public class user
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime creationdate { get; set; }
    }
}