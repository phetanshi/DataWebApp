using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWebApp.Logging.Database.Models
{
    [Table("tblErrorLog")]
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ErrorId { get; set; }

        public int? EventId { get; set; }
        public string UserId { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string Message { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? ExceptionMessage { get; set; }

        public string? StackTrace { get; set; }
        public DateTime ErrorDateTime { get; set; }
        public string? ErrorType { get; set; }
        public string? MethodName { get; set; }
        public string? ClassName { get; set; }
        [StringLength(0, MinimumLength = 500)]
        public string UrlOrModule { get; set; }
        public int LogLevelId { get; set; } = (int)LogLevel.Error;
    }
}
