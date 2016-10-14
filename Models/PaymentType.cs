using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class PaymentType
  {
    [Key]
    public int PaymentTypeId {get;set;} // convention looks at this and Order- line 29 and knows that they are tied through PaymentTypeId 

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(12)]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; }
    public int CustomerId {get;set;}
    public Customer Customer {get;set;} // one customer has many payment types (customer- line25)
  }
}