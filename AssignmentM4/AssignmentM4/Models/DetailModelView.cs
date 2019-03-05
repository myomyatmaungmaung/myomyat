using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentM4.Models
{
    public class DetailModelView
    {
       public ICollection<Question> Question { get; set; }
        public SkinType SkinType { get; set; }
    }
}