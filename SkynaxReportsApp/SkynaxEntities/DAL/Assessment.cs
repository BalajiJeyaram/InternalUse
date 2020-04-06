namespace SkynaxEntities.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Assessment")]
    public partial class Assessment
    {
        [Key]
        [Column(Order = 0)]
        public int iD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int useriD { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int examiD { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Score { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime LastModifiedDate { get; set; }
    }
}
