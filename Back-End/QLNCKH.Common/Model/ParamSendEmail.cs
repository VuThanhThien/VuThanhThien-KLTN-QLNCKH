using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace QLNCKH.Common.Model
{
    public class ParamSendEmail
    {
        public List<string> SendTo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        //public IFormFileCollection Attachments { get; set; }
    }
}
