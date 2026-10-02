namespace HotelManagementSystem1.Models
								{
									using System.ComponentModel.DataAnnotations;
									using Microsoft.AspNetCore.Mvc;
									//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 6:13:52 AM
									public class lookupsModel
									{
										[Required(ErrorMessage="lookupid is Required")]
										public System.Guid lookupid	{ get; set; }
										public string entityname	{ get; set; }
										public string attributetype	{ get; set; }
										public string fieldname	{ get; set; }
										public string fielddesc	{ get; set; }
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
									}
								}
