using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UniShareAPI.Models.Relations;

namespace Blogger.Relations
{
    public class Secret
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int Attempts { get; set; }
        public int Tries { get; set; }
    }
}
