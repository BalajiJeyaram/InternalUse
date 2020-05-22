namespace KCSEntities.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamM")]
    public partial class ExamM
    {
        [Key]
        [Column(Order = 0)]
        public int iD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ExamTitle { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime ExamDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Passscore { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime LastModifiedDate { get; set; }
    }
}
