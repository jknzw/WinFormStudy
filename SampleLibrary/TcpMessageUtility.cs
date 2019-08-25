using System;
using System.Collections.Generic;
using System.Text;

namespace SampleLibrary
{
    public class TcpMessageUtility
    {
        public const string HeaderAllMsg = "ALLMSG";
        public const string HeaderTargetMsg = "TARGETMSG";
        public const string HeaderConnect = "CONNECT";
        public const string HeaderName = "NAME";

        public const string TargetAll = "ALL";

        /// <summary>
        /// Header
        /// </summary>
        public string Header { get; set; } = HeaderAllMsg;

        /// <summary>
        /// クラ→サバ送信時:送信先
        /// </summary>
        public string SendToTarget { get; set; } = TargetAll;

        /// <summary>
        /// サバ→クラ送信時:送信元
        /// </summary>
        public string SendFromTarget { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        public TcpMessageUtility()
        {
        }

        public TcpMessageUtility(string readMessage)
        {
            string[] message = readMessage.Split(new char[] { ',' }, 4);
            Header = message[0];
            SendFromTarget = message[1];
            SendToTarget = message[2];
            Value = message[3];
        }

        public TcpMessageUtility(string header, string fromName, string toName, string value) : this()
        {
            Header = header;
            SendFromTarget = fromName;
            SendToTarget = toName;
            Value = value;
        }
        public string GetSendMessage()
        {
            return string.Join(",", Header, SendFromTarget, SendToTarget, Value);
        }
        public string GetRecvMessage()
        {
            return $"{SendFromTarget}>{Value}";
        }
        public string GetRecvTargetMessage()
        {
            return $"{SendFromTarget}->{SendToTarget}>{Value}";
        }
    }
}