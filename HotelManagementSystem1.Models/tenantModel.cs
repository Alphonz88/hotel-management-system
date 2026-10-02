namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:09 AM
			public class tenantModel
			{

			 public System.Guid ?tenantid	{ get; set; }

[xssFilter]
public string businessname{ get; set; }

[xssFilter]
public string? natureofbusiness{ get; set; }

[xssFilter]
public string businessemail{ get; set; }

[xssFilter]
public string businessphone{ get; set; }

[xssFilter]
public string? businesswebsite{ get; set; }

[xssFilter]
										
               public string? organizationlogo{ get; set; }

public int? numberofemployees{ get; set; }

[xssFilter]
public string? addressline1{ get; set; }

[xssFilter]
public string? addressline2{ get; set; }

[xssFilter]
public string? city{ get; set; }

[xssFilter]
public string? statename{ get; set; }

[xssFilter]
public string? zip{ get; set; }

[xssFilter]
public string? country{ get; set; }

public Guid? parentid	{ get; set; }

[xssFilter]
public string username{ get; set; }

[xssFilter]
public string password{ get; set; }

[xssFilter]
public string userrole{ get; set; }
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
			

			public class tenantModelValidator: AbstractValidator<tenantModel>
			{
					 
					public tenantModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Create_tenant", () =>
                                    {
                                        {RuleFor(m => m.businessname)
.NotEmpty().WithMessage("Organisation  Name is required")
.MaximumLength(50).WithMessage("The allowed length of Organisation  Name is 50 characters or fewer")
;

RuleFor(m => m.businessemail)
.NotEmpty().WithMessage("Organisation  Email is required")
.MaximumLength(128).WithMessage("The allowed length of Organisation  Email is 128 characters or fewer")
.EmailAddress()

;
RuleFor(m => m.businessphone)
.NotEmpty().WithMessage("Organisation  Phone is required")
.MaximumLength(20).WithMessage("The allowed length of Organisation  Phone is 20 characters or fewer ")

;


RuleFor(m => m.numberofemployees)
.LessThanOrEqualTo(99999999).WithMessage("Number of  Employees should be LessThanOrEqualTo 99999999")

;







RuleFor(m => m.username)
.NotEmpty().WithMessage("Username is required")
.MaximumLength(256).WithMessage("The allowed length of Username is 256 characters or fewer")
;
RuleFor(m => m.password)
.NotEmpty().WithMessage("Password is required")
.MaximumLength(256).WithMessage("The allowed length of Password is 256 characters or fewer")
;
RuleFor(m => m.userrole)
.NotEmpty().WithMessage("User Role is required")
.MaximumLength(256).WithMessage("The allowed length of User Role is 256 characters or fewer")
;
}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_tenant", () =>
                                    {
                                        {RuleFor(m => m.businessname)
.NotEmpty().WithMessage("Organisation  Name is required")
.MaximumLength(50).WithMessage("The allowed length of Organisation  Name is 50 characters or fewer")
;

RuleFor(m => m.businessemail)
.NotEmpty().WithMessage("Organisation  Email is required")
.MaximumLength(128).WithMessage("The allowed length of Organisation  Email is 128 characters or fewer")
.EmailAddress()

;
RuleFor(m => m.businessphone)
.NotEmpty().WithMessage("Organisation  Phone is required")
.MaximumLength(20).WithMessage("The allowed length of Organisation  Phone is 20 characters or fewer ")

;


RuleFor(m => m.numberofemployees)
.LessThanOrEqualTo(99999999).WithMessage("Number of  Employees should be LessThanOrEqualTo 99999999")

;







}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
