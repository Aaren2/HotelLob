using HotelLob.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLob.ClassHelper
{
    public class EFClass
    {
        public static Entities context { get; } = new Entities();
    }
}
