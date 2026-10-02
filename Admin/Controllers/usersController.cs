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

                
                
                
				//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:18:38 AM
				
                
                
                
                
				public class usersController : BaseController
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
                    private readonly ILogger<usersController> _logger;
                    
                    
                    
					public usersController(IConfiguration configuration,IHttpContextAccessor accessor,IOptions<ApiSettings> ApiSettings, IOptions<MailSettings> MailSettings, IWebHostEnvironment env, ILogger<usersController> logger):base( configuration)
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
	              

					
			  public virtual IActionResult CreatePlatform()
			  {
					return View();
			  }	
			  [HttpPost()]
			public virtual async Task<string> CreatePlatform(usersModel model, IFormCollection collection)
			{
				string strReturnMessage = "";
				
				try
				{
					ModelState.Remove("usersid");
					ModelState.Remove("createduser");
                    ModelState.Remove("craftmyapp_actionmethodname");
                    model.craftmyapp_actionmethodname="CreatePlatform";
					                    
					ModelState.Remove("profilepicture");

                   
					
			 	    
					 if (ModelState.IsValid)
					 {
							 usersModelValidator validator = new usersModelValidator();
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
								 model.usersid =Guid.NewGuid(); 
                                 model.profilepicture = collection["profilepicture_existing"];

                                  var files = Request.Form.Files;
foreach (var file in files) 
{
var filename = ContentDispositionHeaderValue
.Parse(file.ContentDisposition)
.FileName
.Trim('"'); 
string fileExtention = "." + filename.Split('.').Last(); 
Random rnd = new Random();
string uploadFileName = System.Text.RegularExpressions.Regex.Replace(filename.Split('.').First(), @"[^0-9a-zA-Z_.]+", "").Replace(" ", String.Empty)+"_"+"users_" +rnd.Next(1, 10000).ToString() + DateTime.Now.ToString("ddMMyyHHmmss")+ fileExtention;
if (fileExtention != ".")
{
var filanameFolder = hostingEnv.WebRootPath +  "/uploads/"; 
if (!Directory.Exists(filanameFolder))
{
Directory.CreateDirectory(filanameFolder);
}
filename  = filanameFolder + uploadFileName;
using (FileStream fs = System.IO.File.Create(filename))
{
file.CopyTo(fs);
fs.Flush();
}
if (file.Name == "profilepicture")
{
uploadFileName = baseUrl + "/uploads/" + uploadFileName;
model.profilepicture = uploadFileName;
}
}
}

								 
                                 strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/users/Register_Profile", model);
                                    
								 
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
                 
                 _logger.LogError(ex,"An exception occurred in - users / CreatePlatform, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
               
				 strReturnMessage = ex.Message;
			 }
		     ViewData["message"] = strReturnMessage;
			 if(strReturnMessage.Replace("\"", "").Contains("201.1")){
				 TempData["message"] = "Success";
				 
                
                        MailSender maillog = new MailSender();
                        bool userMailsent = await maillog.sendMail("users"
                            , "registration"
                            , model.tenantid.ToString()
                            , "registration"
                            , _mailSettings
                            , "13EF2D93-CD5C-49DB-AE70-8FB33399275C"
                            , client, model.username, model.userpassword, baseUrl);

                        if (userMailsent)
                        {
                            TempData["message"] = "User Info Sent to your registered email";
                        }
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

			  public virtual IActionResult Register_Profile()
			  {
					return View();
			  }	
			  [HttpPost()]
			public virtual async Task<string> Register_Profile(usersModel model, IFormCollection collection)
			{
				string strReturnMessage = "";
				
				try
				{
					ModelState.Remove("usersid");
					ModelState.Remove("createduser");
                    ModelState.Remove("craftmyapp_actionmethodname");
                    model.craftmyapp_actionmethodname="Register_Profile";
					                    
					ModelState.Remove("profilepicture");

                   
					
			 	    
					 if (ModelState.IsValid)
					 {
							 usersModelValidator validator = new usersModelValidator();
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
								 model.usersid =Guid.NewGuid(); 
                                 model.profilepicture = collection["profilepicture_existing"];

                                  var files = Request.Form.Files;
foreach (var file in files) 
{
var filename = ContentDispositionHeaderValue
.Parse(file.ContentDisposition)
.FileName
.Trim('"'); 
string fileExtention = "." + filename.Split('.').Last(); 
Random rnd = new Random();
string uploadFileName = System.Text.RegularExpressions.Regex.Replace(filename.Split('.').First(), @"[^0-9a-zA-Z_.]+", "").Replace(" ", String.Empty)+"_"+"users_" +rnd.Next(1, 10000).ToString() + DateTime.Now.ToString("ddMMyyHHmmss")+ fileExtention;
if (fileExtention != ".")
{
var filanameFolder = hostingEnv.WebRootPath +  "/uploads/"; 
if (!Directory.Exists(filanameFolder))
{
Directory.CreateDirectory(filanameFolder);
}
filename  = filanameFolder + uploadFileName;
using (FileStream fs = System.IO.File.Create(filename))
{
file.CopyTo(fs);
fs.Flush();
}
if (file.Name == "profilepicture")
{
uploadFileName = baseUrl + "/uploads/" + uploadFileName;
model.profilepicture = uploadFileName;
}
}
}

								 
                                 strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/users/Register_Profile", model);
                                    
								 
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
                 
                 _logger.LogError(ex,"An exception occurred in - users / Register_Profile, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
               
				 strReturnMessage = ex.Message;
			 }
		     ViewData["message"] = strReturnMessage;
			 if(strReturnMessage.Replace("\"", "").Contains("201.1")){
				 TempData["message"] = "Success";
				 
                
                        MailSender maillog = new MailSender();
                        bool userMailsent = await maillog.sendMail("users"
                            , "registration"
                            , model.tenantid.ToString()
                            , "registration"
                            , _mailSettings
                            , "13EF2D93-CD5C-49DB-AE70-8FB33399275C"
                            , client, model.username, model.userpassword, baseUrl);

                        if (userMailsent)
                        {
                            TempData["message"] = "User Info Sent to your registered email";
                        }
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

				
			  public virtual async Task<IActionResult> EditPlatform(string usersid)
			  {

                    string redirectTo="";
                    if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
                            DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
                            DataView dv = new DataView(HotelManagementSystem1role_JSON);
                            dv.RowFilter = "controllername='users' AND viewname='list'";

                            if(dv.Count  >0){
                                redirectTo = dv[0]["actionmethodname"] as string;
							 
                            }

                            try{
                                     var jsonObjusers = await ApiClient.Get_ApiValues(getHttpClient(), "api/users/getById_users?usersid="+usersid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
                                if(jsonObjusers.Length > 2)
                                {
                                  
                                    var model = JsonConvert.DeserializeObject<usersModel>(jsonObjusers);


                
                                     
                                    return View(model);
                                }
                                else
                                {
                    
                                    TempData["message"] = "Data Not Found - Contact Administrator";
                                    return RedirectToAction(redirectTo);
						 
                                }

                            }catch(Exception ex){
                               _logger.LogError(ex,"An exception occurred in - users / EditPlatform, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                                TempData["errMessage"] = "Error while fetching data - Contact Administrator";
                                return RedirectToAction(redirectTo);
                            }

                    }
                    TempData["errMessage"] = "Session Expired";
                    return RedirectToAction("Logout", "users");
                }	
			  [HttpPost()]
				public virtual async Task<string> EditPlatform(usersModel model, IFormCollection collection)
				{
					string strReturnMessage = "";
					try
					{
							ModelState.Remove("usersid");
                            ModelState.Remove("craftmyapp_actionmethodname");
                             model.craftmyapp_actionmethodname="EditPlatform";
							
							model.userrole =collection["userrole"];
							if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
					model.modifieduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
					else
					return "Session Expired";
							
                            model.profilepicture = collection["profilepicture_existing"];

                            ModelState.Remove("profilepicture");

							if (ModelState.IsValid)
							{
									usersModelValidator validator = new usersModelValidator();
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
                                        
										var files = Request.Form.Files;
foreach (var file in files) 
{
var filename = ContentDispositionHeaderValue
.Parse(file.ContentDisposition)
.FileName
.Trim('"'); 
string fileExtention = "." + filename.Split('.').Last(); 
Random rnd = new Random();
string uploadFileName = System.Text.RegularExpressions.Regex.Replace(filename.Split('.').First(), @"[^0-9a-zA-Z_.]+", "").Replace(" ", String.Empty)+"_"+"users_" +rnd.Next(1, 10000).ToString() + DateTime.Now.ToString("ddMMyyHHmmss")+ fileExtention;
if (fileExtention != ".")
{
var filanameFolder = hostingEnv.WebRootPath +  "/uploads/"; 
if (!Directory.Exists(filanameFolder))
{
Directory.CreateDirectory(filanameFolder);
}
filename  = filanameFolder + uploadFileName;
using (FileStream fs = System.IO.File.Create(filename))
{
file.CopyTo(fs);
fs.Flush();
}
if (file.Name == "profilepicture")
{
uploadFileName = baseUrl + "/uploads/" + uploadFileName;
model.profilepicture = uploadFileName;
}
}
}

                                        
                                        
                                        strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/users/Update_Profile", model);
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
                      _logger.LogError(ex,"An exception occurred in - users / EditPlatform, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
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

				
			  public virtual async Task<IActionResult> UpdateProfile(string usersid)
			  {

                    string redirectTo="";
                    if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
                            DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
                            DataView dv = new DataView(HotelManagementSystem1role_JSON);
                            dv.RowFilter = "controllername='users' AND viewname='list'";

                            if(dv.Count  >0){
                                redirectTo = dv[0]["actionmethodname"] as string;
							 
                            }

                            try{
                                     var jsonObjusers = await ApiClient.Get_ApiValues(getHttpClient(), "api/users/getById_users?usersid="+usersid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
                                if(jsonObjusers.Length > 2)
                                {
                                  
                                    var model = JsonConvert.DeserializeObject<usersModel>(jsonObjusers);


                
                                     
                                    return View(model);
                                }
                                else
                                {
                    
                                    TempData["message"] = "Data Not Found - Contact Administrator";
                                    return RedirectToAction(redirectTo);
						 
                                }

                            }catch(Exception ex){
                               _logger.LogError(ex,"An exception occurred in - users / UpdateProfile, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                                TempData["errMessage"] = "Error while fetching data - Contact Administrator";
                                return RedirectToAction(redirectTo);
                            }

                    }
                    TempData["errMessage"] = "Session Expired";
                    return RedirectToAction("Logout", "users");
                }	
			  [HttpPost()]
				public virtual async Task<string> UpdateProfile(usersModel model, IFormCollection collection)
				{
					string strReturnMessage = "";
					try
					{
							ModelState.Remove("usersid");
                            ModelState.Remove("craftmyapp_actionmethodname");
                             model.craftmyapp_actionmethodname="UpdateProfile";
							
							model.userrole =collection["userrole"];
							if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
					model.modifieduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
					else
					return "Session Expired";
							
                            model.profilepicture = collection["profilepicture_existing"];

                            ModelState.Remove("profilepicture");

							if (ModelState.IsValid)
							{
									usersModelValidator validator = new usersModelValidator();
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
                                        
										var files = Request.Form.Files;
foreach (var file in files) 
{
var filename = ContentDispositionHeaderValue
.Parse(file.ContentDisposition)
.FileName
.Trim('"'); 
string fileExtention = "." + filename.Split('.').Last(); 
Random rnd = new Random();
string uploadFileName = System.Text.RegularExpressions.Regex.Replace(filename.Split('.').First(), @"[^0-9a-zA-Z_.]+", "").Replace(" ", String.Empty)+"_"+"users_" +rnd.Next(1, 10000).ToString() + DateTime.Now.ToString("ddMMyyHHmmss")+ fileExtention;
if (fileExtention != ".")
{
var filanameFolder = hostingEnv.WebRootPath +  "/uploads/"; 
if (!Directory.Exists(filanameFolder))
{
Directory.CreateDirectory(filanameFolder);
}
filename  = filanameFolder + uploadFileName;
using (FileStream fs = System.IO.File.Create(filename))
{
file.CopyTo(fs);
fs.Flush();
}
if (file.Name == "profilepicture")
{
uploadFileName = baseUrl + "/uploads/" + uploadFileName;
model.profilepicture = uploadFileName;
}
}
}

                                        
                                        if(model.profilepicture !=  null && model.profilepicture!="")
                                                    HttpContext.Session.SetString("HotelManagementSystem1profilepicture", model.profilepicture.ToString());
                                        strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/users/Update_Profile", model);
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
                      _logger.LogError(ex,"An exception occurred in - users / UpdateProfile, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
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

				
			  public virtual async Task<IActionResult> Update_Profile(string usersid)
			  {

                    string redirectTo="";
                    if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
                            DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
                            DataView dv = new DataView(HotelManagementSystem1role_JSON);
                            dv.RowFilter = "controllername='users' AND viewname='list'";

                            if(dv.Count  >0){
                                redirectTo = dv[0]["actionmethodname"] as string;
							 
                            }

                            try{
                                     var jsonObjusers = await ApiClient.Get_ApiValues(getHttpClient(), "api/users/getById_users?usersid="+usersid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
                                if(jsonObjusers.Length > 2)
                                {
                                  
                                    var model = JsonConvert.DeserializeObject<usersModel>(jsonObjusers);


                
                                     
                                    return View(model);
                                }
                                else
                                {
                    
                                    TempData["message"] = "Data Not Found - Contact Administrator";
                                    return RedirectToAction(redirectTo);
						 
                                }

                            }catch(Exception ex){
                               _logger.LogError(ex,"An exception occurred in - users / Update_Profile, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                                TempData["errMessage"] = "Error while fetching data - Contact Administrator";
                                return RedirectToAction(redirectTo);
                            }

                    }
                    TempData["errMessage"] = "Session Expired";
                    return RedirectToAction("Logout", "users");
                }	
			  [HttpPost()]
				public virtual async Task<string> Update_Profile(usersModel model, IFormCollection collection)
				{
					string strReturnMessage = "";
					try
					{
							ModelState.Remove("usersid");
                            ModelState.Remove("craftmyapp_actionmethodname");
                             model.craftmyapp_actionmethodname="Update_Profile";
							
							model.userrole =collection["userrole"];
							if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
					model.modifieduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
					else
					return "Session Expired";
							
                            model.profilepicture = collection["profilepicture_existing"];

                            ModelState.Remove("profilepicture");

							if (ModelState.IsValid)
							{
									usersModelValidator validator = new usersModelValidator();
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
                                        
										var files = Request.Form.Files;
foreach (var file in files) 
{
var filename = ContentDispositionHeaderValue
.Parse(file.ContentDisposition)
.FileName
.Trim('"'); 
string fileExtention = "." + filename.Split('.').Last(); 
Random rnd = new Random();
string uploadFileName = System.Text.RegularExpressions.Regex.Replace(filename.Split('.').First(), @"[^0-9a-zA-Z_.]+", "").Replace(" ", String.Empty)+"_"+"users_" +rnd.Next(1, 10000).ToString() + DateTime.Now.ToString("ddMMyyHHmmss")+ fileExtention;
if (fileExtention != ".")
{
var filanameFolder = hostingEnv.WebRootPath +  "/uploads/"; 
if (!Directory.Exists(filanameFolder))
{
Directory.CreateDirectory(filanameFolder);
}
filename  = filanameFolder + uploadFileName;
using (FileStream fs = System.IO.File.Create(filename))
{
file.CopyTo(fs);
fs.Flush();
}
if (file.Name == "profilepicture")
{
uploadFileName = baseUrl + "/uploads/" + uploadFileName;
model.profilepicture = uploadFileName;
}
}
}

                                        
                                        
                                        strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/users/Update_Profile", model);
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
                      _logger.LogError(ex,"An exception occurred in - users / Update_Profile, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
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
public virtual async Task<IActionResult> Suspend_Profile(string usersid)
			{
				string message = "";
				try
				{
						message = await ApiClient.Get_ApiValues(getHttpClient(), "api/users/Suspend_Profile?usersid="+usersid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
						 if(message.Replace("\"","").Contains("201.1"))
						{
							TempData["message"] = "Success";

						}else{
							TempData["errMessage"] = message.Replace("\"","");
						}
						
				
				
				}
				catch (Exception ex)
				{
                     _logger.LogError(ex,"An exception occurred in - users / Suspend_Profile, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                
					 TempData["errMessage"] = ex.Message;
					 message = ex.Message;
				}

				string redirectTo="";
						if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
					DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
						 DataView dv = new DataView(HotelManagementSystem1role_JSON);
						 dv.RowFilter = "controllername='users' AND viewname='list'";

						if(dv.Count  >0){
						    redirectTo = dv[0]["actionmethodname"] as string;
							 
						}

					}
				
				return RedirectToAction(redirectTo);
			}
public virtual async Task<IActionResult> DeletePlatform(string usersid)
			{
				string message = "";
				try
				{
						message = await ApiClient.Get_ApiValues(getHttpClient(), "api/users/Suspend_Profile?usersid="+usersid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
				}
				catch (Exception ex)
				{
                 _logger.LogError(ex,"An exception occurred in - users / DeletePlatform, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
               
				message = ex.Message;
				}
				TempData["message"] = message;
				return RedirectToAction("IndexPlatform");
			}

		  public async Task<string> get_Transcript(string input)
          {
            try
            {
                string targetLanguage = HttpContext.Request.Cookies["HotelManagementSystem1CurrentCulture"];
                     string[] parts = targetLanguage.Split('-');
                    string targetLanguage1 = parts[0]; // This will split target LAguage 

                string apiUrl = $"https://inputtools.google.com/request?text={Uri.EscapeDataString(input)}&ime=transliteration_en_{targetLanguage1}&num=1&cp=0&cs=1&ie=utf-8&oe=utf-8&app=demopage";
                
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Referer", "https://www.google.com/");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var jsonResult = JsonDocument.Parse(responseBody);
                    var root = jsonResult.RootElement;
                   
                        var translations = root[1][0][1];

                        foreach (var translation in translations.EnumerateArray())
                        {
                            var transliteratedText = translation.GetString();
                            Console.WriteLine("Transliterated Text: " + transliteratedText);
                        }

                        // Return the first transliterated text
                        return translations[0].GetString();
                   
                }
            }
            catch(Exception ex)
            {
                return input;

            }
        }
		
			
        public virtual IActionResult SessionExpired()
        {
            TempData["messagereferer"] = "donotrefer";
	        TempData["message"] = "Session Expired , Please login again !";
	        return RedirectToAction("Login");
        }
        public virtual IActionResult AuthorizationFailed()
        {
            TempData["messagereferer"] = "donotrefer";
	        TempData["message"] = "API Authorization failed ,Please login again !";
	        return RedirectToAction("Login");
        }
        public virtual IActionResult RoleAuthorizationFailed()
        {
            TempData["messagereferer"] = "donotrefer";
	        TempData["message"] = "Not authorized to view this page !";
	        return RedirectToAction("Login");
        }
		


			
                        [HttpGet()]
                        public virtual async Task<string> get_roles(string parentname)
                        {
				 
		                        return await ApiClient.Get_ApiValues(getHttpClient(), "api/users/get_roles?roles="+HttpContext.Session.GetString("HotelManagementSystem1userrole") +"&parentname="+ parentname);
				 
			
                        }
                        public async Task<ActionResult> ChangeCurrentCulture(string culture)
                        {

                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
                        HttpResponseMessage response_menu = await ApiClient.GET_ApiValuesGetRespnse(getHttpClient(), "api/users/get_project_Menu?viewactionroles="+ HttpContext.Session.GetString("HotelManagementSystem1userrole") + "&SubSystem=Admin");
            
                        if (response_menu.IsSuccessStatusCode)
                        {

                                string menu_JSON = await response_menu.Content.ReadAsStringAsync();


                            var assembly = typeof(usersController).Assembly;
                            System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("Admin.Resource", assembly);
 
                                List<dynamic> menuItems = JsonConvert.DeserializeObject<List<dynamic>>(menu_JSON);

                            foreach (dynamic menuItem in menuItems)
                            { 
		                        string projectElementName = System.Text.RegularExpressions.Regex.Replace(menuItem.projectelementname.ToString(), @"\s+", "").ToLower();
		                        string translatedProjectElementName = _resourceManager.GetString(projectElementName);
		                        string parentnode = System.Text.RegularExpressions.Regex.Replace(menuItem.parentnode.ToString(), @"\s+", "").ToLower();
		                        string translatedparentnode = _resourceManager.GetString(parentnode);
		                        menuItem.projectelementname = translatedProjectElementName;
		                        menuItem.parentnode = translatedparentnode;
                            }
	                        menu_JSON = JsonConvert.SerializeObject(menuItems);
	                        HttpContext.Session.SetString("HotelManagementSystem1menu_JSON", menu_JSON);

				 


                        }



	                        Response.Cookies.Append("HotelManagementSystem1CurrentCulture", culture, new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddDays(10), // Cookie expires in 1 hour
                            HttpOnly = true // Makes the cookie accessible only through the HTTP protocol, not JavaScript
                        });
                        string referrerUrl = HttpContext.Request.Headers["Referer"];
                        if (referrerUrl != null)
                        {
	                        return Redirect(referrerUrl);
                        }
                        else
                        {
	                        return RedirectToAction("Home", "users");
                        }
                        }
[AllowAnonymous]
            public virtual IActionResult Home()
			{
                HttpContext.Session.SetString("ReferrerUrl", "");
                TempData["messagereferer"] = "donotrefer";

                return RedirectToAction("Login");
            } 
[AllowAnonymous]
            public virtual IActionResult Login()
			{
                HttpContext.Session.SetString("ReferrerUrl", "");
                string referrerUrl = HttpContext.Request.Headers["Referer"];
                if (referrerUrl != null && TempData["messagereferer"] ==null)
                {
                       HttpContext.Session.SetString("ReferrerUrl", referrerUrl);

                }
                return View();
			}
            public virtual IActionResult Logout()
			{
				HttpContext.Session.Clear();
                TempData["messagereferer"] = "donotrefer";
				return RedirectToAction("Login");
			}
            [HttpPost()]
[AllowAnonymous]
			public virtual async Task<IActionResult> Login(userloginModel model)
			{
				string message = "";
				string strReturnMessage = "";
				try
				{


						userlockoutModel objuserlockoutModel=new userlockoutModel();
						objuserlockoutModel.username=model.username;
						objuserlockoutModel.latlan=model.latlan;
						string  remoteipaddress=_accessor.HttpContext.Connection.RemoteIpAddress.ToString();
						objuserlockoutModel.remoteipaddress=remoteipaddress;
						objuserlockoutModel.clientipaddress=model.clientipaddress;
						
						strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/userlockout/verify_userlockout", objuserlockoutModel);
						if(strReturnMessage !="Allow"){
						ViewData["message"] = strReturnMessage;
						return View(model);
						}
                        model.source="Internal";
						HttpResponseMessage response = await ApiClient.Post_ApiValuesGetRespnse(client,"api/users/CheckAuthentication", model);
						
						
						if (response.IsSuccessStatusCode)
						{
						DataTable dt = await response.Content.ReadAsAsync<DataTable>();
						if (dt.Rows.Count >0)
						{
							HttpContext.Session.SetString("HotelManagementSystem1username", dt.Rows[0]["username"].ToString());
							HttpContext.Session.SetString("HotelManagementSystem1loginUserID", dt.Rows[0]["usersid"].ToString());
							
							HttpContext.Session.SetString("HotelManagementSystem1token", dt.Rows[0]["token"].ToString());
							HttpContext.Session.SetString("HotelManagementSystem1userrole", dt.Rows[0]["userrole"].ToString());
                            if(dt.Rows[0]["profilepicture"].ToString()!=null && dt.Rows[0]["profilepicture"].ToString()!="")
                            HttpContext.Session.SetString("HotelManagementSystem1profilepicture", dt.Rows[0]["profilepicture"].ToString());
                            else
                            HttpContext.Session.SetString("HotelManagementSystem1profilepicture", baseUrl+"/Icon_set/User.png");

                            HttpContext.Session.SetString("HotelManagementSystem1tenantlogo", baseUrl+"/images/logo.png");

							
							
							message ="Login Success";

							
HttpContext.Session.SetString("HotelManagementSystem1showtenant", "N");
                         HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", dt.Rows[0]["tenantid"].ToString());
                        if (dt.Rows[0]["viewertenantids"].ToString().Contains(","))
                        {
                            HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                            HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                        }

                                                                               HttpContext.Session.SetString("HotelManagementSystem1choosedtenantname", dt.Rows[0]["tenantname"].ToString());

                                                                         
																			 
                                                                             HttpContext.Session.SetString("natureofbusiness", dt.Rows[0]["natureofbusiness"].ToString());
                                                                    
																			if(dt.Rows[0]["tenantid"].ToString() !="00000000-0000-0000-0000-000000000000")
																			{
                                                                                     HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
																					HttpContext.Session.SetString("module", "client");

                                                                                                   
                                                                                           
                                                                                    if (dt.Rows[0]["tenantname"].ToString() == "")
                                                                                    {
                                                                                        ViewData["message"] = "You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                                                                        return View(model);
                                                                                    }   

																			} else
						                                                    {
                                                                                HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
                                                                                 HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                                                                                                            HttpContext.Session.SetString("module", "admin");

                                                                                HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                                                                             }




                                            

							//ROLE SECTION



							HttpResponseMessage response_roles = await ApiClient.GET_ApiValuesGetRespnse(client,"api/users/get_roleAuthorizations?viewactionroles="+dt.Rows[0]["userrole"].ToString());

							
							if (response_roles.IsSuccessStatusCode)
							{
								
								DataTable dt_roles = await response_roles.Content.ReadAsAsync<DataTable>();
								string role_JSON = await response_roles.Content.ReadAsStringAsync();
                                if(dt_roles.Rows.Count ==0){
                                        ViewData["message"]="You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                        return View(model); 
                                }
            
								HttpContext.Session.SetSession("HotelManagementSystem1roles", dt_roles);
								HttpContext.Session.SetString("HotelManagementSystem1role_JSON", role_JSON);
								

								
							} 
												


							//ROLE SECTION



							//MENU SECTION



							HttpResponseMessage response_menu = await ApiClient.GET_ApiValuesGetRespnse(client,"api/users/get_project_Menu?viewactionroles="+dt.Rows[0]["userrole"].ToString()+"&SubSystem=Admin");
							
							if (response_menu.IsSuccessStatusCode)
							{
								
								DataTable dt_menu = await response_menu.Content.ReadAsAsync<DataTable>();
								string menu_JSON = await response_menu.Content.ReadAsStringAsync();
								HttpContext.Session.SetSession("HotelManagementSystem1menu", dt_menu);
								 var assembly = typeof(usersController).Assembly;
                                    System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("Admin.Resource", assembly);
 
                                     List<dynamic> menuItems = JsonConvert.DeserializeObject<List<dynamic>>(menu_JSON);

                                    foreach (dynamic menuItem in menuItems)
                                    { 
					                    string projectElementName = System.Text.RegularExpressions.Regex.Replace(menuItem.projectelementname.ToString(), @"\s+", "").ToLower();
					                    string translatedProjectElementName = _resourceManager.GetString(projectElementName);
					                    string parentnode = System.Text.RegularExpressions.Regex.Replace(menuItem.parentnode.ToString(), @"\s+", "").ToLower();
					                    string translatedparentnode = _resourceManager.GetString(parentnode);
					                    menuItem.projectelementname = translatedProjectElementName;
					                    menuItem.parentnode = translatedparentnode;
                                    }
				                    menu_JSON = JsonConvert.SerializeObject(menuItems);
			                        HttpContext.Session.SetString("HotelManagementSystem1menu_JSON", menu_JSON);
								

								
							} 
												


							//MENU SECTION
					
						}
						else
						{
							 message = await ApiClient.Post_ApiValuesGetString(client,"api/userlockout/ins_userlockout", objuserlockoutModel);
						
							 
						}
						}
						else
						{
							message ="Response Failed";
						}
				}
				catch (Exception ex)
				{
                      _logger.LogError(ex,"An exception occurred in - users / Login, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                   
					message = ex.Message;
				}
				ViewData["message"] = message;
				if(message=="Login Success"){


                    switch (HttpContext.Session.GetString("HotelManagementSystem1userrole")){
case "Hotel Owners Admin":
                                                                            return RedirectToAction("Check_IN_List","CheckIN",new{@isDashboardView="Y"});
                                                                            break;
case "Platform Admin":
                                                                            return RedirectToAction("Room_List","Room",new{@isDashboardView="Y"});
                                                                            break;
case "Employee":
                                                                            return RedirectToAction("Housekeeping_List","Housekeeping",new{@isDashboardView="Y"});
                                                                            break;
case "Manager":
                                                                            return RedirectToAction("guest_List","Guest",new{@isDashboardView="Y"});
                                                                            break;
case "Client":
                                                                            return RedirectToAction("Feedback_List","Feedback",new{@isDashboardView="Y"});
                                                                            break;
}

					return RedirectToAction("UpdateProfile","users", new {@usersid=HttpContext.Session.GetString("HotelManagementSystem1loginUserID")});
					 
				}else{
				   
					return View(model);
				}

			}//Register_user
       [HttpPost]
        public virtual async Task<String> appregister([FromBody] userloginModel model)
        {
            string message = "";
            string redirecturl = "";

            try
            {
				//IOS , ANDROID and WEB etc
                if (model.source == null || model.source == "")
                {

                    message = "|source is required|";

                }
				//Anything
                if (model.username == null || model.username == "")
                {

                    message += "|username is required|";

                }
				 
				//UUID Of android or IOS 
                if (model.deviceid == null || model.deviceid == "")
                {

                    message += "|deviceid is required|";

                }

                if (model.notificationid == null || model.notificationid == "")
                {

                    message += "|notificationid is required|";

                }


                if (model.accesskey == null || model.accesskey == "")
                {
                    message += "|accesskey is Required|";
                }

                else if (model.accesskey != accesskey)
                {
                    message += "|Invalid accesskey|";
                }
                 
                if (message == "")
                {
                     HttpResponseMessage response = await ApiClient.Post_ApiValuesGetRespnse(getHttpClient(),"api/users/CheckAuthentication", model);
						
                    if (response.IsSuccessStatusCode)
                    {
                        DataTable dt = await response.Content.ReadAsAsync<DataTable>();
                        if (dt.Rows.Count > 0)
                        {

                            message = "Login Success";
                            redirecturl = adminUrl + "/" + "users/toapp?usersid=" + dt.Rows[0]["usersid"].ToString();
                            

                            
HttpContext.Session.SetString("HotelManagementSystem1showtenant", "N");
                         HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", dt.Rows[0]["tenantid"].ToString());
                        if (dt.Rows[0]["viewertenantids"].ToString().Contains(","))
                        {
                            HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                            HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                        }

                                                                               HttpContext.Session.SetString("HotelManagementSystem1choosedtenantname", dt.Rows[0]["tenantname"].ToString());

                                                                         
																			 
                                                                             HttpContext.Session.SetString("natureofbusiness", dt.Rows[0]["natureofbusiness"].ToString());
                                                                    
																			if(dt.Rows[0]["tenantid"].ToString() !="00000000-0000-0000-0000-000000000000")
																			{
                                                                                     HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
																					HttpContext.Session.SetString("module", "client");

                                                                                                   
                                                                                           
                                                                                    if (dt.Rows[0]["tenantname"].ToString() == "")
                                                                                    {
                                                                                         message="You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                                                                        
                                                                                    }   

																			} else
						                                                    {
                                                                                HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
                                                                                 HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                                                                                                            HttpContext.Session.SetString("module", "admin");

                                                                                HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                                                                             }




                                            


                            //ROLE SECTION
                            HttpResponseMessage response_roles = await ApiClient.GET_ApiValuesGetRespnse(client,"api/users/get_roleAuthorizations?viewactionroles="+dt.Rows[0]["userrole"].ToString());

							
							if (response_roles.IsSuccessStatusCode)
							{
								
								DataTable dt_roles = await response_roles.Content.ReadAsAsync<DataTable>();
								if(dt_roles.Rows.Count ==0){
                                        message="You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                         
                                }
            
								 
							}


 

                        }
                        else
                        {
                            message = "user not available";

                        }
                    }
                    else
                    {
                        message = "HotelManagementSystem1's API Response Failed";
                    }
                }
                else
                {

                    message = "Validation Failed : " + message;
                }




            }
            catch (Exception ex)
            {
                
                  _logger.LogError(ex,"An exception occurred in - users / appregister, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                  
                message = ex.Message;
            }

        

            if (message == "Login Success")
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "success",
                    message = "success",
                    redirecturl = redirecturl
                });

            }
            else
            {

                return JsonConvert.SerializeObject(new
                {
                    status = "failed",
                    message = message,
                    redirecturl = ""
                });
            }
        }

		[AllowAnonymous]
        public virtual IActionResult applogin()
        {
            return RedirectToAction("Login");
        }

        //Mobile Or UUID FROM Mobile APP
		//uuid or mobile number is username
[AllowAnonymous]
        [HttpPost]
        public virtual async Task<String> applogin(userloginModel model)
        {
            string message = "";
            string redirecturl = "";

            try
            {

                if (model.source == null || model.source == "")
                {

                    message = "|source required|";

                }

                if (model.username == null || model.username == "")
                {

                    message += "|username required|";

                }

                if (model.accesskey == null || model.accesskey == "")
                {
                    message += "|accesskey Required|";
                }

                else if (model.accesskey != accesskey)
                {

                    Console.WriteLine(model.accesskey);
                    Console.WriteLine(accesskey);

                    message += "|Invalid accesskey|";
                }

           

                if (message == "")
                {
  				 HttpResponseMessage response = await ApiClient.Post_ApiValuesGetRespnse(getHttpClient(),"api/users/CheckAuthentication", model);
				

                    if (response.IsSuccessStatusCode)
                    {
                        DataTable dt = await response.Content.ReadAsAsync<DataTable>();
                        if (dt.Rows.Count > 0)
                        {
                            message = "Login Success";
    
                            redirecturl = adminUrl + "/" + "users/toapp?usersid=" + dt.Rows[0]["usersid"].ToString();
                            

                            
HttpContext.Session.SetString("HotelManagementSystem1showtenant", "N");
                         HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", dt.Rows[0]["tenantid"].ToString());
                        if (dt.Rows[0]["viewertenantids"].ToString().Contains(","))
                        {
                            HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                            HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                        }

                                                                               HttpContext.Session.SetString("HotelManagementSystem1choosedtenantname", dt.Rows[0]["tenantname"].ToString());

                                                                         
																			 
                                                                             HttpContext.Session.SetString("natureofbusiness", dt.Rows[0]["natureofbusiness"].ToString());
                                                                    
																			if(dt.Rows[0]["tenantid"].ToString() !="00000000-0000-0000-0000-000000000000")
																			{
                                                                                     HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
																					HttpContext.Session.SetString("module", "client");

                                                                                                   
                                                                                           
                                                                                    if (dt.Rows[0]["tenantname"].ToString() == "")
                                                                                    {
                                                                                         message="You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                                                                        
                                                                                    }   

																			} else
						                                                    {
                                                                                HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
                                                                                 HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                                                                                                            HttpContext.Session.SetString("module", "admin");

                                                                                HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                                                                             }




                                            
 
                            HttpResponseMessage response_roles = await ApiClient.GET_ApiValuesGetRespnse(client,"api/users/get_roleAuthorizations?viewactionroles="+dt.Rows[0]["userrole"].ToString());

							
							if (response_roles.IsSuccessStatusCode)
							{
								
								DataTable dt_roles = await response_roles.Content.ReadAsAsync<DataTable>();
								if(dt_roles.Rows.Count ==0){
                                        message="You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                         
                                }
            
								 
							}

                         }
                        else
                        {
                            message = "user not available";
  

                        }
                    }
                    else
                    {
                        message = "HotelManagementSystem1's API Response Failed";
                    }
                }
                else
                {

                    message = "Validation Failed : " + message;
                }




            }
            catch (Exception ex)
            {
                
                 _logger.LogError(ex,"An exception occurred in - users / applogin, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                  
                message = ex.Message;
            }

            

            if (message == "Login Success")
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "success",
                    message = "success",
                    redirecturl = redirecturl
                });

            }
            else
            {

                return JsonConvert.SerializeObject(new
                {
                    status = "failed",
                    message = message,
                    redirecturl = ""
                });
            }
        }
 //Go_to_app_with_user_id
        public virtual async Task<IActionResult> toapp(string usersid)
        {
            string message = "";

            userloginModel model = new userloginModel();
            try
            {
                model.username = usersid;
                model.source = "allowbyid";
                HttpResponseMessage response = await ApiClient.Post_ApiValuesGetRespnse(getHttpClient(),"api/users/CheckAuthentication", model);
					
               

                if (response.IsSuccessStatusCode)
                {
                    DataTable dt = await response.Content.ReadAsAsync<DataTable>();
                    if (dt.Rows.Count > 0)
                    {
                            HttpContext.Session.SetString("HotelManagementSystem1username", dt.Rows[0]["username"].ToString());
							HttpContext.Session.SetString("HotelManagementSystem1loginUserID", dt.Rows[0]["usersid"].ToString());
							
							HttpContext.Session.SetString("HotelManagementSystem1token", dt.Rows[0]["token"].ToString());
							HttpContext.Session.SetString("HotelManagementSystem1userrole", dt.Rows[0]["userrole"].ToString());
                            if(dt.Rows[0]["profilepicture"].ToString()!=null && dt.Rows[0]["profilepicture"].ToString()!="")
                            HttpContext.Session.SetString("HotelManagementSystem1profilepicture", dt.Rows[0]["profilepicture"].ToString());
                            else
                            HttpContext.Session.SetString("HotelManagementSystem1profilepicture", baseUrl+"/Icon_set/User.png");

                            HttpContext.Session.SetString("HotelManagementSystem1tenantlogo", baseUrl+"/images/logo.png");
                            message = "Login Success";

                         

							
HttpContext.Session.SetString("HotelManagementSystem1showtenant", "N");
                         HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", dt.Rows[0]["tenantid"].ToString());
                        if (dt.Rows[0]["viewertenantids"].ToString().Contains(","))
                        {
                            HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                            HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                        }

                                                                               HttpContext.Session.SetString("HotelManagementSystem1choosedtenantname", dt.Rows[0]["tenantname"].ToString());

                                                                         
																			 
                                                                             HttpContext.Session.SetString("natureofbusiness", dt.Rows[0]["natureofbusiness"].ToString());
                                                                    
																			if(dt.Rows[0]["tenantid"].ToString() !="00000000-0000-0000-0000-000000000000")
																			{
                                                                                     HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
																					HttpContext.Session.SetString("module", "client");

                                                                                                   
                                                                                           
                                                                                    if (dt.Rows[0]["tenantname"].ToString() == "")
                                                                                    {
                                                                                        ViewData["message"] = "You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                                                                        return View(model);
                                                                                    }   

																			} else
						                                                    {
                                                                                HttpContext.Session.SetString("partyname",  dt.Rows[0]["natureofbusiness"].ToString());
                                                                                 HttpContext.Session.SetString("HotelManagementSystem1showtenant", "Y");
                                                                                                            HttpContext.Session.SetString("module", "admin");

                                                                                HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid", "");
                                                                             }




                                            

							//ROLE SECTION



							HttpResponseMessage response_roles = await ApiClient.GET_ApiValuesGetRespnse(client,"api/users/get_roleAuthorizations?viewactionroles="+dt.Rows[0]["userrole"].ToString());

							
							if (response_roles.IsSuccessStatusCode)
							{
								
								DataTable dt_roles = await response_roles.Content.ReadAsAsync<DataTable>();
								string role_JSON = await response_roles.Content.ReadAsStringAsync();
                                if(dt_roles.Rows.Count ==0){
                                        ViewData["message"]="You are not authorized to access HotelManagementSystem1 , Please contact administrator";
                                        return View(model); 
                                }
            
								HttpContext.Session.SetSession("HotelManagementSystem1roles", dt_roles);
								HttpContext.Session.SetString("HotelManagementSystem1role_JSON", role_JSON);
								

								
							} 
												


							//ROLE SECTION



							//MENU SECTION



							HttpResponseMessage response_menu = await ApiClient.GET_ApiValuesGetRespnse(client,"api/users/get_project_Menu?viewactionroles="+dt.Rows[0]["userrole"].ToString()+"&SubSystem=Admin");
							
							if (response_menu.IsSuccessStatusCode)
							{
								
								DataTable dt_menu = await response_menu.Content.ReadAsAsync<DataTable>();
								string menu_JSON = await response_menu.Content.ReadAsStringAsync();
								HttpContext.Session.SetSession("HotelManagementSystem1menu", dt_menu);
								 var assembly = typeof(usersController).Assembly;
                                    System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("Admin.Resource", assembly);
 
                                     List<dynamic> menuItems = JsonConvert.DeserializeObject<List<dynamic>>(menu_JSON);

                                    foreach (dynamic menuItem in menuItems)
                                    { 
					                    string projectElementName = System.Text.RegularExpressions.Regex.Replace(menuItem.projectelementname.ToString(), @"\s+", "").ToLower();
					                    string translatedProjectElementName = _resourceManager.GetString(projectElementName);
					                    string parentnode = System.Text.RegularExpressions.Regex.Replace(menuItem.parentnode.ToString(), @"\s+", "").ToLower();
					                    string translatedparentnode = _resourceManager.GetString(parentnode);
					                    menuItem.projectelementname = translatedProjectElementName;
					                    menuItem.parentnode = translatedparentnode;
                                    }
				                    menu_JSON = JsonConvert.SerializeObject(menuItems);
			                        HttpContext.Session.SetString("HotelManagementSystem1menu_JSON", menu_JSON);
								

								
							} 
												


							//MENU SECTION
					


                    }
                    else
                    {
                         message = "user not exists";


                    }
                }
                else
                {
                    message = "Response Failed";
                }
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex,"An exception occurred in - users / toapp, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                 
                message = ex.Message;
            }
            TempData["message"] = message;
            if (message == "Login Success")
            {
                 switch (HttpContext.Session.GetString("HotelManagementSystem1userrole")){
case "Hotel Owners Admin":
                                                                            return RedirectToAction("Check_IN_List","CheckIN",new{@isDashboardView="Y"});
                                                                            break;
case "Platform Admin":
                                                                            return RedirectToAction("Room_List","Room",new{@isDashboardView="Y"});
                                                                            break;
case "Employee":
                                                                            return RedirectToAction("Housekeeping_List","Housekeeping",new{@isDashboardView="Y"});
                                                                            break;
case "Manager":
                                                                            return RedirectToAction("guest_List","Guest",new{@isDashboardView="Y"});
                                                                            break;
case "Client":
                                                                            return RedirectToAction("Feedback_List","Feedback",new{@isDashboardView="Y"});
                                                                            break;
}

                 return RedirectToAction("UpdateProfile","users", new {@usersid=HttpContext.Session.GetString("HotelManagementSystem1loginUserID")});
					
            }
            else
            {

                return RedirectToAction("Login");
            }
        }
        public virtual IActionResult forgotpassword()
        {
            HttpContext.Session.SetString("ReferrerUrl", "");
            TempData["messagereferer"] = "donotrefer";
            return View();
        }
        [HttpPost()]
        public virtual async Task<IActionResult> forgotpassword(userloginModel loginmodel)
        {
            string message = "";
            string pwd = "";
            usersModel model = new usersModel();
            try
            {
                loginmodel.source = "Internal";
                HttpResponseMessage response_Password = await ApiClient.Post_ApiValuesGetRespnse(getHttpClient(), "api/users/get_decryptedPassword", loginmodel);
                if (response_Password.IsSuccessStatusCode)
                {
                    message = await response_Password.Content.ReadAsAsync<string>();

                    loginmodel.userpassword = message;
                    pwd = message;
                }
                HttpResponseMessage response = await ApiClient.Post_ApiValuesGetRespnse(client, "api/users/CheckAuthentication", loginmodel);

                if (response.IsSuccessStatusCode)
                {
                    var jsonObj = await response.Content.ReadAsStringAsync();
                    if (jsonObj.Length > 2)
                    {
                        jsonObj = jsonObj.Substring(1, jsonObj.Length - 2);
                        model = JsonConvert.DeserializeObject<usersModel>(jsonObj);
                        message = "Login Success";
                        model.userpassword = pwd;
                    }
                    else
                    {
                        message = "Login Failed";
                    }
                }
                else
                {
                    message = "Response Failed";
                }
            }
            catch (Exception ex)
            {
               
                 _logger.LogError(ex,"An exception occurred in - users / forgotpassword, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
               
                message = ex.Message;
            }
            ViewData["message"] = "User Info not available - Please contact Admin ";
            if (message == "Login Success")
            {

                if (model.emailid != null && model.emailid != "")
                {

                    MailSender maillog = new MailSender();
                    bool mailsent = await maillog.sendMail("users"
                        , "forgotpassword"
                        , model.usersid.ToString()
                        , "Forgot Password"
                        , _mailSettings
                        , "13EF2D93-CD5C-49DB-AE70-8FB33399275C"
                        , client, model.username, model.userpassword);

                    if (mailsent)
                    {
                        ViewData["message"] = "User Info Sent to your registered email";
                    }
                    else
                    {
                        ViewData["message"] = "User Info found - Mail Sending Failed - Please contact Admin ";
                    }


                }
                else
                {
                    ViewData["message"] = "User Info found - Mail Sending Failed - Please contact Admin ";
                }



                return View(loginmodel);
            }
            else
            {
                return View(loginmodel);
            }
;
        }

        public virtual async Task<string> get_Dashboard_Items(string viewactionroles)
		{
			string dashmenu = await ApiClient.Get_ApiValues(getHttpClient(), "api/users/get_Dashboard_Items?viewactionroles=" + HttpContext.Session.GetString("HotelManagementSystem1userrole"));
			if (dashmenu != "")
			{
				var assembly = typeof(usersController).Assembly;
				System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("Admin.Resource", assembly);

				List<dynamic> menuItems = JsonConvert.DeserializeObject<List<dynamic>>(dashmenu);

				foreach (dynamic menuItem in menuItems)
				{
					string actiondisplayname = System.Text.RegularExpressions.Regex.Replace(menuItem.actiondisplayname.ToString(), @"\s+", "").ToLower();
					string translatedactiondisplayname = _resourceManager.GetString(actiondisplayname);
					menuItem.actiondisplayname = translatedactiondisplayname;
				}
				dashmenu = JsonConvert.SerializeObject(menuItems);
				
			}
			return dashmenu;

		}


			public virtual IActionResult List_of_User_Profiles()
			{
				return View();
			}
			public virtual IActionResult IndexPlatform()
					{
						 HttpContext.Session.SetString("HotelManagementSystem1choosedtenantid","");
						return View();
					}	
			[HttpGet()]
			public virtual async Task<string> get_List_of_User_Profiles(string tenantid
)
			{
				
				return await ApiClient.Get_ApiValues(getHttpClient(), "api/users/List_of_User_Profiles?tenantid="+tenantid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID")
);
			}
			 

                                        public virtual IActionResult Detail()
                                        {
                                            return View();
                                        }

				
			  public virtual async Task<IActionResult> ChangePassword(string usersid)
			  {
                    string redirectTo="";
            if(HttpContext.Session.GetString("HotelManagementSystem1role_JSON") != null){
                
			DataTable HotelManagementSystem1role_JSON =HttpContext.Session.GetSession<DataTable>("HotelManagementSystem1roles");
			DataView dv = new DataView(HotelManagementSystem1role_JSON);
			dv.RowFilter = "controllername='users' AND viewname='list'";

			if(dv.Count  >0){
				redirectTo = dv[0]["actionmethodname"] as string;
							 
			}	 
                    try
                        {
                            var jsonObjusers = await ApiClient.Get_ApiValues(getHttpClient(), "api/users/getById_users?usersid="+usersid+"&loginUserID="+HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
                                 
						         var model = JsonConvert.DeserializeObject<usersModel>(jsonObjusers);


                
					      
                     
						   
						     

                            usersChangePasswordModel objModel=new usersChangePasswordModel();     
                            objModel.usersid=model.usersid;
						    return View(objModel);

                        }catch(Exception ex){
                            _logger.LogError(ex,"An exception occurred in - users / ChangePassword, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
                            TempData["errMessage"] = "Error while fetching data - Contact Administrator";
                            return RedirectToAction(redirectTo);
                        }
					 

                }
                TempData["errMessage"] = "Session Expired";
                return RedirectToAction("Logout","users");

			  }	
			  [HttpPost()]
				public virtual async Task<string> ChangePassword(usersChangePasswordModel model, IFormCollection collection)
				{
					string strReturnMessage = "";
					try
					{
							ModelState.Remove("usersid");
							 
							if(HttpContext.Session.GetString("HotelManagementSystem1loginUserID") != null)
					model.modifieduser =new Guid(HttpContext.Session.GetString("HotelManagementSystem1loginUserID"));
					else
					return "Session Expired";
							 
							if (ModelState.IsValid)
							{
									    strReturnMessage = await ApiClient.Post_ApiValuesGetString(getHttpClient(),"api/users/ChangePassword", model);
									  
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
                        _logger.LogError(ex,"An exception occurred in - users / ChangePassword, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
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

				
			  public virtual async Task<string> getById_allinfo_users(string usersid)
			  {
					return await ApiClient.Get_ApiValues(getHttpClient(), "api/users/getById_allinfo_users?usersid="+usersid);
					 
			  }






                    
                     
                        

				}


			}
