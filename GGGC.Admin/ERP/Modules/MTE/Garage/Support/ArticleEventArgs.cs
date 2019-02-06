using System;
using System.Collections.Generic;
using System.Linq;
using GGGC.Client.Entities;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class ArticleEventArgs : EventArgs
    {
        public ArticleEventArgs(Article _obj, bool isNew)
        {
            Articles = _obj;
            IsNew = isNew;
        }

        public Article Articles { get; set; }
        public bool IsNew { get; set; }
    }
}
