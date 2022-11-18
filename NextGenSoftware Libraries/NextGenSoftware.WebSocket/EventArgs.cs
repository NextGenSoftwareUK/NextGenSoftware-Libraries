using System;
using System.Net.WebSockets;

namespace NextGenSoftware.WebSocket
{
    public class ConnectedEventArgs : EventArgs
    {
        public string EndPoint { get; set; }
    }

    public class DisconnectedEventArgs : EventArgs
    {
        public string EndPoint { get; set; }
        public string Reason { get; set; }
    }

    public class WebSocketErrorEventArgs : EventArgs
    {
        public string EndPoint { get; set; }
        public string Reason { get; set; }
        public Exception ErrorDetails { get; set; }
    }
    public class DataReceivedEventArgs : CallBackBaseEventArgsWithId
    {
        public DataReceivedEventArgs(string id, string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult) : base(id, endPoint, isCallSuccessful, rawBinaryData, rawJSONData, webSocketResult)
        {
            
        }
    }
    
    public abstract class CallBackBaseEventArgs : EventArgs
    {
        public CallBackBaseEventArgs()
        {

        }

        public CallBackBaseEventArgs(string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult)
        {
            EndPoint = endPoint;
            IsCallSuccessful = isCallSuccessful;
            RawJSONData = rawJSONData;
            RawBinaryData = rawBinaryData;
            WebSocketResult = webSocketResult;
        }

        public bool IsError { get; set; }
        public string Message { get; set; }
        public Exception Excception { get; set; }
        public string EndPoint { get; set; }
        public bool IsCallSuccessful { get; set; }
        public string RawJSONData { get; set; }
        public byte[] RawBinaryData { get; set; }
        public WebSocketReceiveResult WebSocketResult { get; set; }
    }

    public abstract class CallBackBaseEventArgsWithId : CallBackBaseEventArgs
    {
        public CallBackBaseEventArgsWithId(string id, string endPoint, bool isCallSuccessful, byte[] rawBinaryData, string rawJSONData, WebSocketReceiveResult webSocketResult) : base(endPoint, isCallSuccessful, rawBinaryData, rawJSONData, webSocketResult)
        {
            Id = id;
        }

        public CallBackBaseEventArgsWithId() : base()
        {
          
        }

        public string Id { get; set; }
    }
}
