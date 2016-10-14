using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Customer
  {
    [Key]  // primary key in ERD for Customer
    public int CustomerId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required] // validation built in using the required decorator
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public ICollection<PaymentType> PaymentTypes;  // this is the one to many relationship (this is the many side)
  }
}

