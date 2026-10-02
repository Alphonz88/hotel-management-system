namespace Admin.Controllers
			{
				using System;
				using System.Data;
				using System.Linq;
				using Microsoft.AspNetCore.Mvc;
				using Newtonsoft.Json;
				using System.Net.Http;
				using System.Net.Http.Formatting;
				using System.Threading.Tasks;
				using System.Net.Http.Headers;
				using Microsoft.Extensions.Options;
				using Microsoft.AspNetCore.Http;
				using System.Collections.Generic;
				using System.IO;
				using Microsoft.AspNetCore.Hosting;
				using System.Net;
				using FluentValidation.Results;
				using HotelManagementSystem1.Models;
				using Microsoft.AspNetCore.Mvc.Infrastructure;
				using Microsoft.AspNetCore.HttpOverrides;
                using Microsoft.Extensions.Configuration;
                using Microsoft.Extensions.Logging;
                using System.Threading;
	            using System.Globalization;
                using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

                
                
                
				//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:50 AM
				
                
                
                
                
				public class InvoiceController : BaseController
				{	 
					private IWebHostEnvironment hostingEnv;
					private IOptions<ApiSettings> _balSettings;
                    private IOptions<MailSettings> _mailSettings;
					private string  url = "";
					private string  baseUrl = "";
                    private string  adminUrl = "";
                    private string  clientUrl = "";
                    private string  accesskey = "";
					private IHttpContextAccessor _accessor;
                    public IConfiguration Configuration { get; }
                    private readonly ILogger<InvoiceController> _logger;
                    
                    
                    
					public InvoiceController(IConfiguration configuration,IHttpContextAccessor accessor,IOptions<ApiSettings> ApiSettings, IOptions<MailSettings> MailSettings, IWebHostEnvironment env, ILogger<InvoiceController> logger):base( configuration)
					{
                        _logger = logger;
						this.hostingEnv = env;
						_balSettings = ApiSettings;
                        _mailSettings = MailSettings;
						url = _balSettings.Value.apiURL;
						baseUrl = _balSettings.Value.baseURL;
                        adminUrl = _balSettings.Value.adminURL;
                        clientUrl = _balSettings.Value.clientURL;
                        accesskey = _balSettings.Value.accesskey;
 
						_accessor = accessor;
                        Configuration = configuration;
                        
					}
						 
 
                     public virtual IActionResult audit()
			         {
					        return View();
			         }
	              

					
			  public virtual IActionResult Add_Invoice()
			  {
					return View();
			  }	
			  [HttpPost()]
			public virtual async Task<string> Add_Invoice(InvoiceModel model, IFormCollection collection)
			{
				string strReturnMessage = "";
				
				try
				{
					ModelState.Remove("Invoiceid");
					ModelState.Remove("createduser");
                    ModelState.Remove("craftmyapp_actionmethodname");
                    model.craftmyapp_actionmethodname="Add_Invoice";
					if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
								model.createduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
								else
								return "Session Expired";                    
					
                   
					
			 	    
					 if (ModelState.IsValid)
					 {
							 InvoiceModelValidator validator = new InvoiceModelValidator();
							 ValidationResult results = validator.Validate(model);
							 if (!results.IsValid)
							 {
								 var errorCollection = string.Join(" | ", results.Errors.Select(e => e.ErrorMessage.Replace("{propertyName}",e.PropertyName)));
								 strReturnMessage = errorCollection.ToString();
								 foreach (var failure in results.Errors)
								 {
									ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
								 }
							 }
							 else
							 {
								 model.Invoiceid =Guid.NewGuid(); 
                                 
                                  
								 
                                 strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/Invoice/Add_Invoice", model);
                                    
								 
							 }
					 }
					 else
					 {
							   var errorMessages = ModelState.Where(entry => entry.Value.Errors.Any()).SelectMany(entry => entry.Value.Errors.Select(error => $"{entry.Key}: {error.ErrorMessage}"));
                               var errorCollection = string.Join(" | ", errorMessages);
                               strReturnMessage = errorCollection;
					 }
			 }
			 catch (Exception ex)
			 {
                 
                 _logger.LogError(ex,"An exception occurred in - Invoice / Add_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
               
				 strReturnMessage = ex.Message;
			 }
		     ViewData["message"] = strReturnMessage;
			 if(strReturnMessage.Replace("\"", "").Contains("201.1")){
				 TempData["message"] = "Success";
				 MailSender maillog = new MailSender();
                    bool mailsent = await maillog.sendNotification("Invoice"
                    , "ReadyForReview"
                    , model.Invoiceid.ToString()
                    , _mailSettings
                    , model.createduser.ToString()
                    , client);
                
				return "Success";
			 }
              else if (strReturnMessage.StartsWith("BadRequest", StringComparison.OrdinalIgnoreCase))
			{
				strReturnMessage= strReturnMessage.Replace("\"", "").Replace("BadRequest :","");
				TempData["message"] = strReturnMessage;

				return strReturnMessage;
			}
             else{
				  if(strReturnMessage=="401.1")
				  	 	 strReturnMessage = "Authorization Failed";

				  return strReturnMessage;
			 }
 
		   }

				
			  public virtual async Task<IActionResult> Update_Invoice(string Invoiceid)
			  {

                    string redirectTo="";
                    if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
                            DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
                            DataView dv = new DataView(HotelManagementSystem1role_JSON);
                            dv.RowFilter = "controllername='Invoice' AND viewname='list'";

                            if(dv.Count  >0){
                                redirectTo = dv[0]["actionmethodname"] as string;
							 
                            }

                            try{
                                     var jsonObjInvoice = await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/getById_Invoice?Invoiceid="+Invoiceid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
                                if(jsonObjInvoice.Length > 2)
                                {
                                  
                                    var model = JsonConvert.DeserializeObject<InvoiceModel>(jsonObjInvoice);


                
                                     
                                    return View("Add_Invoice", model);
                                }
                                else
                                {
                    
                                    TempData["message"] = "Data Not Found - Contact Administrator";
                                    return RedirectToAction(redirectTo);
						 
                                }

                            }catch(Exception ex){
                               _logger.LogError(ex,"An exception occurred in - Invoice / Update_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                                TempData["errMessage"] = "Error while fetching data - Contact Administrator";
                                return RedirectToAction(redirectTo);
                            }

                    }
                    TempData["errMessage"] = "Session Expired";
                    return RedirectToAction("Logout", "users");
                }	
			  [HttpPost()]
				public virtual async Task<string> Update_Invoice(InvoiceModel model, IFormCollection collection)
				{
					string strReturnMessage = "";
					try
					{
							ModelState.Remove("Invoiceid");
                            ModelState.Remove("craftmyapp_actionmethodname");
                             model.craftmyapp_actionmethodname="Update_Invoice";
							
							
							if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
					model.modifieduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
					else
					return "Session Expired";
							
                            
                            
							if (ModelState.IsValid)
							{
									InvoiceModelValidator validator = new InvoiceModelValidator();
									ValidationResult results = validator.Validate(model);
									if (!results.IsValid)
									{
										var errorCollection = string.Join(" | ", results.Errors.Select(e => e.ErrorMessage.Replace("{propertyName}",e.PropertyName)));
										strReturnMessage = errorCollection.ToString();
										foreach (var failure in results.Errors)
										{
											ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
										}
									}
									else
									{
                                        
										
                                        
                                        
                                        strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/Invoice/Update_Invoice", model);
 									}
							}
							else
							{
									var errorMessages = ModelState.Where(entry => entry.Value.Errors.Any()).SelectMany(entry => entry.Value.Errors.Select(error => $"{entry.Key}: {error.ErrorMessage}"));
                               var errorCollection = string.Join(" | ", errorMessages);
                               strReturnMessage = errorCollection;
							}
					}
					catch (Exception ex)
					{
                      _logger.LogError(ex,"An exception occurred in - Invoice / Update_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
						strReturnMessage = ex.Message;
					}
					ViewData["message"] = strReturnMessage;
					    if(strReturnMessage.Replace("\"", "")=="201.1"){
							TempData["message"] = "Success";
							
							return "Success";
						}
                        else if (strReturnMessage.StartsWith("BadRequest", StringComparison.OrdinalIgnoreCase))
			{
				strReturnMessage= strReturnMessage.Replace("\"", "").Replace("BadRequest :","");
				TempData["message"] = strReturnMessage;

				return strReturnMessage;
			}
                         else{
							if(strReturnMessage=="401.1")
									strReturnMessage = "Authorization Failed";

							return strReturnMessage;
						}
		
				}
public virtual async Task<IActionResult> Remove_Invoice(string Invoiceid)
			{
				string message = "";
				try
				{
						message = await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/Remove_Invoice?Invoiceid="+Invoiceid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
						 if(message.Replace("\"","").Contains("201.1"))
						{
							TempData["message"] = "Success";

						}else{
							TempData["errMessage"] = message.Replace("\"","");
						}
						
				
				
				}
				catch (Exception ex)
				{
                     _logger.LogError(ex,"An exception occurred in - Invoice / Remove_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                
					 TempData["errMessage"] = ex.Message;
					 message = ex.Message;
				}

				string redirectTo="";
						if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
					DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
						 DataView dv = new DataView(HotelManagementSystem1role_JSON);
						 dv.RowFilter = "controllername='Invoice' AND viewname='list'";

						if(dv.Count  >0){
						    redirectTo = dv[0]["actionmethodname"] as string;
							 
						}

					}
				
				return RedirectToAction(redirectTo);
			}

			public virtual IActionResult Added_Invoice()
			{
				return View();
			}
				
			[HttpGet()]
			public virtual async Task<string> get_Added_Invoice(string verifiedstatus
)
			{
				
				return await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/Added_Invoice?verifiedstatus="+verifiedstatus+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			}
			  
											[HttpGet()]
											public virtual async Task<string> get_all_Guest(string tenantid)
											{
											 
											return await ApiClient.Get_ApiValues(getHttpClient(), "api/Guest/get_all_Guest?tenantid="+tenantid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
											}
											 
											[HttpGet()]
											public virtual async Task<string> get_all_Reservation(string tenantid)
											{
											 
											return await ApiClient.Get_ApiValues(getHttpClient(), "api/Reservation/get_all_Reservation?tenantid="+tenantid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
											}
											

                                        public virtual IActionResult MakerDetail_Invoice()
                                        {
                                            return View();
                                        }

			public virtual IActionResult Invoice_for_Review()
			{
				return View();
			}
				
			[HttpGet()]
			public virtual async Task<string> get_Invoice_for_Review(string verifiedstatus
)
			{
				
				return await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/Invoice_for_Review?verifiedstatus="+verifiedstatus+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			}
			 

			 
			[HttpGet()]
			public virtual async Task<string> count_of_Invoice()
			{
				
				return await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/count_of_Invoice?loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			}
			 

            [HttpPost()]
            public virtual async Task<string> verify_Invoice([FromBody]InvoiceReviewModel model)
			{
				string message = "";
				try
				{
					 	message = await ApiClient.Post_ApiValuesGetString(getHttpClient(), "api/Invoice/verify_Invoice", model);
						if(message.Replace("\"","")=="201.1")
						{
							TempData["message"] = "Success";

						}else{
							TempData["errMessage"] = message.Replace("\"","");
						}

						message=message.Replace("\"","");
						
				
				
				}
				catch (Exception ex)
				{
                    
                      _logger.LogError(ex,"An exception occurred in - Invoice / verify_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                  
					 TempData["errMessage"] = ex.Message;
					 message = ex.Message;
				}
 
				
				return message;
			}

                                        public virtual IActionResult CheckerDetail_Invoice()
                                        {
                                            return View();
                                        }

			public virtual IActionResult Invoice_List()
			{
				return View();
			}
				
			[HttpGet()]
			public virtual async Task<string> get_Invoice_List()
			{
				
				return await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/Invoice_List?loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			}
			 

                                        public virtual IActionResult ApprovedDetail_Invoice()
                                        {
                                            return View();
                                        }

				
			  public virtual async Task<string> getById_allinfo_Invoice(string Invoiceid)
			  {
					return await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/getById_allinfo_Invoice?Invoiceid="+Invoiceid);
					 
			  }





[HttpGet()]
			        public virtual async Task<string> metrics_of_Invoice()
			        {
				
				        return await ApiClient.Get_ApiValues(getHttpClient(), "api/Invoice/metrics_of_Invoice?loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			        }

                    
                     
                        

				}


			}
