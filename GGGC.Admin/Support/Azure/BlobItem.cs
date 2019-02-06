using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.Support.Azure
{
    public class BlobItem
    {
        public String Name { get; set; }
        public String BlobType { get; set; }
        public DateTime LastModified { get; set; }
        public String LastModifiedText { get; set; }
        public long Length { get; set; }
        public String LengthText { get; set; }
        public String ContentType { get; set; }
        public String Encoding { get; set; }
        public String ETag { get; set; }
        public String CopyState { get; set; }
    }
}
