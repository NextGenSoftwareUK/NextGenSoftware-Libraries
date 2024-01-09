using System;
using System.Net.WebSockets;

namespace NextGenSoftware.WebSocket
{
    public class ConnectedEventArgs : EventArgs
    {
        public Uri EndPoint { get; set; }
    }

    public class DisconnectedEventArgs : EventArgs
    {
        public Uri EndPoint { get; set; }
        public string Reason { get; set; }
    }

    public class WebSocketErrorEventArgs : EventArgs
    {
        public Uri EndPoint { get; set; }
        public string Reason { get; set; }
        public Exception ErrorDetails { get; set; }
    }

    public class DataReceivedEventArgs : CallBackWithDataReceivedBaseEventArgs
    {
        //public DataReceivedEventArgs(string id, string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult) : base(id, endPoint, isCallSuccessful, rawBinaryData, rawJSONData, webSocketResult)
        //{

        //}
    }

    public class DataSentEventArgs : CallBackWithDataBaseEventArgs
    {
        //public DataReceivedEventArgs(string id, string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult) : base(id, endPoint, isCallSuccessful, rawBinaryData, rawJSONData, webSocketResult)
        //{

        //}
    }

    public abstract class CallBackBaseEventArgs : EventArgs
    {
        public bool IsError { get; set; }
        public bool IsWarning { get; set; }
        public string Message { get; set; }
    }

    public abstract class CallBackWithDataReceivedBaseEventArgs : CallBackWithDataBaseEventArgs
    {
        public CallBackWithDataReceivedBaseEventArgs()
        {

        }

        //public CallBackWithDataReceivedBaseEventArgs(string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult)
        //{
        //    //EndPoint = endPoint;
        //    //IsCallSuccessful = isCallSuccessful;
        //    //RawJSONData = rawJSONData;
        //    //RawBinaryData = rawBinaryData;
        //    WebSocketResult = webSocketResult;
        //}

        //public bool IsError { get; set; }
        //public string Message { get; set; }
        //public Exception Excception { get; set; }
        //public string EndPoint { get; set; }
        //public bool IsCallSuccessful { get; set; }
        //public string RawJSONData { get; set; }
        //public byte[] RawBinaryData { get; set; }
        //public string RawBinaryDataAsString { get; set; }
        //public string RawBinaryDataDecoded { get; set; }
        public WebSocketReceiveResult WebSocketResult { get; set; }
    }

    public abstract class CallBackWithDataBaseEventArgs : CallBackBaseEventArgs
    {
        public CallBackWithDataBaseEventArgs()
        {

        }

        //public CallBackWithDataBaseEventArgs(string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult)
        public CallBackWithDataBaseEventArgs(Uri endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData)
        {
            EndPoint = endPoint;
            IsCallSuccessful = isCallSuccessful;
            RawJSONData = rawJSONData;
            RawBinaryData = rawBinaryData;
           // WebSocketResult = webSocketResult;
        }

        //public bool IsError { get; set; }
        //public string Message { get; set; }
        public Exception Excception { get; set; }
        public Uri EndPoint { get; set; }
        public bool IsCallSuccessful { get; set; }
        public string RawJSONData { get; set; }
        public byte[] RawBinaryData { get; set; }
        public string RawBinaryDataAsString { get; set; }
        public string RawBinaryDataDecoded { get; set; }
        //public WebSocketReceiveResult WebSocketResult { get; set; }
    }

    public abstract class CallBackBaseEventArgsWithId : CallBackWithDataBaseEventArgs
    {
        //public CallBackBaseEventArgsWithId(string id, string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult) : base(endPoint, isCallSuccessful, rawBinaryData, rawJSONData, webSocketResult)
        //{
        //    Id = id;
        //}

        public CallBackBaseEventArgsWithId() : base()
        {
          
        }

        public string Id { get; set; }
    }

    public abstract class CallBackBaseEventArgsWithDataAndId : CallBackWithDataReceivedBaseEventArgs
    {
        //public CallBackBaseEventArgsWithId(string id, string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult) : base(endPoint, isCallSuccessful, rawBinaryData, rawJSONData, webSocketResult)
        //{
        //    Id = id;
        //}

        public CallBackBaseEventArgsWithDataAndId() : base()
        {

        }

        public string Id { get; set; }
    }
}
