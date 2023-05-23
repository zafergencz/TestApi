using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestApi.Application.Models;

    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customerId { get; set; }

        public string? name { get; set; }

        public string? surname { get; set; }

        public int? birthDate { get; set; }

        public long? identityNo { get; set; }

        public bool? identityVerified { get; set; }

        public string? status { get; set; }

    }
