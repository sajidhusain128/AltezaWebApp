using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltezaWebApp.Models
{
    public class ServiceModel
    {
        
    }

    public class EnqiryForm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string ServiceType { get; set; }
        public string Message { get; set; }
    }
}
