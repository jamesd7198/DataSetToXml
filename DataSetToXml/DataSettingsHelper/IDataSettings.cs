using System;
using System.Collections.Generic;
using System.Data;

namespace DataSetToXml.DataSettingsHelper
{
    public interface IDataSettings : IDisposable
    {
        IDbConnection Connection { get; }
        IDbCommand Command { get; }
        IDbDataAdapter Adapter { get; }
        IList<IDataParameter> Parameters { get; }
    }

}
