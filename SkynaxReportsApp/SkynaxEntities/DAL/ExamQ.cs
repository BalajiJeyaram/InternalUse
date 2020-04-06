namespace SkynaxEntities.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExamQ
    {
        [Key]
        [Column(Order = 0)]
        public int iD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int examiD { get; set; }

        [Key]
        [Column(Order = 2)]
        public string item { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Created { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime LastModifiedDate { get; set; }
    }
}
