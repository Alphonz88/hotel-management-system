namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:56:03 AM
			public class ReservationModel
			{

			 public System.Guid ?Reservationid	{ get; set; }
public System.Guid ?tenantid { get; set; }
public String viewertenantids { get; set; }

[xssFilter]
public string? reservationno{ get; set; }

[xssFilter]
public string? guestno{ get; set; }

[DataType(DataType.Date)][ModelBinder(BinderType = typeof(DateTimeModelBinder))][DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]public DateTime? bookingdate	{ get; set; }

[DataType(DataType.Date)][ModelBinder(BinderType = typeof(DateTimeModelBinder))][DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]public DateTime? checkindate	{ get; set; }

[DataType(DataType.Date)][ModelBinder(BinderType = typeof(DateTimeModelBinder))][DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]public DateTime? checkoutdate	{ get; set; }

[xssFilter]
public string? reservationstatus{ get; set; }

[xssFilter]
public string? roomtype{ get; set; }

[xssFilter]
public string? noofguests{ get; set; }
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
			

			public class ReservationModelValidator: AbstractValidator<ReservationModel>
			{
					 
					public ReservationModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_reservation", () =>
                                    {
                                        {

RuleFor(m => m.bookingdate)


;
RuleFor(m => m.checkindate)


;
RuleFor(m => m.checkoutdate)


;



}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_reservation", () =>
                                    {
                                        {

RuleFor(m => m.bookingdate)


;
RuleFor(m => m.checkindate)


;
RuleFor(m => m.checkoutdate)


;



}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
