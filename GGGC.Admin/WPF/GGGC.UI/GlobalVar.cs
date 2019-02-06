using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGGC.UI
{
    public static class GlobalVar
    {
        static string _globalValue;

        public static string User
        {
            get
            {
                return _globalValue;
            }
            set
            {
                _globalValue = value;
            }
        }

    }
}
