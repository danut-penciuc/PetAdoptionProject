using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Responses
{
    public class AdopterResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientCode { get; set; }
    }
}
