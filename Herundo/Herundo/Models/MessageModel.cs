using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Herundo.Models
{
    public class MessageModel
    {
        [Required(ErrorMessage = "Content is required.")]
        [StringLength(140, ErrorMessage = "Content cannot be longer than 140 characters.")]
        public string Content { get; set; }

        public string Location { get; set; }
    }
}