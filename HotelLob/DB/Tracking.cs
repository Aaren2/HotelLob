//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelLob.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tracking
    {
        public int IdTracking { get; set; }
        public int IdLogin { get; set; }
        public System.DateTime DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string Error { get; set; }
    
        public virtual Login Login { get; set; }
    }
}
