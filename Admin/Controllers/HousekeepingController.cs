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

                
                
                
				//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:31 AM
				
                
                
                
                
				public class HousekeepingController : BaseController
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
                    private readonly ILogger<HousekeepingController> _logger;
                    
                    
                    
					public HousekeepingController(IConfiguration configuration,IHttpContextAccessor accessor,IOptions<ApiSettings> ApiSettings, IOptions<MailSettings> MailSettings, IWebHostEnvironment env, ILogger<HousekeepingController> logger):base( configuration)
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
	              

					
			  public virtual IActionResult Add_Housekeeping()
			  {
					return View();
			  }	
			  [HttpPost()]
			public virtual async Task<string> Add_Housekeeping(HousekeepingModel model, IFormCollection collection)
			{
				string strReturnMessage = "";
				
				try
				{
					ModelState.Remove("Housekeepingid");
					ModelState.Remove("createduser");
                    ModelState.Remove("craftmyapp_actionmethodname");
                    model.craftmyapp_actionmethodname="Add_Housekeeping";
					if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
								model.createduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
								else
								return "Session Expired";                    
					
                   
					
			 	    
					 if (ModelState.IsValid)
					 {
							 HousekeepingModelValidator validator = new HousekeepingModelValidator();
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
								 model.Housekeepingid =Guid.NewGuid(); 
                                 
                                  
								 
                                 strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/Housekeeping/Add_Housekeeping", model);
                                    
								 
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
                 
                 _logger.LogError(ex,"An exception occurred in - Housekeeping / Add_Housekeeping, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
               
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

				
			  public virtual async Task<IActionResult> Update_Housekeeping(string Housekeepingid)
			  {

                    string redirectTo="";
                    if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
                            DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
                            DataView dv = new DataView(HotelManagementSystem1role_JSON);
                            dv.RowFilter = "controllername='Housekeeping' AND viewname='list'";

                            if(dv.Count  >0){
                                redirectTo = dv[0]["actionmethodname"] as string;
							 
                            }

                            try{
                                     var jsonObjHousekeeping = await ApiClient.Get_ApiValues(getHttpClient(), "api/Housekeeping/getById_Housekeeping?Housekeepingid="+Housekeepingid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
                                if(jsonObjHousekeeping.Length > 2)
                                {
                                  
                                    var model = JsonConvert.DeserializeObject<HousekeepingModel>(jsonObjHousekeeping);


                
                                     
                                    return View("Add_Housekeeping", model);
                                }
                                else
                                {
                    
                                    TempData["message"] = "Data Not Found - Contact Administrator";
                                    return RedirectToAction(redirectTo);
						 
                                }

                            }catch(Exception ex){
                               _logger.LogError(ex,"An exception occurred in - Housekeeping / Update_Housekeeping, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                                TempData["errMessage"] = "Error while fetching data - Contact Administrator";
                                return RedirectToAction(redirectTo);
                            }

                    }
                    TempData["errMessage"] = "Session Expired";
                    return RedirectToAction("Logout", "users");
                }	
			  [HttpPost()]
				public virtual async Task<string> Update_Housekeeping(HousekeepingModel model, IFormCollection collection)
				{
					string strReturnMessage = "";
					try
					{
							ModelState.Remove("Housekeepingid");
                            ModelState.Remove("craftmyapp_actionmethodname");
                             model.craftmyapp_actionmethodname="Update_Housekeeping";
							
							
							if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
					model.modifieduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
					else
					return "Session Expired";
							
                            
                            
							if (ModelState.IsValid)
							{
									HousekeepingModelValidator validator = new HousekeepingModelValidator();
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
                                        
										
                                        
                                        
                                        strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/Housekeeping/Update_Housekeeping", model);
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
                      _logger.LogError(ex,"An exception occurred in - Housekeeping / Update_Housekeeping, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
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
public virtual async Task<IActionResult> Remove_Housekeeping(string Housekeepingid)
			{
				string message = "";
				try
				{
						message = await ApiClient.Get_ApiValues(getHttpClient(), "api/Housekeeping/Remove_Housekeeping?Housekeepingid="+Housekeepingid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
						 if(message.Replace("\"","").Contains("201.1"))
						{
							TempData["message"] = "Success";

						}else{
							TempData["errMessage"] = message.Replace("\"","");
						}
						
				
				
				}
				catch (Exception ex)
				{
                     _logger.LogError(ex,"An exception occurred in - Housekeeping / Remove_Housekeeping, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                
					 TempData["errMessage"] = ex.Message;
					 message = ex.Message;
				}

				string redirectTo="";
						if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
					DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
						 DataView dv = new DataView(HotelManagementSystem1role_JSON);
						 dv.RowFilter = "controllername='Housekeeping' AND viewname='list'";

						if(dv.Count  >0){
						    redirectTo = dv[0]["actionmethodname"] as string;
							 
						}

					}
				
				return RedirectToAction(redirectTo);
			}

                                        public virtual IActionResult Detail_Housekeeping()
                                        {
                                            return View();
                                        }

			public virtual IActionResult Housekeeping_List()
			{
				return View();
			}
				
			[HttpGet()]
			public virtual async Task<string> get_Housekeeping_List(string tenantid
)
			{
				
				return await ApiClient.Get_ApiValues(getHttpClient(), "api/Housekeeping/Housekeeping_List?tenantid="+tenantid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			}
			  
											[HttpGet()]
											public virtual async Task<string> get_all_Room(string tenantid)
											{
											 
											return await ApiClient.Get_ApiValues(getHttpClient(), "api/Room/get_all_Room?tenantid="+tenantid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
											}
											

				
			  public virtual async Task<string> getById_allinfo_Housekeeping(string Housekeepingid)
			  {
					return await ApiClient.Get_ApiValues(getHttpClient(), "api/Housekeeping/getById_allinfo_Housekeeping?Housekeepingid="+Housekeepingid);
					 
			  }






                    
                     
                        

				}


			}
