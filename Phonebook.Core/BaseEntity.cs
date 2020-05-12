using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Phonebook.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        { }
        public uint Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
    }
}
