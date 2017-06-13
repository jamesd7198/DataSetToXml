using System;
using System.Collections.Generic;
using System.Data;

namespace DataSetToXml.DataSettingsHelper
{
    public class DataSettings : IDataSettings
    {
        public IDbConnection Connection { get; }
        public IDbCommand Command { get; }
        public IDbDataAdapter Adapter { get; }
        public IList<IDataParameter> Parameters { get; }

        public DataSettings(IDbConnection connection, IDbCommand command, IDbDataAdapter adapter,
            params IDataParameter[] parameters)
        {
            Connection = connection;
            Command = command;
            Adapter = adapter;
            Parameters = new List<IDataParameter>();

            foreach (IDataParameter parameter in parameters)
            {
                Parameters.Add(parameter);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                //Adapter?.Dispose(); //TODO: What about this?
                Command?.Dispose();
                Connection?.Dispose();
            }

            _disposed = true;
        }
    }

}
