using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iParkPro.Entities
{
    public class Park
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BaseUrl { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public Category Type { get; set; }
    }
}
