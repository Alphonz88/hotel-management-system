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

                
                
                
				//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:11 AM
				
                
                
                
                
				public class AlertTemplatesController : BaseController
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
                    private readonly ILogger<AlertTemplatesController> _logger;
                    
                    
                    
					public AlertTemplatesController(IConfiguration configuration,IHttpContextAccessor accessor,IOptions<ApiSettings> ApiSettings, IOptions<MailSettings> MailSettings, IWebHostEnvironment env, ILogger<AlertTemplatesController> logger):base( configuration)
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
	              

					
			  public virtual IActionResult Create_Alert_Templates()
			  {
					return View();
			  }	
			  [HttpPost()]
			public virtual async Task<string> Create_Alert_Templates(AlertTemplatesModel model, IFormCollection collection)
			{
				string strReturnMessage = "";
				
				try
				{
					ModelState.Remove("AlertTemplatesid");
					ModelState.Remove("createduser");
                    ModelState.Remove("craftmyapp_actionmethodname");
                    model.craftmyapp_actionmethodname="Create_Alert_Templates";
					if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
								model.createduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
								else
								return "Session Expired";                    
					
                   
					
			 	    
					 if (ModelState.IsValid)
					 {
							 AlertTemplatesModelValidator validator = new AlertTemplatesModelValidator();
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
								 model.AlertTemplatesid =Guid.NewGuid(); 
                                 
                                  
								 
                                 strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/AlertTemplates/Create_Alert_Templates", model);
                                    
								 
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
                 
                 _logger.LogError(ex,"An exception occurred in - AlertTemplates / Create_Alert_Templates, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
               
				 strReturnMessage = ex.Message;
			 }
		     ViewData["message"] = strReturnMessage;
			 if(strReturnMessage.Replace("\"", "").Contains("201.1")){
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

				
			  public virtual async Task<IActionResult> Update_Alert_Templates(string AlertTemplatesid)
			  {

                    string redirectTo="";
                    if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
                            DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
                            DataView dv = new DataView(HotelManagementSystem1role_JSON);
                            dv.RowFilter = "controllername='AlertTemplates' AND viewname='list'";

                            if(dv.Count  >0){
                                redirectTo = dv[0]["actionmethodname"] as string;
							 
                            }

                            try{
                                     var jsonObjAlertTemplates = await ApiClient.Get_ApiValues(getHttpClient(), "api/AlertTemplates/getById_AlertTemplates?AlertTemplatesid="+AlertTemplatesid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
                                if(jsonObjAlertTemplates.Length > 2)
                                {
                                  
                                    var model = JsonConvert.DeserializeObject<AlertTemplatesModel>(jsonObjAlertTemplates);


                
                                     
                                    return View("Create_Alert_Templates", model);
                                }
                                else
                                {
                    
                                    TempData["message"] = "Data Not Found - Contact Administrator";
                                    return RedirectToAction(redirectTo);
						 
                                }

                            }catch(Exception ex){
                               _logger.LogError(ex,"An exception occurred in - AlertTemplates / Update_Alert_Templates, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                                TempData["errMessage"] = "Error while fetching data - Contact Administrator";
                                return RedirectToAction(redirectTo);
                            }

                    }
                    TempData["errMessage"] = "Session Expired";
                    return RedirectToAction("Logout", "users");
                }	
			  [HttpPost()]
				public virtual async Task<string> Update_Alert_Templates(AlertTemplatesModel model, IFormCollection collection)
				{
					string strReturnMessage = "";
					try
					{
							ModelState.Remove("AlertTemplatesid");
                            ModelState.Remove("craftmyapp_actionmethodname");
                             model.craftmyapp_actionmethodname="Update_Alert_Templates";
							
							
							if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
					model.modifieduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
					else
					return "Session Expired";
							
                            
                            
							if (ModelState.IsValid)
							{
									AlertTemplatesModelValidator validator = new AlertTemplatesModelValidator();
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
                                        
										
                                        
                                        
                                        strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/AlertTemplates/Update_Alert_Templates", model);
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
                      _logger.LogError(ex,"An exception occurred in - AlertTemplates / Update_Alert_Templates, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
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
public virtual async Task<IActionResult> Remove_Alert_Templates(string AlertTemplatesid)
			{
				string message = "";
				try
				{
						message = await ApiClient.Get_ApiValues(getHttpClient(), "api/AlertTemplates/Remove_Alert_Templates?AlertTemplatesid="+AlertTemplatesid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
						 if(message.Replace("\"","").Contains("201.1"))
						{
							TempData["message"] = "Success";

						}else{
							TempData["errMessage"] = message.Replace("\"","");
						}
						
				
				
				}
				catch (Exception ex)
				{
                     _logger.LogError(ex,"An exception occurred in - AlertTemplates / Remove_Alert_Templates, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                
					 TempData["errMessage"] = ex.Message;
					 message = ex.Message;
				}

				string redirectTo="";
						if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
					DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
						 DataView dv = new DataView(HotelManagementSystem1role_JSON);
						 dv.RowFilter = "controllername='AlertTemplates' AND viewname='list'";

						if(dv.Count  >0){
						    redirectTo = dv[0]["actionmethodname"] as string;
							 
						}

					}
				
				return RedirectToAction(redirectTo);
			}

                                        public virtual IActionResult Detail_Alert_Templates()
                                        {
                                            return View();
                                        }

			public virtual IActionResult Alert_Templates_List()
			{
				return View();
			}
				
			[HttpGet()]
			public virtual async Task<string> get_Alert_Templates_List(string entityname
,string entityaction
,string alerttype
)
			{
				
				return await ApiClient.Get_ApiValues(getHttpClient(), "api/AlertTemplates/Alert_Templates_List?entityname="+entityname+"&entityaction="+entityaction+"&alerttype="+alerttype+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			}
			 

				
			  public virtual async Task<string> getById_allinfo_AlertTemplates(string AlertTemplatesid)
			  {
					return await ApiClient.Get_ApiValues(getHttpClient(), "api/AlertTemplates/getById_allinfo_AlertTemplates?AlertTemplatesid="+AlertTemplatesid);
					 
			  }






                    
                     
                        

				}


			}
