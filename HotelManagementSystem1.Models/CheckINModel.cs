namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:23 AM
			public class CheckINModel
			{

			 public System.Guid ?CheckINid	{ get; set; }
public System.Guid ?tenantid { get; set; }
public String viewertenantids { get; set; }

[xssFilter]
public string? checkinno{ get; set; }

[xssFilter]
public string? guestno{ get; set; }

[DataType(DataType.Date)][ModelBinder(BinderType = typeof(DateTimeModelBinder))][DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]public DateTime? checkindate	{ get; set; }

public decimal? depositamount{ get; set; }

[xssFilter]
public string? status{ get; set; }

[xssFilter]
public string? roomno{ get; set; }

[xssFilter]
public string? checkintime{ get; set; }
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
			

			public class CheckINModelValidator: AbstractValidator<CheckINModel>
			{
					 
					public CheckINModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_Check_IN", () =>
                                    {
                                        {

RuleFor(m => m.checkindate)


;
RuleFor(m => m.depositamount)
.LessThanOrEqualTo(99999999).WithMessage("DepositAmount should be LessThanOrEqualTo 99999999")

;



}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_Check_IN", () =>
                                    {
                                        {

RuleFor(m => m.checkindate)


;
RuleFor(m => m.depositamount)
.LessThanOrEqualTo(99999999).WithMessage("DepositAmount should be LessThanOrEqualTo 99999999")

;



}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
