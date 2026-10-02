namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:31 AM
			public class HousekeepingModel
			{

			 public System.Guid ?Housekeepingid	{ get; set; }
public System.Guid ?tenantid { get; set; }
public String viewertenantids { get; set; }

[xssFilter]
public string housekeepingno{ get; set; }

[DataType(DataType.Date)][ModelBinder(BinderType = typeof(DateTimeModelBinder))][DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]public DateTime cleaningdate	{ get; set; }

[xssFilter]
public string? roomno{ get; set; }

[xssFilter]
public string cleaningstatus{ get; set; }

[xssFilter]
public string? supervisor{ get; set; }

[xssFilter]
public string? remark{ get; set; }
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
			

			public class HousekeepingModelValidator: AbstractValidator<HousekeepingModel>
			{
					 
					public HousekeepingModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_Housekeeping", () =>
                                    {
                                        {RuleFor(m => m.housekeepingno)
.MaximumLength(256).WithMessage("The allowed length of Housekeeping No is 256 characters or fewer")
;
RuleFor(m => m.cleaningdate)
.NotEmpty().WithMessage("Cleaning Date is required")


;

RuleFor(m => m.cleaningstatus)
.NotEmpty().WithMessage("Cleaning Status is required")
;


}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_Housekeeping", () =>
                                    {
                                        {RuleFor(m => m.housekeepingno)
.MaximumLength(256).WithMessage("The allowed length of Housekeeping No is 256 characters or fewer")
;
RuleFor(m => m.cleaningdate)
.NotEmpty().WithMessage("Cleaning Date is required")


;

RuleFor(m => m.cleaningstatus)
.NotEmpty().WithMessage("Cleaning Status is required")
;


}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
