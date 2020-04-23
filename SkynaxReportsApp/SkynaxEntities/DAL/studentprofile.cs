using System;

namespace SkynaxEntities.DAL
{
    public class studentprofile
    {
        public int iD { get; set; }
        public int useriD { get; set; }
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