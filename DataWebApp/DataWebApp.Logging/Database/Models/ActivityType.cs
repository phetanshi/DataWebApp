using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWebApp.Logging.Database.Models
{
    [Table("tblActivityTypes")]
    public class ActivityType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ActivityTypeId { get; set; }
        public string ActivityTypeName { get; set; }
    }
}
