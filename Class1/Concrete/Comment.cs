using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concrete
{
    public class Comment : BaseEntity
    {
        [Required, StringLength(300)]
        public string Text { get; set; }
        public int NoteId { get; set; }
        public int BlogUserId { get; set; }
        // İlişkiler
        public virtual Note Note { get; set; }
        public virtual BlogUser BlogUser { get; set; }
    }
}
