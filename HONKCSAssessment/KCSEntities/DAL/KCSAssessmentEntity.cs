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


        public int LInfo1 { get; set; } = 2;


        public string CInfo1 { get; set; } = "Yes";


        public string CInfo2 { get; set; } = "N/A";


        public string CInfo3 { get; set; } = "No";


        public string CInfo4 { get; set; } = "N/A";


        public string CInfo5 { get; set; } = "No";


        public string CInfo6 { get; set; } = "Duplicated";


        public int AQIInfo1 { get; set; } = 5;


        public int AQIInfo2 { get; set; } = 10;

        public int AQIInfo3 { get; set; } = 10;

        public int AQIInfo4 { get; set; } = 10;

        public int AQIInfo5 { get; set; } = 10;

        public int AQIInfo6 { get; set; } = 10;

        public int AQIInfo7 { get; set; } = 10;

        public int AQIInfo8 { get; set; } = 10;

        public int AQIInfo9 { get; set; } = 10;


        public string CoachComments { get; set; } = "Default value entered by C#";


        public DateTime CreatedDate { get; set; }


        public DateTime LastModifiedDate { get; set; }

        public int CreatedBy { get; set; }

        public int LastModifiedBy { get; set; }

    }
}
