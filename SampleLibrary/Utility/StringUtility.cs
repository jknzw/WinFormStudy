using System;
using System.Collections.Generic;
using System.Text;

namespace SampleLibrary.Utility
{
    public static class StringUtility
    {
        public static int GetInt(this string value)
        {
            int.TryParse(value, out int result);
            return result;
        }
        public static int GetInt(this object value)
        {
            return GetInt(value?.ToString());
        }
    }
}
