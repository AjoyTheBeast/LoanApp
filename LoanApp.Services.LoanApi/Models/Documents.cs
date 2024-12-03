using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanApp.Services.LoanApi.Models
{
    public class Documents
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public LoanRequest LoanRequest { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        
    }
}
