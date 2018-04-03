using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class SettingsVisibility
    {
        public int Id { get; set; }
        public short Authorized { get; set; }
        public short Main { get; set; }
        public short News { get; set; }
        public short Reserve { get; set; }
        public short Youth { get; set; }
    }
}
