namespace HotelManagementSystem1WebApi.Controllers
			{
				using System;
				using System.Data;
				using System.Linq;
                using Newtonsoft.Json.Linq;
				using Microsoft.AspNetCore.Mvc;
				using System.Collections.Generic;
				using Microsoft.Extensions.Options;
				using Microsoft.Extensions.Logging;
				using Microsoft.AspNetCore.Authorization;
				using Microsoft.Extensions.Configuration;
				using System.IdentityModel.Tokens.Jwt;
				using System.Security.Claims;
				using System.Text;
				using Microsoft.IdentityModel.Tokens;
				using HotelManagementSystem1.Models;
				using HotelManagementSystem1.DAL;
				using FluentValidation.Results;

				using Microsoft.AspNetCore.Hosting;
				using System.IO;
				using System.Net.Http.Headers;
                using Microsoft.AspNetCore.Http;
                using Newtonsoft.Json;
                using System.Threading.Tasks;
				[Route("api/[controller]/[action]")]
				//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:50 AM
				public class InvoiceController : BaseController
				{
				    public InvoiceController(IHttpContextAccessor httpContextAccessor,IOptions<ConnectionSettings> connectionSettings, ILoggerFactory loggerFactory, IConfiguration configuration,IWebHostEnvironment hostingEnvironment)
				    {
					     _configuration = configuration;
					     _logger = loggerFactory.CreateLogger<InvoiceController>();
					     _connectionSettings = connectionSettings;
					     objInvoiceDAL = new InvoiceDAL(_connectionSettings.Value.ConnectionString);
                         obj_External_System_DAL =new External_System_DAL(_connectionSettings.Value.ConnectionString);
                         objExternalSystemUtitlity = new ExternalSystemUtility(_connectionSettings, _configuration);
					     hostingEnv = hostingEnvironment;

                            var authHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                            if (authHeader.StartsWith("Bearer "))
                            {
                                   var token = authHeader.Substring("Bearer ".Length);
                                   string usersid = obj_External_System_DAL.get_users_info_by_token(token);
                                   string tenantid = string.IsNullOrEmpty(httpContextAccessor.HttpContext.Request.Query["tenantid"]) ? Guid.Empty.ToString(): httpContextAccessor.HttpContext.Request.Query["tenantid"].ToString();
                                   pvar_tenantid = usersid + "|" + tenantid;//usersid+tenantid
                                   pvar_usersid=usersid;

                            }
				    }
				private InvoiceDAL objInvoiceDAL;
                private External_System_DAL obj_External_System_DAL;
				private IOptions<ConnectionSettings> _connectionSettings;
				private ILogger _logger;
				private IConfiguration _configuration;
				private IWebHostEnvironment hostingEnv;
                private ExternalSystemUtility objExternalSystemUtitlity;
                private string pvar_tenantid="|";
                private string pvar_usersid="";

			    
            [HttpPost()]
            [ActionName("Add_Invoice")]
            public virtual IActionResult Add_Invoice([FromBody]InvoiceModel model)
            { 
              string message = "";
                
                access_logsdetailsModel obj_access_logsdetailsModel = new access_logsdetailsModel();
                   obj_access_logsdetailsModel.action_method_name="Add_Invoice";
            try{

            if (ModelState.IsValid)
            {

            	InvoiceModelValidator validator = new InvoiceModelValidator();
            	ValidationResult results = validator.Validate(model);
            	if (!results.IsValid)
            	{
            		var errorCollection = string.Join(" | ", results.Errors.Select(e => e.ErrorMessage.Replace("{propertyName}",e.PropertyName)));
             		message = ("Validation Error : " + errorCollection);


            	}else{

                                   var authHeader = HttpContext.Request.Headers["Authorization"][0];
                                if (authHeader.StartsWith("Bearer "))
                                {
                                     
		                      
                                var token = authHeader.Substring("Bearer ".Length);
		                        String[] userdetails=obj_External_System_DAL.get_users_by_token(token);
                                model.createduser=new Guid(userdetails[0].ToString());
                                obj_access_logsdetailsModel.access_logsid=new Guid(userdetails[1].ToString());

		       
                                 

                                
            		                
                                     message = objInvoiceDAL.Add_Invoice(model);
                               }
                                else{
                                  message = "Invalid Token";
                                 }

            	}


            }
            else
            {
            	var errorCollection = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            	message = errorCollection.ToString();

            	_logger.LogError("InvoiceModel - Add_Invoice , Validation Error :" + message);
            	message = ("Validation Error : " + message);
            }






            }catch(Exception ex){
               message=ex.Message;
               _logger.LogError(ex,"An exception occurred in - Add_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
             
            }
            if(obj_access_logsdetailsModel.access_logsid !=null)
            {
                 obj_access_logsdetailsModel.api_response=message.Replace("\"",""); 
                 obj_External_System_DAL.create_access_logs_details(obj_access_logsdetailsModel);
            }

            if(message.Replace("\"","").Contains("201.1"))
                    return Ok(message);
                    else if(message.Replace("\"","")=="401.1")
                    return Unauthorized(message);
                    else
                    return BadRequest(message);


             }
[HttpGet()]
			  [ActionName("getById_Invoice")]
			  public virtual InvoiceModel getById_Invoice(string Invoiceid,string loginUserID="")
			  { 
				    InvoiceModel objInvoice = new InvoiceModel();
					try
					{
						  objInvoice = objInvoiceDAL.getById_Invoice(Invoiceid);
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - getById_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
            
					}
					return objInvoice;

			  }
			  [HttpPost()]
			  [ActionName("Update_Invoice")]
			  public virtual IActionResult Update_Invoice([FromBody]InvoiceModel model)
			  { 
				    string message = "";
                   access_logsdetailsModel obj_access_logsdetailsModel = new access_logsdetailsModel();
                   obj_access_logsdetailsModel.action_method_name="Update_Invoice";

					try{

					if (ModelState.IsValid)
					{

						InvoiceModelValidator validator = new InvoiceModelValidator();
						ValidationResult results = validator.Validate(model);
						if (!results.IsValid)
						{
							var errorCollection = string.Join(" | ", results.Errors.Select(e => e.ErrorMessage.Replace("{propertyName}",e.PropertyName)));
							message = errorCollection.ToString();
							//return BadRequest("Validation Error : " + message);

						}else{
                            var authHeader = HttpContext.Request.Headers["Authorization"][0];
	                        if (authHeader.StartsWith("Bearer "))
	                        {
		                       
                                 
		                      
                                var token = authHeader.Substring("Bearer ".Length);
		                        String[] userdetails=obj_External_System_DAL.get_users_by_token(token);
                                model.modifieduser=new Guid(userdetails[0].ToString());
                                obj_access_logsdetailsModel.access_logsid=new Guid(userdetails[1].ToString());

		       
                                 
		       
                                	
							    message = objInvoiceDAL.Update_Invoice(model);	
                            }
                            else{
                                message = "Invalid Token";
                                 
                            }
							
						}


					}
					else
					{
						var errorCollection = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
						message = errorCollection.ToString();

						_logger.LogError("InvoiceModel - Update_Invoice, Validation Error :" + message);
					
						//return BadRequest("Validation Error : " + message);
					}






					}catch(Exception ex){
                        
						message=ex.Message;
					    _logger.LogError(ex,"An exception occurred in - Update_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
           
					}

                    if(obj_access_logsdetailsModel.access_logsid !=null)
                    {
                            obj_access_logsdetailsModel.api_response=message.Replace("\"",""); 
                            obj_External_System_DAL.create_access_logs_details(obj_access_logsdetailsModel);
                    }

					if(message.Replace("\"","")=="201.1")
					return Ok(message);
					else if(message.Replace("\"","")=="401.1")
					return Unauthorized(message);
					else
					return BadRequest(message);

					


			   }
[HttpGet()]
            public virtual string Remove_Invoice(string Invoiceid,string loginUserID="")
			{
					string message ="";
                    access_logsdetailsModel obj_access_logsdetailsModel = new access_logsdetailsModel();
                   obj_access_logsdetailsModel.action_method_name="Remove_Invoice";

					try{
						
						  var authHeader = HttpContext.Request.Headers["Authorization"][0];
	                        if (authHeader.StartsWith("Bearer "))
	                        {
		                        
		                      
		                      
		                      var token = authHeader.Substring("Bearer ".Length);
		                         
		                        String[] userdetails=obj_External_System_DAL.get_users_by_token(token);
		                        loginUserID=userdetails[0].ToString();
                                obj_access_logsdetailsModel.access_logsid=new Guid(userdetails[1].ToString());
		       
                                 
                        	 message = objInvoiceDAL.Remove_Invoice(Invoiceid,loginUserID);
						    }
	                        else{
		                        message = "Invalid Token";
		                       
	                        }
					 

					}catch(Exception ex){
						message=ex.Message;
                         _logger.LogError(ex,"An exception occurred in - Remove_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                         
					}
                    if(obj_access_logsdetailsModel.access_logsid !=null)
                    {
                         obj_access_logsdetailsModel.api_response=message.Replace("\"",""); 
                         obj_External_System_DAL.create_access_logs_details(obj_access_logsdetailsModel);
                    }
                 
					return message;

			}
[HttpGet()]
			
			[ActionName("Added_Invoice")]
			public virtual System.Data.DataTable Added_Invoice(string verifiedstatus=""
)
			{
					 
				  	DataTable dtInvoice = new DataTable();
					try
					{
                        
						dtInvoice = objInvoiceDAL.Added_Invoice( verifiedstatus
);
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - Added_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                       
					}
					return dtInvoice;

			   }
			   
[HttpGet()]
			
			[ActionName("get_all_Invoice")]
			public virtual System.Data.DataTable get_all_Invoice(string tenantid,string loginUserID="")
			{
					 
				  	DataTable dtInvoice = new DataTable();
					try
					{
                        tenantid=pvar_tenantid;
						dtInvoice = objInvoiceDAL.get_all_Invoice(tenantid);
					}
					catch (Exception ex)
					{
                        _logger.LogError(ex,"An exception occurred in - get_all_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
             
					   
					}
					return dtInvoice;

			   }
[HttpGet()]
			
			[ActionName("count_of_Invoice")]
			public virtual System.Data.DataTable count_of_Invoice()
			{
					 
				  	DataTable dtInvoice = new DataTable();
					try
					{
                        
						dtInvoice = objInvoiceDAL.count_of_Invoice();
					}
					catch (Exception ex)
					{
                          _logger.LogError(ex,"An exception occurred in - count_of_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                         
					}
					return dtInvoice;

			   }
			   
[HttpGet()]
			
			[ActionName("Invoice_for_Review")]
			public virtual System.Data.DataTable Invoice_for_Review(string verifiedstatus=""
)
			{
					 
				  	DataTable dtInvoice = new DataTable();
					try
					{
                        
						dtInvoice = objInvoiceDAL.Invoice_for_Review( verifiedstatus
);
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - Invoice_for_Review, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                       
					}
					return dtInvoice;

			   }
			   
[HttpPost()]
            [ActionName("verify_Invoice")]       
            public virtual string verify_Invoice([FromBody] InvoiceReviewModel model)
			{
					string message ="";

					try{
						  model.verifiedby = pvar_usersid;
						  message = objInvoiceDAL.verify_Invoice(model);
						 
						 

					}catch(Exception ex){
						message=ex.Message;
                         _logger.LogError(ex,"An exception occurred in - verify_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                          
					}
					
					return message;

			}
[HttpGet()]
			
			[ActionName("Invoice_List")]
			public virtual System.Data.DataTable Invoice_List()
			{
					 
				  	DataTable dtInvoice = new DataTable();
					try
					{
                        
						dtInvoice = objInvoiceDAL.Invoice_List();
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - Invoice_List, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                       
					}
					return dtInvoice;

			   }
			   
[HttpGet()]
			  [ActionName("getById_allinfo_Invoice")]
			  public virtual System.Data.DataTable getById_allinfo_Invoice(string Invoiceid)
			  { 
				    DataTable dtInvoice = new DataTable();
					try
					{
						  dtInvoice = objInvoiceDAL.getById_allinfo_Invoice(Invoiceid);
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - getById_allinfo_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
   
					}
					return dtInvoice;

			  }



[HttpGet()]
			
			[ActionName("metrics_of_Invoice")]
			public virtual System.Data.DataTable metrics_of_Invoice()
			{
					 
				  	DataTable dtInvoice = new DataTable();
					try
					{
                        
						dtInvoice = objInvoiceDAL.metrics_of_Invoice();
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - metrics_of_Invoice, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                       
					}
					return dtInvoice;

			   }
			   



				}


			}
