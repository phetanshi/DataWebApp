using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataWebApp.Logging.Database.Models
{
    [Table("tblLogLevels")]
    public class AppLogLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long LogLevelId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Level { get; set; }

        public List<ActivityLog>? ActivityLogs { get; set; }
    }
}
