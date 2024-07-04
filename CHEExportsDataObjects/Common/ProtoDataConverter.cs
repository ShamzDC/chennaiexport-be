using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{

    public static class ProtoDataConverter
    {
        private static TypeConverter GetTypeConverter(Type type)
        {
            return TypeDescriptor.GetConverter(type);
        }

        private static object? ConvertToTarget(Type SrcType, Type DestinationType, object? sourceObj, bool isProtoToBus = true)
        {
            if (SrcType.Name == typeof(string).Name && string.IsNullOrWhiteSpace(Convert.ToString(sourceObj)))
            {
                return null;
            }

            TypeConverter typeConverter = GetTypeConverter(SrcType);
            if (typeConverter.CanConvertTo(DestinationType))
            {
                if (!SrcType.FullName.Contains("DateTime"))
                {
                    return typeConverter.ConvertTo(sourceObj, DestinationType);
                }

                try
                {
                    return typeConverter.ConvertTo(sourceObj, DestinationType);
                }
                catch (Exception)
                {
                    if (sourceObj != null)
                    {
                        object dateTimeConversion = GetDateTimeConversion(sourceObj, isProtoToBus);
                        return typeConverter.ConvertTo(dateTimeConversion, DestinationType);
                    }
                }
            }

            typeConverter = GetTypeConverter(DestinationType);
            if (typeConverter.CanConvertFrom(SrcType))
            {
                if (!DestinationType.FullName.Contains("DateTime"))
                {
                    return typeConverter.ConvertFrom(sourceObj);
                }

                try
                {
                    return typeConverter.ConvertFrom(sourceObj);
                }
                catch (Exception)
                {
                    if (sourceObj != null)
                    {
                        object dateTimeConversion2 = GetDateTimeConversion(sourceObj, isProtoToBus);
                        return typeConverter.ConvertFrom(dateTimeConversion2);
                    }
                }
            }

            return null;
        }

        private static object GetDateTimeConversion(object sourceObj, bool isProtoToBus = true)
        {
            DateTime minValue = DateTime.MinValue;
            if (sourceObj != null)
            {
                string text = "dd/MM/yyyy";

                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(text))
                {
                    list.Add(text);
                }

                list.AddRange(CultureInfo.CurrentCulture.DateTimeFormat.GetAllDateTimePatterns().ToList());
                try
                {
                    if (isProtoToBus)
                    {
                        minValue = Convert.ToDateTime(DateTime.ParseExact(Convert.ToString(sourceObj), list.ToArray(), CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern));
                        return minValue;
                    }

                    return DateTime.ParseExact(Convert.ToString(sourceObj), list.ToArray(), CultureInfo.CurrentCulture).ToString(text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return sourceObj;
                }
            }

            return minValue;
        }

        public static void GetData<T1, T2>(T1 ProtoObject, T2 dataObjects)
        {
            if (ProtoObject == null || dataObjects == null)
            {
                return;
            }

            PropertyInfo[] properties = ProtoObject.GetType().GetProperties();
            PropertyInfo[] properties2 = dataObjects.GetType().GetProperties();
            foreach (PropertyInfo lPropertyInfo in properties2)
            {
                PropertyInfo propertyInfo = properties.Where((PropertyInfo x) => x.Name.Replace("_", "").ToLower() == lPropertyInfo.Name.Replace("_", "").ToLower()).FirstOrDefault();
                if (propertyInfo != null)
                {
                    lPropertyInfo.SetValue(dataObjects, ConvertToTarget(propertyInfo.PropertyType, lPropertyInfo.PropertyType, propertyInfo.GetValue(ProtoObject)));
                }
            }
        }

        public static void GetProto<T1, T2>(T1 dataObjects, T2 ProtoObject)
        {
            if (ProtoObject == null || dataObjects == null)
            {
                return;
            }

            PropertyInfo[] properties = dataObjects.GetType().GetProperties();
            PropertyInfo[] properties2 = ProtoObject.GetType().GetProperties();
            foreach (PropertyInfo lPropertyInfo in properties2)
            {
                PropertyInfo propertyInfo = properties.Where((PropertyInfo x) => x.Name.Replace("_", "").ToLower() == lPropertyInfo.Name.Replace("_", "").ToLower()).FirstOrDefault();
                if (propertyInfo != null)
                {
                    object obj = ConvertToTarget(propertyInfo.PropertyType, lPropertyInfo.PropertyType, propertyInfo.GetValue(dataObjects), isProtoToBus: false);
                    if (obj != null)
                    {
                        lPropertyInfo.SetValue(ProtoObject, obj);
                    }
                }
            }
        }

        public static bool HasProperty(dynamic obj, string name)
        {
            Type objType = obj.GetType();
            PropertyInfo lpropetyinfo = objType.GetProperty("Msg");
            if (lpropetyinfo != null)
            {
                return true;
            }
            return false;
        }

        public static void SetMessages(DataObjectBase aDataObjectBase, dynamic Proto)
        {

            if (HasProperty(Proto, "Msg"))
            {
                Proto.Msg = new protoMessage();
                if (aDataObjectBase.errorMsg_lsit != null)
                {
                    foreach (var obj in aDataObjectBase.errorMsg_lsit)
                    {
                        protoMsgDetail md = new protoMsgDetail();
                        md.MsgDescription = obj.ToString();
                        Proto.Msg.ErrorMessage.Add(md);
                    }
                    if (Proto.Msg.ErrorMessage != null && Proto.Msg.ErrorMessage.Count > 0)
                    {
                        Proto.Msg.HasError = true;
                    }
                }
                if (aDataObjectBase.infoMsg != null)
                {
                    protoMsgDetail md = new protoMsgDetail();
                    md.MsgDescription = aDataObjectBase.infoMsg;
                    Proto.Msg.InfoMessage = md;
                }
                if (aDataObjectBase.infoMsgList != null)
                {
                    foreach (var obj in aDataObjectBase.infoMsgList)
                    {
                        protoMsgDetail md = new protoMsgDetail();
                        md.MsgDescription = obj.ToString();
                        Proto.Msg.InfoMsgList.Add(md);
                    }
                    if (Proto.Msg.ErrorMessage != null && Proto.Msg.ErrorMessage.Count > 0)
                    {
                        Proto.Msg.HasError = true;
                    }
                }

            }
        }
    }
}
