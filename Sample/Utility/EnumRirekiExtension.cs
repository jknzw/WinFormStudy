using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Utility
{
    public enum EnumRireki : int
    {
        // "年月日", "用途", "収入", "支出", "残金", "備考"
        Ymd,
        Youto,
        Shunyu,
        Shishutsu,
        Zankin,
        Biko,
    }

    public static class EnumRirekiExtension
    {
        public static readonly Dictionary<EnumRireki, string> RirekiDic = new Dictionary<EnumRireki, string>()
        {
            { EnumRireki.Ymd,"年月日"},
            { EnumRireki.Youto,"用途"},
            { EnumRireki.Shunyu,"収入"},
            { EnumRireki.Shishutsu,"支出"},
            { EnumRireki.Zankin,"残金"},
            { EnumRireki.Biko,"備考"},
        };

        public static int GetInt(this EnumRireki rireki)
        {
            return (int)rireki;
        }
    }
}
