namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:50 AM
			public class InvoiceModel
			{

			 public System.Guid ?Invoiceid	{ get; set; }
public System.Guid ?verifiedby	{ get; set; }
[DataType(DataType.Date)]
[ModelBinder(BinderType = typeof(DateTimeModelBinder))]
[DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
public System.DateTime ?verifieddate	{ get; set; }

[xssFilter]
public string? invoicenumber{ get; set; }

[xssFilter]
public string? guestno{ get; set; }

[xssFilter]
public string? bookingnumber{ get; set; }

[DataType(DataType.Date)][ModelBinder(BinderType = typeof(DateTimeModelBinder))][DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]public DateTime? invoicedate	{ get; set; }

public decimal? roomcharges{ get; set; }

public decimal? taxdiscount{ get; set; }

[xssFilter]
public string? totalamount{ get; set; }

[xssFilter]
public string? verifiedstatus{ get; set; }
public System.Guid ?createduser	{ get; set; }
[DataType(DataType.Date)]
[ModelBinder(BinderType = typeof(DateTimeModelBinder))]
[DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
public System.DateTime ?createddate	{ get; set; }
public System.Guid ?modifieduser	{ get; set; }
[DataType(DataType.Date)]
[ModelBinder(BinderType = typeof(DateTimeModelBinder))]
[DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
public System.DateTime ?modifieddate	{ get; set; }
public bool isdeleted	{ get; set; }
[xssFilter]
                        [Required(ErrorMessage = "craftmyapp_actionmethodname is required,please pass current action name")]
                        public String craftmyapp_actionmethodname{ get; set; }



			}
			

			public class InvoiceModelValidator: AbstractValidator<InvoiceModel>
			{
					 
					public InvoiceModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_Invoice", () =>
                                    {
                                        {


RuleFor(m => m.invoicedate)


;
RuleFor(m => m.roomcharges)
.LessThanOrEqualTo(99999999).WithMessage("Room charges should be LessThanOrEqualTo 99999999")

;
RuleFor(m => m.taxdiscount)
.LessThanOrEqualTo(100).WithMessage("Tax Discount should be LessThanOrEqualTo 100")

;


}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_Invoice", () =>
                                    {
                                        {


RuleFor(m => m.invoicedate)


;
RuleFor(m => m.roomcharges)
.LessThanOrEqualTo(99999999).WithMessage("Room charges should be LessThanOrEqualTo 99999999")

;
RuleFor(m => m.taxdiscount)
.LessThanOrEqualTo(100).WithMessage("Tax Discount should be LessThanOrEqualTo 100")

;


}

                                    });

						 
						
					}

			}

                

                

                
 

                

                 
                                                    public class InvoiceReviewModel
                                                    {
                                                        public string Invoiceid { get; set; }
                                                        public string reviewcomments { get; set; }
                                                        public string verifiedstatus { get; set; }
                                                        public string verifiedby { get; set; }

														

                                                    }

        

			}
