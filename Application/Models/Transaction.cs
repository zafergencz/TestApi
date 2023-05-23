using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Application.Models;

    
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long transactionId { get; set; }
        public string? customerId { get; set; }
        public string? orderId { get; set; }

        public string? typeId { get; set; }

        public string? amount { get; set; }

        public string? cardPan { get; set; }

        public string? responseCode { get; set; }

        public string? responseMessage { get; set; }

        public int? status { get; set; }

    }


