namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:46:37 AM
			public class PaymentModel
			{

			 public System.Guid ?Paymentid	{ get; set; }
public System.Guid ?tenantid { get; set; }
public String viewertenantids { get; set; }

[xssFilter]
public string? paymentno{ get; set; }

public decimal? roomrent{ get; set; }

public int? noofdays{ get; set; }

[xssFilter]
public string? totalamount{ get; set; }

[xssFilter]
public string? invoiceno{ get; set; }

[xssFilter]
public string? guestno{ get; set; }

[xssFilter]
public string? transactionno{ get; set; }

[xssFilter]
public string? paymentmethod{ get; set; }

[DataType(DataType.Date)][ModelBinder(BinderType = typeof(DateTimeModelBinder))][DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]public DateTime? paymentdate	{ get; set; }

[xssFilter]
public string? paymentstatus{ get; set; }
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
			

			public class PaymentModelValidator: AbstractValidator<PaymentModel>
			{
					 
					public PaymentModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_Payment", () =>
                                    {
                                        {
RuleFor(m => m.roomrent)
.LessThanOrEqualTo(99999999).WithMessage("Room Rent should be LessThanOrEqualTo 99999999")

;
RuleFor(m => m.noofdays)
.LessThanOrEqualTo(99999999).WithMessage("No of days should be LessThanOrEqualTo 99999999")

;





RuleFor(m => m.paymentdate)


;

}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_Payment", () =>
                                    {
                                        {
RuleFor(m => m.roomrent)
.LessThanOrEqualTo(99999999).WithMessage("Room Rent should be LessThanOrEqualTo 99999999")

;
RuleFor(m => m.noofdays)
.LessThanOrEqualTo(99999999).WithMessage("No of days should be LessThanOrEqualTo 99999999")

;





RuleFor(m => m.paymentdate)


;

}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
