using System.Net.Http;
using System.Net;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using CHEExportsProto;

namespace CHEExportsAPI
{
    public static class Common
    {
        public static HttpClient httpClient { get; set; }
       

        public static bool HasProperty(dynamic obj)
        {
            Type objType = obj.GetType();
            PropertyInfo lpropetyinfo = objType.GetProperty("Msg");
            if (lpropetyinfo != null)
            {
                return true;
            }
            return false;
        }

        public static T GetErrorMessage<T>(this Exception e, T obj)
        {
            dynamic obje = obj;
            if (e is RpcException)
            {
                RpcException ex = e as RpcException;
                if (ex.StatusCode == Grpc.Core.StatusCode.Unauthenticated)
                {
                    throw new UnauthorizedAccessException() as Exception;
                }
                else
                {
                    if ((HasProperty(obj)))
                    {
                        obje.Msg = new protoMessage();
                        obje.Msg.ErrorMessage.Add("Unable to connect to service. Please try later.");
                    }

                }
            }
            else if (HasProperty(obj))
            {
                obje.Msg = new protoMessage();
                obje.Msg.ErrorMessage.Add("Unable to connect to service. Please try later.");
            }

            return obj;
        }


        public static IActionResult GetResponse<T>(this ControllerBase ControllerBase, T obj)
        {
            if (HasProperty(obj) && obj.GetType().GetProperty("Msg").GetValue(obj) != null)
            {
                protoMessage msgObj = obj.GetType().GetProperty("Msg").GetValue(obj) as protoMessage;
                if (msgObj.HasError)
                {
                    return ControllerBase.BadRequest(msgObj);
                }
            }
            return ControllerBase.Ok(obj);
        }

        public static IActionResult GetErrorMessage(this ControllerBase ControllerBase, Exception e)
        {
            string errorMessage = "Bula, We are currently facing some technical issues with the Portal right now.Please try again after some time.";
            HttpStatusCode stCode = HttpStatusCode.InternalServerError;
            if (e is RpcException)
            {
                RpcException ex = e as RpcException;
                if (ex.StatusCode == Grpc.Core.StatusCode.Unauthenticated)
                {
                    stCode = HttpStatusCode.Unauthorized;
                    return ControllerBase.Unauthorized("User not authorised or token expired. Please login again.");

                }
                else if (ex.StatusCode == Grpc.Core.StatusCode.Unavailable)
                {
                    stCode = HttpStatusCode.ServiceUnavailable;
                    return ControllerBase.StatusCode((int)stCode, errorMessage);
                }
                else if (ex.StatusCode == Grpc.Core.StatusCode.Internal && ex.Status.Detail == "Error starting gRPC call. HttpRequestException: No connection could be made because the target machine actively refused it. SocketException: No connection could be made because the target machine actively refused it.")
                {
                    stCode = HttpStatusCode.ServiceUnavailable;
                    return ControllerBase.StatusCode((int)stCode, errorMessage);
                }
                else if (ex.StatusCode == Grpc.Core.StatusCode.Internal && ex.Status.Detail.Contains("Faulted state"))
                {
                    stCode = HttpStatusCode.ServiceUnavailable;
                    errorMessage = "Core system service temporarily unavailable. Please contact system administrator.";
                    return ControllerBase.StatusCode((int)stCode, errorMessage);
                }

                else if (ex.StatusCode == Grpc.Core.StatusCode.Internal && ex.Status.Detail.Contains("No connection could be made because the target machine actively refused it"))
                {
                    stCode = HttpStatusCode.ServiceUnavailable;
                    errorMessage = "Sevice temporarily unavailble. Please try after some time.";
                    return ControllerBase.StatusCode((int)stCode, errorMessage);
                }
                else
                {
                    RpcException exc = e as RpcException;
                    stCode = HttpStatusCode.InternalServerError;
                    errorMessage = exc.Status.Detail;
                    return ControllerBase.StatusCode((int)stCode, errorMessage);
                }
            }
            protoMessage Msg = new protoMessage();
            Msg.ErrorMessage.Add(new protoMsgDetail() { MsgDescription = errorMessage });
            return ControllerBase.StatusCode((int)stCode, Msg);
        }

    }
}
