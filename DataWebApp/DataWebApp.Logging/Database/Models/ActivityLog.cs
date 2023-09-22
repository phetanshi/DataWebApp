using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWebApp.Logging.Database.Models
{
    [Table("tblActivityLog")]
    public class ActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActivityId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime LogDateTime { get; set; }

        [StringLength(0, MinimumLength = 500)]
        public string UrlOrModule { get; set; }
        public long? LogLevelId { get; set; }
    }
}
