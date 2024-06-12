using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDeduplication.Models
{
    public class CellData
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        //public string Email { get; set; }

        public CellData(string path, string name, string phoneNum/*,string email*/)
        {
            this.Path = path;
            this.Name = name;
            this.PhoneNum = phoneNum;
            //this.Email = email;
        }
    }
}
