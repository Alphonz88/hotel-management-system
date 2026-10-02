namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:38:57 AM
			public class ReceptionistModel
			{

			 public System.Guid ?Receptionistid	{ get; set; }
public System.Guid ?tenantid { get; set; }
public String viewertenantids { get; set; }

[xssFilter]
public string? employeeno{ get; set; }

[xssFilter]
public string? employeename{ get; set; }

[xssFilter]
public string? shift{ get; set; }

[xssFilter]
public string? experience{ get; set; }

[xssFilter]
public string? salary{ get; set; }

[xssFilter]
public string? phonenumber{ get; set; }

[xssFilter]
public string? email{ get; set; }
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
			

			public class ReceptionistModelValidator: AbstractValidator<ReceptionistModel>
			{
					 
					public ReceptionistModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_Receptionist", () =>
                                    {
                                        {




RuleFor(m => m.phonenumber)
.MaximumLength(20).WithMessage("The allowed length of Phone Number is 20 characters or fewer ")

;
RuleFor(m => m.email)
.MaximumLength(128).WithMessage("The allowed length of Email is 128 characters or fewer")
.EmailAddress()

;
}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_Receptionist", () =>
                                    {
                                        {




RuleFor(m => m.phonenumber)
.MaximumLength(20).WithMessage("The allowed length of Phone Number is 20 characters or fewer ")

;
RuleFor(m => m.email)
.MaximumLength(128).WithMessage("The allowed length of Email is 128 characters or fewer")
.EmailAddress()

;
}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
