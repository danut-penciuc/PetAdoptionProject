using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Requests
{
    public class CreateAdopterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientCode { get; set; }
    }
}
