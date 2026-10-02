namespace HotelManagementSystem1.Models{
			using System;
			using System.ComponentModel.DataAnnotations;
			using Microsoft.AspNetCore.Mvc;
			using System.Collections.Generic;
			using FluentValidation;
			using System.Linq;
			//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:30:56 AM
			public class RoomModel
			{

			 public System.Guid ?Roomid	{ get; set; }
public System.Guid ?tenantid { get; set; }
public String viewertenantids { get; set; }

public int? roomno{ get; set; }

public int? floornumber{ get; set; }

[xssFilter]
public string? capacitymaxnoofpersons{ get; set; }

[xssFilter]
public string? bedtype{ get; set; }

[xssFilter]
public string? pricepernight{ get; set; }

[xssFilter]
public string? availablestauts{ get; set; }

public Guid? roomtype	{ get; set; }

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
			

			public class RoomModelValidator: AbstractValidator<RoomModel>
			{
					 
					public RoomModelValidator()
					{

						 When(model => model.craftmyapp_actionmethodname == "Add_Room", () =>
                                    {
                                        {RuleFor(m => m.roomno)
.LessThanOrEqualTo(99999999).WithMessage("Room No should be LessThanOrEqualTo 99999999")

;
RuleFor(m => m.floornumber)
.LessThanOrEqualTo(99999999).WithMessage("Floor Number should be LessThanOrEqualTo 99999999")

;






}

                                    });
When(model => model.craftmyapp_actionmethodname == "Update_Room", () =>
                                    {
                                        {RuleFor(m => m.roomno)
.LessThanOrEqualTo(99999999).WithMessage("Room No should be LessThanOrEqualTo 99999999")

;
RuleFor(m => m.floornumber)
.LessThanOrEqualTo(99999999).WithMessage("Floor Number should be LessThanOrEqualTo 99999999")

;






}

                                    });

						 
						
					}

			}

                

                

                
 

                

                

        

			}
