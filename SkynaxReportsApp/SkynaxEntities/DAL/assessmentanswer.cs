namespace SkynaxEntities.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessmentanswer")]
    public partial class assessmentanswer
    {
        [Key]
        [Column(Order = 0)]
        public int iD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int assessmentiD { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int examQiD { get; set; }

        public string answertext { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime LastModifiedDate { get; set; }
    }
}
