using Humanizer;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;

namespace BTL_LTWeb.Models
{
    public partial class TDanhSachCuaHang
    {
        public string SDTCuaHang { get; set; }
        public string DiaChi { get; set; }   
        public string KhuVuc { get; set; }   
        public float KinhDo { get; set; }
        public float ViDo { get; set; }
    }
}

