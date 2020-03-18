using System;
using System.Collections.Generic;
using System.Text;

namespace DataCaching.Data
{
    public interface isaCacheItem<O> :  IComparable<O>
    {
        int Id { get; set; }
    }
}
