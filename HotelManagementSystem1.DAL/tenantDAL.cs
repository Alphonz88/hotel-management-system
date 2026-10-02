namespace HotelManagementSystem1.DAL{
			    using System;
			    using System.Text;
			    using System.Data;
			    using System.Data.Common;
			    using HotelManagementSystem1.Models;
			    using EncrypDecrypt;
			    using Newtonsoft.Json;
				using Newtonsoft.Json.Linq;
                using Npgsql;
				using NpgsqlTypes;
				using System.Text.RegularExpressions;

			    //This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:09 AM
			    public class tenantDAL
			    {
					public virtual string db_connectionstring{get;set;}
					
			 	    public tenantDAL(string connectionString)
				    {
						
					    db_connectionstring=connectionString;
				    }
				  
			        
              public virtual string Create_tenant(tenantModel model)
			  { 
				  String ResponseMessage="";
					 
					try{
							 
                            using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
					        {
						        npsql.Open();
						        using (var dbCommand = new NpgsqlCommand("\"Create_tenant\"", npsql))
						        {
                                        dbCommand.CommandType = CommandType.StoredProcedure;
						            	RandomStringGenerator objRandom =new RandomStringGenerator();
				string user_passwordkey = objRandom.CreateRandomPassword(8);
				string user_password = PwdEncrypDecrypt.Encrypt(user_passwordkey,model.password);
				model.password=user_password;
				model.createduser = model.tenantid;
								        					dbCommand.Parameters.AddWithValue("pvar_tenantid",NpgsqlDbType.Uuid,(object)model.tenantid??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businessname",NpgsqlDbType.Varchar,(object)model.businessname??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_natureofbusiness",NpgsqlDbType.Varchar,(object)model.natureofbusiness??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businessemail",NpgsqlDbType.Varchar,(object)model.businessemail??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businessphone",NpgsqlDbType.Varchar,(object)model.businessphone??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businesswebsite",NpgsqlDbType.Varchar,(object)model.businesswebsite??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_organizationlogo",NpgsqlDbType.Varchar,(object)model.organizationlogo??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_numberofemployees",NpgsqlDbType.Integer,(object)model.numberofemployees??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_addressline1",NpgsqlDbType.Varchar,(object)model.addressline1??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_addressline2",NpgsqlDbType.Varchar,(object)model.addressline2??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_city",NpgsqlDbType.Varchar,(object)model.city??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_statename",NpgsqlDbType.Varchar,(object)model.statename??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_zip",NpgsqlDbType.Varchar,(object)model.zip??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_country",NpgsqlDbType.Varchar,(object)model.country??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_parentid",NpgsqlDbType.Uuid,(object)model.parentid??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_username",NpgsqlDbType.Varchar,(object)model.username??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_password",NpgsqlDbType.Varchar,(object)model.password??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_userrole",NpgsqlDbType.Varchar,(object)model.userrole??DBNull.Value);
dbCommand.Parameters.AddWithValue("pvar_createduser",NpgsqlDbType.Uuid,(object)model.createduser??DBNull.Value);	
					
dbCommand.Parameters.AddWithValue("pvar_passwordkey", NpgsqlDbType.Varchar, (object)user_passwordkey ?? DBNull.Value);
                                        NpgsqlParameter outParm = new NpgsqlParameter("pvar_returnMessage", NpgsqlDbType.Varchar)
                                        {
                                             Direction = ParameterDirection.Output
                                        };
                                        dbCommand.Parameters.Add(outParm);

                                        dbCommand.ExecuteNonQuery();
								        ResponseMessage = outParm.Value.ToString();
								        if (dbCommand.Connection.State != ConnectionState.Closed)
                    			        {
										         dbCommand.Connection.Dispose();
								        }

						        }
						        npsql.Close();
					        }
 

					}catch(Exception ex){
						ResponseMessage=ex.Message;
						Console.WriteLine(ex);
					} 
					
					return ResponseMessage;

			   }
public virtual tenantModel getById_tenant(string tenantid)
									 {
										DataTable dataTable = new DataTable();
										DataSet dataSet = new DataSet();
										try{
												 
												using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
												{
													npsql.Open();
													using (var dbCommand = new NpgsqlCommand("\"getById_sp_tenant\"", npsql))
													{
														dbCommand.CommandType = CommandType.StoredProcedure;
														dbCommand.Parameters.AddWithValue("pvar_tenantid",(object)tenantid??DBNull.Value);
														using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(dbCommand))
														{
															dataSet.Reset();
															dataAdapter.Fill(dataSet);
															dataTable = dataSet.Tables[0];
															if (dbCommand.Connection.State != ConnectionState.Closed)
															{
																dbCommand.Connection.Dispose();
															}
														}
													}
													npsql.Close();
												}
					 
										}catch{
												throw;
										}
										if (dataTable.Rows.Count > 0)
										{
											DataRow row = dataTable.Rows[0];
											return ModelConverter.ConvertDataRowToModel<tenantModel>(row);
										}
										else
										{
											return null;
										}
									 }
			 public virtual string  Update_tenant(tenantModel model)
			 { 
				 String ResponseMessage="";
					try{
						 	 
							using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
							{
								npsql.Open();
								using (var dbCommand = new NpgsqlCommand("\"Update_tenant\"", npsql))
								{
										dbCommand.CommandType = CommandType.StoredProcedure;
															dbCommand.Parameters.AddWithValue("pvar_tenantid",NpgsqlDbType.Uuid,(object)model.tenantid??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businessname",NpgsqlDbType.Varchar,(object)model.businessname??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_natureofbusiness",NpgsqlDbType.Varchar,(object)model.natureofbusiness??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businessemail",NpgsqlDbType.Varchar,(object)model.businessemail??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businessphone",NpgsqlDbType.Varchar,(object)model.businessphone??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_businesswebsite",NpgsqlDbType.Varchar,(object)model.businesswebsite??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_organizationlogo",NpgsqlDbType.Varchar,(object)model.organizationlogo??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_numberofemployees",NpgsqlDbType.Integer,(object)model.numberofemployees??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_addressline1",NpgsqlDbType.Varchar,(object)model.addressline1??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_addressline2",NpgsqlDbType.Varchar,(object)model.addressline2??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_city",NpgsqlDbType.Varchar,(object)model.city??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_statename",NpgsqlDbType.Varchar,(object)model.statename??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_zip",NpgsqlDbType.Varchar,(object)model.zip??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_country",NpgsqlDbType.Varchar,(object)model.country??DBNull.Value);

dbCommand.Parameters.AddWithValue("pvar_parentid",NpgsqlDbType.Uuid,(object)model.parentid??DBNull.Value);
dbCommand.Parameters.AddWithValue("pvar_modifieduser",NpgsqlDbType.Uuid,model.modifieduser);	
															
										NpgsqlParameter outParm = new NpgsqlParameter("@returnMessage", NpgsqlDbType.Varchar)
										{
											 Direction = ParameterDirection.Output
										};
										dbCommand.Parameters.Add(outParm);

										dbCommand.ExecuteNonQuery();
										ResponseMessage = outParm.Value.ToString();
										if (dbCommand.Connection.State != ConnectionState.Closed)
										{
												 dbCommand.Connection.Dispose();
										}

								}
								npsql.Close();
							}		 

					}catch(Exception ex){
						ResponseMessage=ex.Message;
					}
					
					return ResponseMessage;

			   }
public virtual string  Remove_tenant(string id,string loginUserID)
			  { 
				  String ResponseMessage="";
					try{ 
							using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
							{
								npsql.Open();
								using (var dbCommand = new NpgsqlCommand("\"Remove_tenant\"", npsql))
								{
										dbCommand.CommandType = CommandType.StoredProcedure;
										dbCommand.Parameters.AddWithValue("pvar_tenantid",(object)id??DBNull.Value);
										dbCommand.Parameters.AddWithValue("pvar_modifieduser",(object)loginUserID??DBNull.Value);
										NpgsqlParameter outParm = new NpgsqlParameter("@returnMessage", NpgsqlDbType.Varchar)
										{
											 Direction = ParameterDirection.Output
										};
										dbCommand.Parameters.Add(outParm);

										dbCommand.ExecuteNonQuery();
										ResponseMessage = outParm.Value.ToString();
										if (dbCommand.Connection.State != ConnectionState.Closed)
										{
												 dbCommand.Connection.Dispose();
										}

								}
								npsql.Close();
							}	 

					}catch(Exception ex){
						ResponseMessage=ex.Message;
					}
					
					return ResponseMessage;

			   }
public virtual System.Data.DataTable ViewList_tenant()
			  { 
					DataTable dataTable = new DataTable();
                DataSet dataSet = new DataSet(); 

					try{
 
							using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
							{
								npsql.Open();
								using (var dbCommand = new NpgsqlCommand("\"ViewList_tenant\"", npsql))
								{
									dbCommand.CommandType = CommandType.StoredProcedure;
									
									using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(dbCommand))
									{
										dataSet.Reset();
										dataAdapter.Fill(dataSet);
										dataTable = dataSet.Tables[0];
										if (dbCommand.Connection.State != ConnectionState.Closed)
										{
											dbCommand.Connection.Dispose();
										}
									}
								}
								npsql.Close();
							}

						 

					}catch{
						throw;
					}


					return dataTable;	


					 

			   }
			   
			 
public virtual System.Data.DataTable get_all_tenant(string tenantid)
			  { 

				    DataTable dataTable = new DataTable();
					DataSet dataSet = new DataSet();

					try{
 
							using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
							{
								npsql.Open();
								using (var dbCommand = new NpgsqlCommand("\"get_all_tenant\"", npsql))
								{
									dbCommand.CommandType = CommandType.StoredProcedure;
									dbCommand.Parameters.AddWithValue("pvar_tenantid",(object)tenantid??DBNull.Value);
									
									using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(dbCommand))
									{
										dataSet.Reset();
										dataAdapter.Fill(dataSet);
										dataTable = dataSet.Tables[0];
										if (dbCommand.Connection.State != ConnectionState.Closed)
										{
											dbCommand.Connection.Dispose();
										}
									}
								}
								npsql.Close();
							}
						

					}catch{
						throw;
					}
					return dataTable;	


					 

			   }
public virtual System.Data.DataTable get_project_tenant(string tenantid)
			  { 
					DataTable dataTable = new DataTable();
                DataSet dataSet = new DataSet(); 

					try{
 
							using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
							{
								npsql.Open();
								using (var dbCommand = new NpgsqlCommand("\"get_project_tenant\"", npsql))
								{
									dbCommand.CommandType = CommandType.StoredProcedure;
									dbCommand.Parameters.AddWithValue("pvar_tenantid",(object)tenantid??DBNull.Value);
									using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(dbCommand))
									{
										dataSet.Reset();
										dataAdapter.Fill(dataSet);
										dataTable = dataSet.Tables[0];
										if (dbCommand.Connection.State != ConnectionState.Closed)
										{
											dbCommand.Connection.Dispose();
										}
									}
								}
								npsql.Close();
							}

						 

					}catch{
						throw;
					}


					return dataTable;	


					 

			   }
			   
			 
