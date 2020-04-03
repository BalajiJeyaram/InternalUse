using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkynaxEntities.Models
{
    public class userprofile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int iD { get; set; }
        [ConcurrencyCheck]
        public string UserName { get; set; }
        public string Password{ get; set; }
        public char ActiveUser { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastModifiedDate { get; set; }
    }
}
