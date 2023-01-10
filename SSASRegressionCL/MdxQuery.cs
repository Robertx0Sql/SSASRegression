using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;
using System.Data;
using System.Xml;

namespace SSASRegressionCL
{
    public class MdxQuery : IDisposable, IMdxQuery
    {

        public MdxQuery(string connectionString)
        {
            this.ConnectionString = connectionString;
            _adomdConnection = new AdomdConnection();
            ConnectToDatabase();
        }
        public MdxQuery(string Server, string CatalogName)
        {
            this._server = Server;
            this._catalogName = CatalogName;
            _adomdConnection = new AdomdConnection();
            ConnectToDatabase();
        }
        public MdxQuery(string Server, string CatalogName, string EffectiveUserName)
        {


            this._server = Server;
            this._catalogName = CatalogName;
            this._EffectiveUserName = EffectiveUserName;
            _adomdConnection = new AdomdConnection();
            ConnectToDatabase();
        }

        private AdomdConnection _adomdConnection;
        private string _server;
        private string _catalogName;
        private string _sMdxCommand;
        string _EffectiveUserName;
        string _ConnectionString = string.Empty;

        private string Server
        {
            get
            {
                return _server;
            }
        }
        public string EffectiveUserName
        {
            get { return _EffectiveUserName; }
            set
            {
                _EffectiveUserName = value;
                ConnectToDatabase();
            }
        }

        public string CatalogName
        {
            get
            {
                return _catalogName;
            }
            set
            {
                _catalogName = value;
                _adomdConnection.ChangeDatabase(_catalogName);
            }
        }

        public DataTable GetCatalogs()
        {
            DataSet ds = _adomdConnection.GetSchemaDataSet(AdomdSchemaGuid.Catalogs, new object[0]);

            DataTable dt = ds.Tables[0];

            return dt;
        }
        public string MDXCommand
        {
            get
            {
                return _sMdxCommand;
            }
            set
            {
                _sMdxCommand = value;
            }
        }


        public string Provider
        {
            get
            {
                return Settings1.Default.Provider;
            }
        }

        public string ConnectionString
        {
            get
            {
                if (!_ConnectionString.IsNullOrEmptyOrWhitespace())
                { return _ConnectionString; }
                else
                {
                    if (!Server.IsNullOrEmptyOrWhitespace())
                    {
                        string cs = String.Format("Provider={0};data source={1};Initial Catalog={2};", Provider, Server, CatalogName);

                        if (!this.EffectiveUserName.IsNullOrEmptyOrWhitespace())
                        {
                            cs += String.Format(";EffectiveUserName={0};", this._EffectiveUserName);
                        }
                        _ConnectionString = cs;
                        return cs;
                    }
                    else
                        throw new ArgumentNullException("Server");
                }


            }
            set { _ConnectionString = value; }
        }
        private void ConnectToDatabase()
        {
            try
            {
                if (_adomdConnection.State == ConnectionState.Open && _adomdConnection.ConnectionString != ConnectionString)
                    _adomdConnection.Close();

                if (_adomdConnection.State != ConnectionState.Open)
                {
                    _adomdConnection.ConnectionString = ConnectionString;
                    _adomdConnection.Open();
                }
            }
            catch (AdomdConnectionException ex)
            {
                throw new ConnectionException(ex.Message, ex);
            }


        }

        private AdomdCommand GetAdomdCommand()
        {
            if (MDXCommand.Trim().Length == 0)
                throw new ArgumentNullException("No MDX Specified.");
            // ? test for bad mdx ? means a double execution of the mdx! -  "Content=Schema"

            AdomdCommand oCmd = null;
            try
            {
                oCmd = new AdomdCommand(MDXCommand, _adomdConnection);
            }
            catch (AdomdException ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            catch (InvalidOperationException exo)
            {
                throw;// new QueryException(exo.Message, exo);
            }
            catch (Exception ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            return oCmd;
        }

        public string ExecuteXmlReader(string Mdx)
        {
            MDXCommand = Mdx;
            AdomdCommand oCmd = GetAdomdCommand();
            string xml = null;
            try
            {
                XmlReader xmlReader = oCmd.ExecuteXmlReader();
                xml = xmlReader.ReadOuterXml();
                xmlReader.Close();
            }
            catch (AdomdException ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            catch (InvalidOperationException exo)
            {
                throw;// new QueryException(exo.Message, exo);
            }
            catch (Exception ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            return xml;
        }
        public CellSet ExecuteCommandCellSet(string Mdx)
        {
            MDXCommand = Mdx;
            return ExecuteCommandCellSet();
        }
        /// <summary>
        /// Execute an MDX command via the AdomdCommand.ExecuteCellSet method.
        /// </summary>
        private CellSet ExecuteCommandCellSet()
        {
            CellSet set = null;
            try
            {
                AdomdCommand oCmd = GetAdomdCommand();
                set = oCmd.ExecuteCellSet();
            }
            catch (AdomdException ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            catch (InvalidOperationException exo)
            {
                throw;// new QueryException(exo.Message, exo);
            }
            catch (Exception ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            return set;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="Mdx"></param>
        /// <returns></returns>
        public AdomdDataReader ExecuteCommandReader(string Mdx)
        {
            MDXCommand = Mdx;
            return ExecuteCommandReader();
        }

        private AdomdDataReader ExecuteCommandReader()
        {
            AdomdDataReader oReader = null;
            try
            {
                AdomdCommand oCmd = GetAdomdCommand();

                oReader = oCmd.ExecuteReader();
            }
            catch (AdomdException ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            catch (InvalidOperationException exo)
            {
                throw;// new QueryException(exo.Message, exo);
            }
            catch (Exception ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            return oReader;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Mdx"></param>
        /// <returns></returns>
        public DataTable ExecuteCommandDataSet(string Mdx)
        {
            MDXCommand = Mdx;
            return ExecuteCommandDataSet();
        }
        /// <summary>
        /// Execute an MDX command via a AdomdDataAdapter
        /// </summary>
        private DataTable ExecuteCommandDataSet()
        {
            DataTable dt = null;
            try
            {
                AdomdCommand oCmd = GetAdomdCommand();

                AdomdDataAdapter da = new AdomdDataAdapter(oCmd);

                dt = new DataTable();

                da.Fill(dt);
            }
            catch (AdomdException ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            catch (InvalidOperationException exo)
            {
                throw;// new QueryException(exo.Message, exo);
            }
           
            catch (Exception ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            return dt;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_adomdConnection != null)
                {
                    _adomdConnection.Dispose();
                    _adomdConnection = null;
                }
        }
        ~MdxQuery()
        {
            Dispose(false);
        }
    }
}
