﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.Base
{
    public class BaseService
    {
        #region コンストラクタ
        public BaseService()
        {

        }
        public BaseService(Form form)
        {
            ControlValuesDictionary = form.GetControlDictionary();
        }
        #endregion

        #region プロパティ
        public Dictionary<string, dynamic> ControlValuesDictionary { get; } = null;
        #endregion
    }
}
