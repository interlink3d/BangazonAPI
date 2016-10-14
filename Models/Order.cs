using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Order
  {
    [Key]
    public int OrderId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    
    [DataType(DataType.Date)]
    public DateTime? DateCompleted {get;set;}


    public int CustomerId {get;set;} // listed foreign key 
    public Customer Customer {get;set;} // referenced the table to be used for foreign key


    public int? PaymentTypeId {get;set;}  // PaymentTypeId is the foreign key to the PaymentType table
    public PaymentType PaymentType {get;set;} 

    public ICollection<LineItem> LineItems;
  }
}
