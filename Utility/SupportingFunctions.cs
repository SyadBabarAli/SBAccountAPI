using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SBAccountAPI.Utility
{
    public class SupportingFunctions
    {
        /////////////////////////////////////////////////////////////////////////////////Supporting Functions///////////////////////////////////////////////////////////
        public static string strFeedbackDB = ConfigurationManager.ConnectionStrings["SBAccount"].ConnectionString;
        public static string strTicketDB = ConfigurationManager.ConnectionStrings["SBAccount"].ConnectionString;
        public static string strApplicationConfiguration = "Data Source=.;Initial Catalog=SBAccount;Integrated Security=True"; // ConfigurationManager.ConnectionStrings["SBAccount"].ConnectionString;

        public SqlConnectionStringBuilder GetConnectionFromDB(string connectionString)
        {
            string obj = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(obj);
            return builder;
        }

        public DataTable QueryIntoDataTable(string pQuery)
        {
            string Query = pQuery;
            //EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder(strApplicationConfiguration);
            DataSet objDataSet = SqlHelper.ExecuteDataset(strApplicationConfiguration, CommandType.Text, Query);
            DataTable obj = objDataSet.Tables[0];
            return obj;
        }
        public DataTable QueryIntoDataTableTicketDB(string pQuery)
        {
            string Query = pQuery;
            DataSet objDataSet = SqlHelper.ExecuteDataset(strTicketDB, CommandType.Text, Query);
            DataTable obj = objDataSet.Tables[0];
            return obj;
        }

        public object QuerySinglColumn(string sqlQuery, string columnName)
        {
            string SQLQuery = sqlQuery;
            DataSet objDataNotificationLog = SqlHelper.ExecuteDataset(strApplicationConfiguration, CommandType.Text, SQLQuery);
            object objCNT = objDataNotificationLog.Tables[0].Rows[0][columnName];
            return objCNT;
        }

        public object GetColumnName(DataRow row, List<string> pListColumn, string[] nameOfColumns)
        {
            List<string> listColumn = pListColumn;
            object result = null;
            bool isTrue = false;
            foreach (var nameOfColumn in nameOfColumns)
            {
                foreach (var columnName in listColumn)
                {
                    if (columnName.IndexOf(nameOfColumn) != -1)
                    {
                        result = columnName;
                        isTrue = true;
                        break;
                    }
                }
                if (isTrue)
                {
                    break;
                }
            }

            if (result != null)
            {
                result = row[result.ToString()];
            }

            return result;
        }


        public void SendEmail(string subjectEmail, string bodyEmail, bool isAttachment, string toEmail, string ccEmail, string[] attachments)
        {
            try
            {
                string EmailId = ConfigurationManager.AppSettings["EmailId"];
                string Password = ConfigurationManager.AppSettings["Password"];
                MailMessage objMailMessage = new MailMessage(EmailId, toEmail);
                SmtpClient objSmtpClient = new SmtpClient
                {
                    Host = "hoexedg4.aku.edu",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(toEmail, Password)
                };

                if (ccEmail.Trim().Length > 0)
                {
                    objMailMessage.CC.Add(ccEmail);
                }

                objMailMessage.Subject = subjectEmail;
                objMailMessage.Body = bodyEmail.Replace("\n", "<br />");
                objMailMessage.IsBodyHtml = true;
                objMailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                if (isAttachment)
                {
                    foreach (var item in attachments)
                    {
                        objMailMessage.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }
                }

                objSmtpClient.Send(objMailMessage);
                objSmtpClient.Dispose();
                objMailMessage.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Create Log For Service Status
        public void WriteErrorLog(string message)
        {
            string PathTaskServiceLog = ConfigurationManager.AppSettings["PathTaskServiceLog"];
            StreamWriter sw = new StreamWriter(PathTaskServiceLog, true);
            sw.WriteLine(DateTime.Now.ToString() + ": " + message);
            sw.Flush();
            sw.Close();
        }
    }
}