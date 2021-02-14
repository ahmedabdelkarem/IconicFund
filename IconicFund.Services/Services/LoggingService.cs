using IconicFund.Helpers.Enums;
using IconicFund.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using IconicFund.Repositories;
using IconicFund.Models.Entities;
using System.Linq;

namespace IconicFund.Services.Services
{
    public class LoggingService : ILoggingService
    {

        private readonly IBaseRepository repository;
        public LoggingService(IBaseRepository repository)
        {
            this.repository = repository;
        }


        /// <summary>
        /// set data as xml to save object to database 
        /// method convert any entity class to xml 
        /// return xml object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Object"></param>
        /// <returns></returns>
        public string SetDataToXML<T>(T Object) where T : class
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement RowData = doc.CreateElement(string.Empty, "RowData", string.Empty);
            doc.AppendChild(RowData);


            // get all public static properties of MyClass type
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();
           

            // write property names
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
               string ColumnName =  propertyInfo.Name;
                string ColumnValue = propertyInfo.GetValue(Object)?.ToString();

                XmlElement Item = doc.CreateElement(string.Empty, ColumnName, string.Empty);
                XmlText descriptionText = doc.CreateTextNode(ColumnValue);
                Item.AppendChild(descriptionText);
                RowData.AppendChild(Item);
                 
            }
             
            return doc.InnerXml;
        }


        /// <summary>
        /// recived xml object and convert it to dicionary 
        /// return dictionary of data 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public async Task<Dictionary<string , string>> GetDataFromXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList DataList = doc.GetElementsByTagName("RowData");
            Dictionary<string, string> RowData = new Dictionary<string, string>();

            foreach (XmlNode item in DataList)
            {
                for (int i = 0; i < item.ChildNodes.Count; i++)
                {
                    RowData.Add(item.ChildNodes[i].Name, item.ChildNodes[i].InnerText);
                }
            }

            return RowData;
        }

        /// <summary>
        /// log any action and save data as xml object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_categoty"></param>
        /// <param name="_action"></param>
        /// <param name="OldData"></param>
        /// <param name="NewData"></param>
        /// <param name="_UserId"></param>
        /// <param name="RowId"></param>
        /// <returns></returns>
        public async Task LogActionData<T>(LoggingCategory _categoty, LoggingAction _action, T OldData, T NewData, Guid _UserId, string RowId) where T : class 
        {

            var OData = SetDataToXML<T>(OldData);

            var NData = NewData != null ? SetDataToXML<T>(NewData) : "";

            var _LogData = new SystemLogging
            {
                LoggingAction = _action,
                LoggingCategory = _categoty,
                NewData = NData,
                ActionDate = DateTime.Now,
                OldData = OData,
                RowID = RowId,
                UserID = _UserId,
            };

           await  repository.AddAsync<SystemLogging>(_LogData);

           await repository.SaveChangesAsync(); 

        }


        /// <summary>
        /// search in logging 
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="RowId"></param>
        /// <returns></returns>
        public async Task<List<SystemLogging>> SearchInLogging(DateTime? DateFrom = null, DateTime? DateTo = null, string RowId = ""
            , LoggingAction? LoggingAction = null, LoggingCategory? LoggingCategory = null)
        {
            var _result = (await repository.GetAllWhereAsync<SystemLogging>(a => (DateFrom == null || a.ActionDate >= DateFrom)
            && ( DateTo == null || a.ActionDate <= DateTo ) 
            && (string.IsNullOrEmpty(RowId) || a.RowID.Equals(RowId))
            && (LoggingAction == null || a.LoggingAction == LoggingAction) &&
            (LoggingCategory == null || a.LoggingCategory == LoggingCategory), i=>i.UserData)).OrderByDescending(a=>a.ActionDate).ToList();

            return _result;
        }

        

        public async Task<SystemLogging> GetById(long Id)
        {
            return await repository.FirstOrDefaultAsync<SystemLogging>(a => a.ID == Id);
        }
    }
}
