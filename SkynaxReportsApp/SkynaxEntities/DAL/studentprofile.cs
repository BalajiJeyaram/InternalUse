using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynaxEntities.DAL
{
    public class studentprofile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iD { get; set; }
        public int useriD { get; set; }
        [NotMapped]/*ref: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.schema.notmappedattribute?redirectedfrom=MSDN&view=netframework-4.8 */
        public string username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DoB { get; set; }
        public string Emailaddress { get; set; }
        public string Optional { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}