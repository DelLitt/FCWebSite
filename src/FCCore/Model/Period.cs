using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Period
    {
        public short Id { get; set; }
        public byte Duration { get; set; }
        public short gameFormatId { get; set; }
        public byte Number { get; set; }
    }
}
