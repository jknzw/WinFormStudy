using System;
using System.Collections.Generic;
using System.Text;

namespace SampleLibrary
{
    public class ReturnInfo
    {
        public int ReturnCode { get; set; } = 0;

        public List<string> ErrorMessageList { get; private set; } = new List<string>();

        public Dictionary<string, dynamic> ReturnValues { get; set; } = new Dictionary<string, dynamic>();

        public void AddErrorMessage(string message)
        {
            ErrorMessageList.Add(message);
        }

        public bool IsError()
        {
            if (ReturnCode != 0 || ErrorMessageList.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
