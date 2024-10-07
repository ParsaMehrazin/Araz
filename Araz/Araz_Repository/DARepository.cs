using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ViewModel.ViewModels;


namespace Repository
{

    class DAL
    {
        #region DataAccess

        public static DataTable SQLDataTable(string querry, string ConnectionString)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
       //         ConnectionString = ConnectionString.Replace("\\", @"\");
                conn.Open();
                SqlCommand sc = new SqlCommand(querry, conn);
                sc.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(sc);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return dt = new DataTable();
            }
            finally
            {
                if (conn != null)
                    ((IDisposable)conn).Dispose();
            }
        }
        public static SqlDataReader SQC(string querry, string ConnectionString)
        {
            SqlDataReader dr;
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(querry, conn);
                SqlDataAdapter da = new SqlDataAdapter(sc);
                dr = sc.ExecuteReader();
                return dr;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conn != null)
                    ((IDisposable)conn).Dispose();
            }

        }
        public static Boolean ExecNQ(string querry, string ConnectionString)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(querry, conn);
                sc.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (conn != null)
                    ((IDisposable)conn).Dispose();
            }
        }
        public static string SQLScaler(string querry, string ConnectionString)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                string scaler;
                conn.Open();
                SqlCommand sc = new SqlCommand(querry, conn);
                scaler = sc.ExecuteScalar().ToString();
                return scaler;
            }
            catch
            {
                return "-1";
            }
            finally
            {
                if (conn != null)
                    ((IDisposable)conn).Dispose();
            }
        }

        #endregion
    }

    public static class DARepository
    {
        public static string CNN { get; set; }
        public static string DidarToken { get; set; }
        public static string CNNDidarApi { get; set; }
        public static string CNNSendSMSApi { get; set; }

        //public static bool ShowMessage(string msg, int showMessageTime = 2)
        //{

        //    //Always =2;
        //    //False=0;
        //    // True=1;
        //    if (string.IsNullOrEmpty(msg))
        //    {
        //        MessageBox.Show("پیغامی یافت نشد", "خطا", MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
        //            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        //        return false;
        //    }
        //    var split = msg.Split('@');
        //    if (split[0] == "0" || split[0] == "-1")
        //    {
        //        if (showMessageTime == 0 || showMessageTime == 2)
        //            MessageBox.Show(split[1], "خطا", MessageBoxButtons.OK,
        //                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
        //                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        //        return false;
        //    }
        //    else
        //        if (showMessageTime == 1 || showMessageTime == 2)
        //        MessageBox.Show(split[1]);

        //    return true;
        //}

        public static bool GetDataTypeIsString(string input)
        {
            if (input == "System.String")
                return true;
            else
                return false;

        }

        public static SqlDbType GetSqlDbType(string input)
        {
            switch (input)
            {

                case "varbinary(max)":
                    return SqlDbType.VarBinary;
                    break;
                case "System.Int32":
                    return SqlDbType.Int;
                    break;
                case "System.Int64":
                    return SqlDbType.BigInt;
                    break;
                case "System.Int16":
                    return SqlDbType.TinyInt;
                    break;
                case "System.String":
                    return SqlDbType.NVarChar;
                    break;
                default:
                    return SqlDbType.NVarChar;
                    break;
            }
        }

        public static string ExcuteOperationalSP(string schema, string procidureName, params ServiceOperatorParameter[] parameters)
        {
            try
            {
                string Result = "";
                using (SqlConnection conn = new SqlConnection(CNN))
                using (SqlCommand cmd = new SqlCommand(schema + "." + "sp_" + procidureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        string type = string.Empty;
                        if (parameters[i].Name == "FileContent")
                            type = "varbinary(max)";
                        else
                            type = parameters[i].Value.GetType().ToString();

                        cmd.Parameters.Add("@" + parameters[i].Name, GetSqlDbType(type));

                        if (parameters[i].Name == "FileContent" && (parameters[i].Value == null || parameters[i].Value == ""))
                            cmd.Parameters["@" + parameters[i].Name].Value = new byte[0];
                        else
                            cmd.Parameters["@" + parameters[i].Name].Value = parameters[i].Value;
                    }

                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@msg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int res = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                    string mes = cmd.Parameters["@msg"].Value.ToString();
                    conn.Close();

                    Result = res + "@" + mes;
                }
                return Result;
            }
            catch (Exception ex)
            {
                return "0@" + ex.Message;
            }
            finally
            {
            }
        }
        public static BaseRepositoryResponseViewModel ExcuteOperationalSP_New(string schema, string procidureName, params ServiceOperatorParameter[] parameters)
        {
            try
            {
                string Result = "";
                using (SqlConnection conn = new SqlConnection(CNN))
                using (SqlCommand cmd = new SqlCommand(schema + "." + "sp_" + procidureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        string type = string.Empty;
                        if (parameters[i].Name == "FileContent")
                        {
                            type = "varbinary(max)";
                            cmd.Parameters.Add("@" + parameters[i].Name, GetSqlDbType(type));
                            if (parameters[i].Value == null || parameters[i].Value == "")
                                cmd.Parameters["@" + parameters[i].Name].Value = new byte[0];
                        }
                        else if (parameters[i].DataTable != null)
                        {
                            type = "Structured";

                            SqlParameter tvp = new SqlParameter(parameters[i].Name, parameters[i].DataTable);
                            tvp.SqlDbType = SqlDbType.Structured;
                            tvp.TypeName = parameters[i].Value.ToString();
                            cmd.Parameters.Add(tvp);
                        }
                        else
                        {
                            type = parameters[i].Value.GetType().ToString();
                            cmd.Parameters.Add("@" + parameters[i].Name, GetSqlDbType(type));
                            cmd.Parameters["@" + parameters[i].Name].Value = parameters[i].Value;
                        }
                    }
                    cmd.Parameters.Add("@msg", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int res = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                    string mes = cmd.Parameters["@msg"].Value.ToString();
                    conn.Close();
                    return new BaseRepositoryResponseViewModel() { Result = res, msg = mes, ResponseMessages = null, Success = res > 0 };
                }
            }
            catch (Exception ex)
            {
                return new BaseRepositoryResponseViewModel() { Result = 0, msg = ex.Message, ResponseMessages = null, Success = false };
            }
            finally
            {
            }
        }

        public static int GetSingleValueFromFunction(string name, params ServiceOperatorParameter[] parameters)
        {
            string paras = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                string type = parameters[i].Value.GetType().ToString();
                if (GetDataTypeIsString(type))
                    paras += "'" + parameters[i].Value + "',";
                else
                    paras += parameters[i].Value + ",";
            }

            if (paras.Length > 0)
                paras = paras.Remove(paras.Length - 1, 1);

            string query = string.Empty;

            query = "SELECT [dbo].[fn" + name + "](" + paras + ")";
            var res = DAL.SQLScaler(query, CNN);
            if (res != null && !string.IsNullOrEmpty(res))
                return int.Parse(res);
            else
                return -100;


        }

        public static string GetSingleValueFromFunctionString(string name, params ServiceOperatorParameter[] parameters)
        {
            string paras = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                string type = parameters[i].Value.GetType().ToString();
                if (GetDataTypeIsString(type))
                    paras += "'" + parameters[i].Value + "',";
                else
                    paras += parameters[i].Value + ",";
            }

            if (paras.Length > 0)
                paras = paras.Remove(paras.Length - 1, 1);

            string query = string.Empty;

            query = "SELECT [dbo].[fn" + name + "](" + paras + ")";
            var res = DAL.SQLScaler(query, CNN);
            if (res != null && !string.IsNullOrEmpty(res))
                return res;
            else
                return "";


        }

        public static DateTime GetSingleValueFromFunctionDateTime(string name, params ServiceOperatorParameter[] parameters)
        {
            string paras = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                string type = parameters[i].Value.GetType().ToString();
                if (GetDataTypeIsString(type))
                    paras += "'" + parameters[i].Value + "',";
                else
                    paras += parameters[i].Value + ",";
            }

            if (paras.Length > 0)
                paras = paras.Remove(paras.Length - 1, 1);

            string query = string.Empty;

            query = "SELECT [dbo].[fn" + name + "](" + paras + ")";
            var res = DAL.SQLScaler(query, CNN);
            if (res != null && !string.IsNullOrEmpty(res))
                return Convert.ToDateTime(res);
            else
                return new DateTime();


        }

        public static IQueryable<T> GetAllFromTableValueFunction<T>(string name, params ServiceOperatorParameter[] parameters) where T : new()
        {

            string paras = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                string type = parameters[i].Value.GetType().ToString();
                if (GetDataTypeIsString(type))
                    paras += "'" + parameters[i].Value + "',";
                else
                    paras += parameters[i].Value + ",";
            }

            if (paras.Length > 0)
                paras = paras.Remove(paras.Length - 1, 1);


            string query = "SELECT * FROM dbo.fn" + name + "(" + paras + ")";

            DAL.SQLDataTable(query, CNN);

            DataTable ds = DAL.SQLDataTable(query, CNN);
            return ds.DataTableToList<T>().AsQueryable();
        }

        public static IQueryable<T> GetAllFromSP<T>(string name, params ServiceOperatorParameter[] parameters) where T : new()
        {
            string paras = string.Empty;
            string type = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].Value.ToString().Contains("dbo.tvp"))
                {
                    paras += parameters[i] = null;
                }
                else
                {
                    type = parameters[i].Value.GetType().ToString();
                    if (GetDataTypeIsString(type))
                        paras += "'" + parameters[i].Value + "',";
                    else
                        paras += parameters[i].Value + ",";
                }
            }

            if (paras.Length > 0)
                paras = paras.Remove(paras.Length - 1, 1);

            string query = "EXECUTE dbo.Sp" + name + "  \n " + paras;

            DataTable ds = DAL.SQLDataTable(query, CNN);
            return ds.DataTableToList<T>().AsQueryable();
        }

        public static string ExcuteOperationalSPBatch(string schema, string procidureName, params ServiceOperatorParameter[] parameters)
        {
            try
            {
                string Result = "";
                using (SqlConnection conn = new SqlConnection(CNN))
                using (SqlCommand cmd = new SqlCommand(schema + ".sp_" + procidureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].DataTable == null)
                        {
                            string type = parameters[i].Value.GetType().ToString();
                            cmd.Parameters.Add("@" + parameters[i].Name, GetSqlDbType(type));
                            cmd.Parameters["@" + parameters[i].Name].Value = parameters[i].Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + parameters[i].Name, SqlDbType.Structured);
                            cmd.Parameters["@" + parameters[i].Name].TypeName = parameters[i].Value.ToString();
                            cmd.Parameters["@" + parameters[i].Name].Value = parameters[i].DataTable;



                        }
                    }

                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@msg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int res = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                    string mes = cmd.Parameters["@msg"].Value.ToString();
                    conn.Close();
                    Result = res + "@" + mes;
                }
                return Result;
            }
            catch (Exception ex)
            {
                return "0@" + ex.Message;
            }
            finally
            {
                //if (NeedsToRubAnalaysJob(ProcidureName))
                //{
                //    string query = @"USE msdb; IF not EXISTS(SELECT 1 
                //       FROM msdb.dbo.sysjobs J
                //       JOIN msdb.dbo.sysjobactivity A
                //           ON A.job_id = J.job_id
                //       WHERE J.name = N'Analisysflights'
                //       AND A.run_requested_date IS NOT NULL
                //       AND A.stop_execution_date IS NULL
                //      )   EXEC dbo.sp_start_job N'Analisysflights' ;";
                //    DAL.ExecNQ(query, CNN);
                //}
            }
        }

        public static List<T> DataTableToList<T>(this DataTable table) where T : new()
        {

            List<T> list = new List<T>();
            try
            {
                var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
                {
                    PropertyInfo = propertyInfo,
                    Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
                }).ToList();

                foreach (var row in table.Rows.Cast<DataRow>())
                {
                    T obj = new T();
                    foreach (var typeProperty in typeProperties)
                    {
                        object value = null;
                        object safeValue = null;
                        var myColumn = row.Table.Columns.Cast<DataColumn>().SingleOrDefault(col => col.ColumnName.ToLower() == typeProperty.PropertyInfo.Name.ToLower());
                        if (myColumn != null)
                            value = row[typeProperty.PropertyInfo.Name];


                        safeValue = value == null || DBNull.Value.Equals(value)
                           ? null
                           : Convert.ChangeType(value, typeProperty.Type);

                        typeProperty.PropertyInfo.SetValue(obj, safeValue, null);


                    }
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public static DataTable ListToDataTable<T>(this IList<T> data)
        {

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(nameof(T)); // "tvpOpOneDetail");
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        #region ■ Get All/Single From View_...

        #region ■ Get All/Single From View_...

        public static IQueryable<T> GetAllFromView<T>(string select, string where) where T : new()
        {
            string _query = select + " " + where;
            DataTable ds = DAL.SQLDataTable(_query, CNN);
            return ds.DataTableToList<T>().AsQueryable();
        }


        public static List<View_SysSetting> GetAllSetting(string Where)
        {
            string query = "SELECT * FROM View_SysSetting " + Where;
            DataTable ds = DAL.SQLDataTable(query, CNN);
            return ds.DataTableToList<View_SysSetting>();
        }

        public static List<View_SysOption> GetAllOption(string Where)
        {
            string query = "SELECT * FROM View_Option " + Where;
            DataTable ds = DAL.SQLDataTable(query, CNN);
            return ds.DataTableToList<View_SysOption>();
        }

        //public static List<View_ProductionProcessMain> GetProductionProcessMain(string Where)
        //{
        //    string query = "SELECT * FROM View_ProductionProcessMain " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_ProductionProcessMain>();
        //}

        //public static List<View_Tax> GetAllTax(string Where)
        //{
        //    string query = "SELECT * FROM View_Tax " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Tax>();
        //}

        //public static List<View_City> GetAllCity(string Where)
        //{
        //    string query = "SELECT * FROM View_City " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_City>();
        //}

        //public static List<View_Country> GetAllCountry(string Where)
        //{
        //    string query = "SELECT * FROM View_Country " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Country>();
        //}

        //public static List<View_Employee> GetAllEmployee(string Where)
        //{
        //    string query = "SELECT * FROM View_Employee " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Employee>();
        //}

        //public static List<View_Customer> GetAllCustomer(string Where)
        //{
        //    // To Do Edit Table Ashkhas and Create View_Customer on db
        //    string query = "SELECT * FROM View_Customer " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Customer>();
        //}

        //public static List<View_CustomerCompany> GetAllCustomerCompany(string Where)
        //{
        //    string query = "SELECT * FROM View_CustomerCompany " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_CustomerCompany>();
        //}

        //public static List<View_CustomerCompany> GetAllCustomerCompany(int fkCustomer)
        //{
        //    string query = "SELECT * FROM View_CustomerCompany WHERE fkCustomer = " + fkCustomer.ToString();
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_CustomerCompany>();
        //}

        //public static List<View_ObjectAccess> GetAllObjectAccess(string Where)
        //{
        //    string query = "SELECT * FROM View_ObjectAccess " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_ObjectAccess>();
        //}

        //public static List<View_EmployeeAddress> GetAllEmployeeAddress(int fkEmployee)
        //{
        //    string query = "SELECT * FROM View_EmployeeAddress Where fkEmployee = " + fkEmployee;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_EmployeeAddress>();
        //}

        //public static List<View_EmployeeAddress> GetAllEmployeeAddress(string Where)
        //{
        //    string query = "SELECT * FROM View_EmployeeAddress " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_EmployeeAddress>();
        //}

        //public static List<View_CustomerAddress> GetAllCustomerAddress(int fkCustomer)
        //{
        //    string query = "SELECT * FROM View_CustomerAddress Where fkCustomer = " + fkCustomer;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_CustomerAddress>();
        //}

        //public static List<View_CustomerAddress> GetAllCustomerAddress(string Where)
        //{
        //    string query = "SELECT * FROM View_CustomerAddress " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_CustomerAddress>();
        //}

        //public static List<View_EmployeeObjectAccess> GetAllEmployeeObjectAccess(long fkEmployee)
        //{
        //    string query = "SELECT * FROM View_EmployeeObjectAccess Where fkEmployee = " + fkEmployee;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_EmployeeObjectAccess>();
        //}

        //public static List<View_EmployeeObjectAccess> GetAllEmployeeObjectAccess(string Where)
        //{
        //    string query = "SELECT * FROM View_EmployeeObjectAccess " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_EmployeeObjectAccess>();
        //}

        //public static List<View_Role> GetAllRole(string Where)
        //{
        //    string query = "SELECT * FROM View_Role " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Role>();
        //}

        //public static List<View_EmployeeRole> GetAllEmployeeRole(long fkEmployee)
        //{
        //    string query = "SELECT * FROM View_EmployeeRole Where fkEmployee = " + fkEmployee;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_EmployeeRole>();
        //}

        //public static List<View_EmployeeRole> GetAllEmployeeRole(string where)
        //{
        //    string query = "SELECT * FROM View_EmployeeRole " + where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_EmployeeRole>();
        //}

        //public static List<View_ProductGroup> GetAllProductGroup(string Where)
        //{
        //    string query = "SELECT * FROM View_ProductGroup " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_ProductGroup>();
        //}

        //public static List<View_GroupProperty> GetAllGroupProperty(string Where)
        //{
        //    string query = "SELECT * FROM View_GroupProperty " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_GroupProperty>();
        //}

        //public static List<View_GroupPropertyValue> GetAllGroupPropertyValue(string Where)
        //{
        //    string query = "SELECT * FROM View_GroupPropertyValue " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_GroupPropertyValue>();
        //}

        //public static List<View_Role_Old> GetAllRole_Old(string Where)
        //{
        //    string query = "SELECT * FROM View_Role_Old " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Role_Old>();
        //}

        //public static List<View_User> GetAllUser(string Where)
        //{
        //    string query = "SELECT * FROM View_User " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_User>();
        //}

        //public static List<View_Ashkhas> GetAllAshkhas(string Where)
        //{
        //    string query = "SELECT * FROM View_Ashkhas " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Ashkhas>();
        //}

        //public static List<View_AshkhasTozihat> GetAllAshkhasTozihat(string Where)
        //{
        //    string query = "SELECT * FROM View_AshkhasTozihat " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_AshkhasTozihat>();
        //}

        //public static List<View_Car> GetAllCar(string Where)
        //{
        //    string query = "SELECT * FROM View_Car " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Car>();
        //}
        //public static List<View_FinancialYear> GetAllFinancialYear(string Where)
        //{
        //    string query = "SELECT * FROM View_FinancialYear " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_FinancialYear>();
        //}

        //public static List<View_AshkhasToMoein> GetAllAshkhasToMoein(string Where)
        //{
        //    string query = "SELECT * FROM View_AshkhasToMoein " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_AshkhasToMoein>();
        //}

        //public static List<View_InputEvidence> GetAllInputEvidence(string Where)
        //{
        //    string query = "SELECT * FROM View_InputEvidence " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputEvidence>();
        //}

        //public static List<View_EvidenceCost> GetAllEvidenceCost(string Where)
        //{
        //    string query = "SELECT * FROM View_EvidenceCost " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_EvidenceCost>();
        //}

        //public static List<View_InputEvidenceRelatedInputAssignmentProductItem> GetAllInputEvidenceRelatedInputAssignmentProductItem(string Where)
        //{
        //    string query = "SELECT * FROM View_InputEvidenceRelatedInputAssignmentProductItem " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputEvidenceRelatedInputAssignmentProductItem>();
        //}

        //public static List<View_InputEvidenceItem> GetAllInputEvidenceItem(string Where)
        //{
        //    string query = "SELECT * FROM View_InputEvidenceItem " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputEvidenceItem>();
        //}

        //public static List<View_FinancialReport> GetAllFinancialReport(string Where)
        //{
        //    string query = "SELECT * FROM View_FinancialReport " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_FinancialReport>();
        //}
        //public static List<View_DeclareWeightsViewModel> GetAllDeclareWeightsBuy(string Where)
        //{
        //    string query = "SELECT * FROM View_DeclareWeightsBuy " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_DeclareWeightsViewModel>();
        //}

        //public static List<View_SdDetail> GetAllSdDetail(string Where)
        //{
        //    string query = "SELECT * FROM View_SdDetail " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_SdDetail>();
        //}

        //public static List<View_CheckStatus> GetAllCheckStatus(string Where)
        //{
        //    string query = "SELECT * FROM View_SdDetail " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_CheckStatus>();
        //}
        //public static List<View_DeclaringWeight> GetAllDeclaringWeight(string Where)
        //{
        //    string query = "SELECT * FROM View_DeclaringWeight " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_DeclaringWeight>();
        //}
        //public static List<View_BankTransactionDetail> GetAllBankTransactionDetail(string Where)
        //{
        //    string query = "SELECT * FROM View_BankTransactionDetail " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_BankTransactionDetail>();
        //}
        //public static List<View_Discount> GetAllDiscount(string Where)
        //{
        //    string query = "SELECT * FROM View_GetAllDiscount " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Discount>();
        //}
        //public static List<View_DriverCarrier> GetAllDriverCarrier(string Where)
        //{
        //    string query = "SELECT * FROM View_DriverCarrier " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_DriverCarrier>();
        //}
        //public static List<View_StatusCheck> GetAllStatusCheck(string Where)
        //{
        //    string query = "SELECT * FROM View_StatusCheck " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_StatusCheck>();
        //}
        //public static List<View_DeclareWeightsViewModel> GetAllDeclareWeightsSale(string Where)
        //{
        //    string query = "SELECT * FROM View_DeclareWeightsSale " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_DeclareWeightsViewModel>();
        //}
        //public static List<View_DeclareWeightsViewModel> GetAllDeclareWeightsOut(string Where)
        //{
        //    string query = "SELECT * FROM View_DeclareWeightsOut " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_DeclareWeightsViewModel>();
        //}


        //public static List<View_InputAssignment> GetAllInputAssignment(string Where)
        //{
        //    string query = "SELECT * FROM View_InputAssignment " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputAssignment>();
        //}

        //public static List<View_InputAssignmentAbstract> GetAllInputAssignmentAbstract(string Where)
        //{
        //    string query = "SELECT * FROM View_InputAssignmentAbstract " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputAssignmentAbstract>();
        //}
        //public static List<View_InputAssignmentProduct> GetAllInputAssignmentProduct(string Where)
        //{
        //    string query = "SELECT * FROM View_InputAssignmentProduct " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputAssignmentProduct>();
        //}
        //public static List<View_InputAssignmentProductItem> GetAllInputAssignmentProductItem(string Where)
        //{
        //    string query = "SELECT * FROM View_InputAssignmentProductItem " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputAssignmentProductItem>();
        //}
        //public static List<View_InputAssignmentWaybill> GetAllInputAssignmentWaybill(string Where)
        //{
        //    string query = "SELECT * FROM View_InputAssignmentWaybill " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputAssignmentWaybill>();
        //}
        //public static List<View_InputAssignmentWaybillAbstract> GetAllInputAssignmentWaybillAbstract(string Where)
        //{
        //    string query = "SELECT * FROM View_InputAssignmentWaybillAbstract " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_InputAssignmentWaybillAbstract>();
        //}

        //public static List<View_DiscountPayAndRecieveViewModel> GetDiscount(string Where)
        //{
        //    string query = "SELECT * FROM View_DiscountPayAndRecieve  " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_DiscountPayAndRecieveViewModel>();
        //}

        //public static List<View_Product> GetAllProduct(string Where)
        //{
        //    string query = "SELECT * FROM View_Product " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_Product>();
        //}

        //public static List<View_TransportationDetail> GetTransport(string Where)
        //{
        //    string query = "SELECT * FROM View_TransportationDetail " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_TransportationDetail>();
        //}

        //public static List<View_OwnStoreKalaInPricingOutBaha> GetOwnStoreKalaInPricingAvvalDore(string Where)
        //{
        //    string query = "SELECT * FROM View_OwnStoreKalaInPricingAvvalDore " + Where;
        //    DataTable ds = DAL.SQLDataTable(query, CNN);
        //    return ds.DataTableToList<View_OwnStoreKalaInPricingOutBaha>();
        //}


        #endregion

        //public static async Task<DealKarizRoot> GetAllTransactionsKarizFromDidar()
        //{

        //    //private static readonly HttpClient client = new HttpClient();

        //    //public async Task<string> GetPipelineData()
        //    //{
        //    //    string url = "https://app.didar.me/api/pipeline/list/0?apikey=5ooyoutiqrs5wyisrm83io361n0wm65l";
        //    //    HttpResponseMessage response = await client.PostAsync(url, null);
        //    //    response.EnsureSuccessStatusCode();
        //    //    string responseBody = await response.Content.ReadAsStringAsync();
        //    //    return responseBody;
        //    //}

        //    using (var client = new HttpClient())
        //    {
        //        //var request = new HttpRequestMessage(HttpMethod.Post, "{{didar}}/api/pipeline/list/0?apikey=" + DidarToken);
        //        var request = new HttpRequestMessage(HttpMethod.Post, CNNDidarApi + "/pipeline/list/0?apikey=" + DidarToken);
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        var ResultString = response.Content.ReadAsStringAsync().Result;
        //        //Console.WriteLine(await response.Content.ReadAsStringAsync());
        //        ResultString = ResultString.Replace("{}", "null");
        //        return JsonConvert.DeserializeObject<DealKarizRoot>(ResultString);
        //    }
        //}
    }

    #endregion

}

