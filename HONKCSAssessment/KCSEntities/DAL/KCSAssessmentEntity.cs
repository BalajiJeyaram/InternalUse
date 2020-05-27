using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KCSEntities.DAL
{
    public class KCSAssessment
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iD { get; set; }

        public DateTime AInfo1 { get; set; }

        [Required(ErrorMessage ="Business type is required")]
        [MaxLength(10, ErrorMessage = "Name cannot be longer than 10 characters.")]
        public string AInfo2 { get; set; }
        [Required(ErrorMessage = "TSE's Name is required")]
        [MaxLength(50, ErrorMessage = "TSE's Name cannot be longer than 50 characters.")]
        public string AInfo3 { get; set; }

        [Required(ErrorMessage = "Coach Name is required")]
        [MaxLength(50, ErrorMessage = "Coach Name cannot be longer than 50 characters.")]
        public string AInfo4 { get; set; }

        [Required(ErrorMessage = "Case Number is required")]
        [MaxLength(10, ErrorMessage = "Case Number cannot be longer than 10 characters.")]
        public string AInfo5 { get; set; }

        [Required(ErrorMessage = "Article Number is required")]
        [MaxLength(10, ErrorMessage = "Article Number cannot be longer than 10 characters.")]
        public string AInfo6 { get; set; }

        [NotMapped]
        public int LInfo1 { get; set; } = 2;

        [NotMapped]
        public string CInfo1 { get; set; } = "Yes";

        [NotMapped]
        public string CInfo2 { get; set; } = "N/A";

        [NotMapped]
        public string CInfo3 { get; set; } = "No";

        [NotMapped]
        public string CInfo4 { get; set; } = "N/A";

        [NotMapped]
        public string CInfo5 { get; set; } = "No";

        [NotMapped]
        public string CInfo6 { get; set; } = "Duplicated";

        [NotMapped]
        public int AQIInfo1 { get; set; } = 5;

        [NotMapped]
        public int AQIInfo2 { get; set; } = 10;
        [NotMapped]
        public int AQIInfo3 { get; set; } = 10;
        [NotMapped]
        public int AQIInfo4 { get; set; } = 10;
        [NotMapped]
        public int AQIInfo5 { get; set; } = 10;
        [NotMapped]
        public int AQIInfo6 { get; set; } = 10;
        [NotMapped]
        public int AQIInfo7 { get; set; } = 10;
        [NotMapped]
        public int AQIInfo8 { get; set; } = 10;
        [NotMapped]
        public int AQIInfo9 { get; set; } = 10;

        [NotMapped]
        public string CoachComments { get; set; } = "Enered Default value from C#";

        [NotMapped]
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public DateTime LastModifiedDate { get; set; }

    }
}
