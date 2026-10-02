namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 7:00:03 AM
			public class FeedbackModel
			{

			 public System.Guid ?Feedbackid	{ get; set; }
public System.Guid ?tenantid { get; set; }
public String viewertenantids { get; set; }

[xssFilter]
public string feedbackno{ get; set; }

[xssFilter]
public string? guestno{ get; set; }

[xssFilter]
public string? staffrating{ get; set; }

[xssFilter]
public string? roomrating{ get; set; }
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
			

			public class FeedbackModelValidator: AbstractValidator<FeedbackModel>
			{
					 
					public FeedbackModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_Feedback", () =>
                                    {
                                        {RuleFor(m => m.feedbackno)
.MaximumLength(256).WithMessage("The allowed length of Feedback No is 256 characters or fewer")
;



}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_Feedback", () =>
                                    {
                                        {RuleFor(m => m.feedbackno)
.MaximumLength(256).WithMessage("The allowed length of Feedback No is 256 characters or fewer")
;



}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
