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
				//This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:09 AM
				public class tenantController : BaseController
				{
				    public tenantController(IHttpContextAccessor httpContextAccessor,IOptions<ConnectionSettings> connectionSettings, ILoggerFactory loggerFactory, IConfiguration configuration,IWebHostEnvironment hostingEnvironment)
				    {
					     _configuration = configuration;
					     _logger = loggerFactory.CreateLogger<tenantController>();
					     _connectionSettings = connectionSettings;
					     objtenantDAL = new tenantDAL(_connectionSettings.Value.ConnectionString);
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
				private tenantDAL objtenantDAL;
                private External_System_DAL obj_External_System_DAL;
				private IOptions<ConnectionSettings> _connectionSettings;
				private ILogger _logger;
				private IConfiguration _configuration;
				private IWebHostEnvironment hostingEnv;
                private ExternalSystemUtility objExternalSystemUtitlity;
                private string pvar_tenantid="|";
                private string pvar_usersid="";

			    [AllowAnonymous()]
            [HttpPost()]
            [ActionName("Create_tenant")]
            public virtual IActionResult Create_tenant([FromBody]tenantModel model)
            { 
              string message = "";
              
            try{

            if (ModelState.IsValid)
            {

            	tenantModelValidator validator = new tenantModelValidator();
            	ValidationResult results = validator.Validate(model);
            	if (!results.IsValid)
            	{
            		var errorCollection = string.Join(" | ", results.Errors.Select(e => e.ErrorMessage.Replace("{propertyName}",e.PropertyName)));
             		message = ("Validation Error : " + errorCollection);


            	}else{
                    
                         message = objtenantDAL.Create_tenant(model);
                        
            	}


            }
            else
            {
            	var errorCollection = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            	message = errorCollection.ToString();

            	_logger.LogError("tenantModel - Create_tenant , Validation Error :" + message);
            	message = ("Validation Error : " + message);
            }






            }catch(Exception ex){
            	message=ex.Message; 
                _logger.LogError(ex,"An exception occurred in - Create_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
             
            	 
            }

                

           if(message.Replace("\"","").Contains("201.1"))
                    return Ok(message);
                    else if(message.Replace("\"","")=="401.1")
                    return Unauthorized(message);
                    else
                    return BadRequest(message);


             }
[HttpGet()]
			  [ActionName("getById_tenant")]
			  public virtual tenantModel getById_tenant(string tenantid,string loginUserID="")
			  { 
				    tenantModel objtenant = new tenantModel();
					try
					{
						  objtenant = objtenantDAL.getById_tenant(tenantid);
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - getById_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
            
					}
					return objtenant;

			  }
			  [HttpPost()]
			  [ActionName("Update_tenant")]
			  public virtual IActionResult Update_tenant([FromBody]tenantModel model)
			  { 
				    string message = "";
                   access_logsdetailsModel obj_access_logsdetailsModel = new access_logsdetailsModel();
                   obj_access_logsdetailsModel.action_method_name="Update_tenant";

					try{

					if (ModelState.IsValid)
					{

						tenantModelValidator validator = new tenantModelValidator();
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

		       
                                 
		       
                                	
							    message = objtenantDAL.Update_tenant(model);	
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

						_logger.LogError("tenantModel - Update_tenant, Validation Error :" + message);
					
						//return BadRequest("Validation Error : " + message);
					}






					}catch(Exception ex){
                        
						message=ex.Message;
					    _logger.LogError(ex,"An exception occurred in - Update_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
           
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
            public virtual string Remove_tenant(string tenantid,string loginUserID="")
			{
					string message ="";
                    access_logsdetailsModel obj_access_logsdetailsModel = new access_logsdetailsModel();
                   obj_access_logsdetailsModel.action_method_name="Remove_tenant";

					try{
						
						  var authHeader = HttpContext.Request.Headers["Authorization"][0];
	                        if (authHeader.StartsWith("Bearer "))
	                        {
		                        
		                      
		                      
		                      var token = authHeader.Substring("Bearer ".Length);
		                         
		                        String[] userdetails=obj_External_System_DAL.get_users_by_token(token);
		                        loginUserID=userdetails[0].ToString();
                                obj_access_logsdetailsModel.access_logsid=new Guid(userdetails[1].ToString());
		       
                                 
                        	 message = objtenantDAL.Remove_tenant(tenantid,loginUserID);
						    }
	                        else{
		                        message = "Invalid Token";
		                       
	                        }
					 

					}catch(Exception ex){
						message=ex.Message;
                         _logger.LogError(ex,"An exception occurred in - Remove_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                         
					}
                    if(obj_access_logsdetailsModel.access_logsid !=null)
                    {
                         obj_access_logsdetailsModel.api_response=message.Replace("\"",""); 
                         obj_External_System_DAL.create_access_logs_details(obj_access_logsdetailsModel);
                    }
                 
					return message;

			}
[HttpGet()]
			[AllowAnonymous()]
			[ActionName("ViewList_tenant")]
			public virtual System.Data.DataTable ViewList_tenant()
			{
					 
				  	DataTable dttenant = new DataTable();
					try
					{
                        
						dttenant = objtenantDAL.ViewList_tenant();
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - ViewList_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                       
					}
					return dttenant;

			   }
			   
[HttpGet()]
			[AllowAnonymous()]
			[ActionName("get_all_tenant")]
			public virtual System.Data.DataTable get_all_tenant(string tenantid,string loginUserID="")
			{
					 
				  	DataTable dttenant = new DataTable();
					try
					{
                        tenantid=pvar_tenantid;
						dttenant = objtenantDAL.get_all_tenant(tenantid);
					}
					catch (Exception ex)
					{
                        _logger.LogError(ex,"An exception occurred in - get_all_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
             
					   
					}
					return dttenant;

			   }
[HttpGet()]
			[ActionName("get_project_tenant")]
			public virtual System.Data.DataTable get_project_tenant(string tenantid)
			{
					tenantid= pvar_tenantid;
				  	DataTable dttenant = new DataTable();
					try
					{
						dttenant = objtenantDAL.get_project_tenant(tenantid);
					}
					catch (Exception ex)
					{
                           _logger.LogError(ex,"An exception occurred in - get_project_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
                     
					}
					return dttenant;

			  }
[HttpGet()]
			  [ActionName("getById_allinfo_tenant")]
			  public virtual System.Data.DataTable getById_allinfo_tenant(string tenantid)
			  { 
				    DataTable dttenant = new DataTable();
					try
					{
						  dttenant = objtenantDAL.getById_allinfo_tenant(tenantid);
					}
					catch (Exception ex)
					{
                         _logger.LogError(ex,"An exception occurred in - getById_allinfo_tenant, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
   
					}
					return dttenant;

			  }

                [AllowAnonymous()]
                [HttpGet()]
                [ActionName("get_parentid")]
                public virtual System.Data.DataTable get_parentid(string businesstype="")
                {
		                DataTable dttenant = new DataTable();
		                try
		                {   
			
			                dttenant = objtenantDAL.lookup_tenant_parentid(businesstype);
		                }
		                catch (Exception ex)
		                {
                             _logger.LogError(ex,"An exception occurred in - get_parentid, Error Message : " + (ex.StackTrace != null ? $", Stack Trace: {ex.StackTrace.ToString()}" :ex.Message));
              
		                }
		                return dttenant;
                }






				}


			}