public virtual System.Data.DataTable getById_allinfo_tenant(string tenantid)
			 {
				DataSet dataSet=new DataSet();
				DataTable dataTable = new DataTable();
				try{
					     
						using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
						{
							npsql.Open();
							using (var dbCommand = new NpgsqlCommand("\"getById_sp_all_tenant\"", npsql))
							{
								dbCommand.CommandType = CommandType.StoredProcedure;
								dbCommand.Parameters.AddWithValue("pvar_tenantid",(object)tenantid??DBNull.Value);
								using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(dbCommand))
								{
									dataSet.Reset();
									dataAdapter.Fill(dataSet);
									dataTable = dataSet.Tables[0];
									if (dbCommand.Connection.State != ConnectionState.Closed)
									{
										dbCommand.Connection.Dispose();
									}
								}
							}
							npsql.Close();
						}
					 
				}catch{
						throw;
				}
				return dataTable;
			 }
			  
public virtual System.Data.DataTable lookup_tenant_parentid(string businesstype="")
							        {
                                            DataSet dataSet = new DataSet();
									        DataTable dataTable=new DataTable();
									        try{

                                        		        using (NpgsqlConnection npsql = new NpgsqlConnection(db_connectionstring))
					                                    {
						                                    npsql.Open();
						                                    using (var dbCommand = new NpgsqlCommand("\"lookup_tenant_parentid\"", npsql))
						                                    {
                                            
                                                                dbCommand.Parameters.AddWithValue("pvar_businesstype",(object)businesstype??DBNull.Value);  
																
																 
                                                                dbCommand.CommandType = CommandType.StoredProcedure;
							                                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(dbCommand))
                                                                {
                                                                    dataSet.Reset();
                                                                    dataAdapter.Fill(dataSet);
                                                                    dataTable = dataSet.Tables[0];
                                                                    if (dbCommand.Connection.State != ConnectionState.Closed)
												                    {
													                    dbCommand.Connection.Dispose();
												                    }
                                                                }
						                                    }
						                                    npsql.Close();
					                                    }
			 

										 
									        }catch{
										        throw;
									        }
									        return dataTable;
							        }







			    }


			    }
