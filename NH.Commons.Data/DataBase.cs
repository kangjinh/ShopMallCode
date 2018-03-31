using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace NH.Commons.Data
{
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public class DataBase : IDisposable
    {
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private SqlConnection con = null;
        /// <summary>
        /// 数据库在配置文件中的键名称
        /// </summary>
        private string DatabaseKeyName = "CMSDataConnection";
        /// <summary>
        /// 数据库链接类别，默认值为0，表示数据库CMS_YSYY，值为1表示数据库dnt36
        /// </summary>
        //private int DatabaseCategory = 0;
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        private string DatabaseConnectionString = string.Empty;
        /// <summary>
        /// 初始化数据库对象
        /// </summary>
        public DataBase()
        {
            //this.DatabaseCategory = 0;
            this.DatabaseKeyName = "CMSDataConnection";
        }
        ///// <summary>
        ///// 初始化数据库对象
        ///// </summary>
        ///// <param name="databaseCategory">数据库链接类别，默认值为0，表示数据库CMS_YSYY，值为1表示数据库dnt36</param>
        //public DataBase(int databaseCategory)
        //{
        //    this.DatabaseCategory = databaseCategory;
        //}
        /// <summary>
        /// 初始化数据库对象
        /// </summary>
        /// <param name="connectionString">数据库链接字符串</param>
        public DataBase(string connectionString)
        {
            this.DatabaseConnectionString = connectionString;
        }

        /// <summary>
        /// 数据库配置枚举
        /// </summary>
        /// <param name="dbConfig"></param>
        public DataBase(int dbConfig)
            : this((DBConfig)dbConfig)
        {

        }

        /// <summary>
        /// 数据库配置枚举
        /// </summary>
        /// <param name="dbConfig"></param>
        public DataBase(DBConfig dbConfig)
        {
            switch (dbConfig)
            {
                case DBConfig.Default:
                    this.DatabaseKeyName = "CMSDataConnection";
                    break;                
                default:
                    this.DatabaseKeyName = "CMSDataConnection";
                    break;
            }
        }
        ///// <summary>
        ///// 获取数据库链接键
        ///// </summary>
        ///// <returns></returns>
        //private string GetConnectionString()
        //{
        //    string result = "";
        //    switch (this.DatabaseCategory)
        //    {
        //        case 0:
        //            result = "CMSDataConnection";
        //            break;
        //        case 1:
        //            result = "DNTDataConnection";
        //            break;
        //        default:
        //            result = "CMSDataConnection";
        //            break;
        //    }
        //    return result;
        //}
        /// <summary>
        /// 打开数据库链接
        /// </summary>
        public void Open()
        {
            try
            {
                for (int i = 0; i < 5; i++)  //尝试链接5次，一旦链接上就跳出循环，此处增加循环是为了避免服务器重启时数据库链接出现链接中断
                {
                    if (con == null)
                    {
                        if (string.IsNullOrEmpty(this.DatabaseConnectionString))
                            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[this.DatabaseKeyName].ToString());
                        else
                            con = new SqlConnection(this.DatabaseConnectionString);
                    }
                    //判断链接状态，如果是中断状态，则先关闭再链接
                    if (con.State == ConnectionState.Broken)
                    {
                        con.Close();
                        con.Open();
                    }
                    //判断链接状态，如果是关闭状态则打开
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    //判断链接状态，如果是打开状态则跳出循环
                    if (con != null && con.State == ConnectionState.Open)
                        break;
                    else
                        System.Threading.Thread.Sleep(100);
                }  //循环结束
            }
            catch (SqlException sex)
            {
                LogHelper.WriteLog("数据库链接出错：" + sex.ToString(), "DB_OpenDataBaseConnectionSqlException.txt", true);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("数据库链接出错：" + ex.ToString(), "DB_OpenDataBaseConnectionException.txt", true);
            }
            /*
            if (string.IsNullOrEmpty(this.DatabaseConnectionString))
            {
                if (con == null)
                    con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[this.DatabaseKeyName].ToString());
            }
            else
            {
                if (con == null)
                    con = new SqlConnection(this.DatabaseConnectionString);
            }
            if (con.State == ConnectionState.Closed)
                con.Open();
            */
        }
        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        public void Close()
        {
            try
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("关闭数据库链接出错：" + ex.ToString(), "DB_CloseDataBaseConnectionException.txt", true);
            }
            /*
            if (con != null)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            */
        }
        /// <summary>
        /// 实现接口IDisposable中的方法Disponse,释放数据库链接资源
        /// </summary>
        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }

        #region [SqlParameter]传入参数并且转换为SqlParameter类型
        /// <summary>
        /// 转换参数
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小[长度]</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// 传入返回值参数
        /// </summary>
        /// <param name="ParamName">存储过程名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小[长度]</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null, 0);
        }
        /// <summary>
        /// 传入返回值参数（加入小数参数）
        /// </summary>
        /// <param name="ParamName">存储过程名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Scale">小数点位数（用于Output类型参数）</param>
        /// <param name="Size">参数大小[长度]</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size, byte Scale)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null, Scale);
        }
        /// <summary>
        /// 传入返回值参数
        /// </summary>
        /// <param name="ParamName">存储过程名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小[长度]</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeReturnParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null);
        }
        /// <summary>
        /// 初始化参数值
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小[长度]</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            return MakeParam(ParamName, DbType, Size, Direction, Value, 0);
        }
        /// <summary>
        /// 初始化参数值
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小[长度]</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <param name="Scale">小数点位数（用于Output类型参数）</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value, byte Scale)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;
            else if (Scale > 0)
                param.Scale = Scale;
            return param;
        }

        #endregion

        #region [int]执行指定存储过程public int RunProc(string ProcName)
        /// <summary>
        ///  执行指定存储过程
        /// </summary>
        /// <param name="ProcName">存储过程的名称</param>
        /// <returns>返回存储过程返回值</returns>
        public int RunProc(string ProcName)
        {
            SqlCommand cmd = CreateCommand(ProcName, null);
            cmd.ExecuteNonQuery();
            int result = (int)cmd.Parameters["ReturnValue"].Value;
            cmd.Dispose();
            this.Close();
            return result;
        }

        /// <summary>
        /// 执行带有不定参数的存储过程
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <param name="prams">存储过程所需参数</param>
        /// <returns>返回存储过程返回值</returns>
        public int RunProc(string ProcName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(ProcName, prams);
            cmd.ExecuteNonQuery();
            int result = (int)cmd.Parameters["ReturnValue"].Value;
            cmd.Dispose();
            this.Close();
            return result;
        }

        /// <summary>
        /// 执行带有不定参数的存储过程或命令文本
        /// </summary>
        /// <param name="ProcName">存储过程的名称或命令文本</param>
        /// <param name="prams">存储过程所需参数</param>
        /// <param name="size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns>返回存储过程返回值</returns>
        public int RunProc(string ProcName, SqlParameter[] prams, Int32 size)
        {
            SqlCommand cmd = CreateCommand(ProcName, prams, size);
            cmd.ExecuteNonQuery();
            int result = (int)cmd.Parameters["ReturnValue"].Value;
            cmd.Dispose();
            this.Close();
            return result;
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象
        /// </summary>
        /// <param name="ProcName">存储过程的名称</param>
        /// <param name="DataReader">返回存储过程返回值</param>
        public void RunProc(string ProcName, out SqlDataReader DataReader)
        {
            SqlCommand cmd = CreateCommand(ProcName, null);
            DataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //return (int)cmd.Parameters["ReturnValue"].Value;
        }

        /// <summary>
        /// 执行带有不定参数的存储过程，返回SqlDataReader对象
        /// </summary>
        /// <param name="ProcName">存储过程的名称</param>
        /// <param name="prams">存储过程所需参数</param>
        /// <param name="DataReader">输出SqlDataReader对象</param>
        public void RunProc(string ProcName, SqlParameter[] prams, out SqlDataReader DataReader)
        {
            SqlCommand cmd = CreateCommand(ProcName, prams);
            DataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //return (int)cmd.Parameters["ReturnValue"].Value;
        }

        #endregion

        #region [int]执行文本命令,此方法适合更新,删除,添加SQL语句public int RunNonQueryText(string Sqlstr)
        /// <summary>
        /// 执行文本命令,此方法适合更新,删除,添加SQL语句
        /// </summary>
        /// <param name="Sqlstr">SQL语句</param>
        /// <returns>返回影响的行数</returns>
        public int RunQuery(string SqlText)
        {
            SqlCommand cmd = CreateCommand(SqlText);
            int returnvalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            this.Close();
            return returnvalue;
        }

        /// <summary>
        /// 执行指定SQL查询文本命令,返回SqlDataReader对象
        /// </summary>
        /// <param name="Sqlstr">执行SQL语句</param>
        /// <param name="reader">输入的SqlDataReader对象</param>
        public void RunQuery(string SqlText, out SqlDataReader reader)
        {
            SqlCommand cmd = CreateCommand(SqlText);
            reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 执行SqlCommand中的ExecuteNonQuery方法
        /// </summary>
        /// <param name="SqlText"></param>
        /// <returns></returns>
        public int RunNonQuery(string SqlText)
        {
            SqlCommand cmd = CreateCommand(SqlText);
            int revalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            this.Close();
            return revalue;
        }

        /// <summary>
        /// 执行SqlCommand中的ExecuteNonQuery方法
        /// </summary>
        /// <param name="SqlText"></param>
        /// <returns></returns>
        public int RunNonQuery(string SqlText, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(SqlText, prams);
            int revalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            this.Close();
            return revalue;
        }

        /// <summary>
        /// 执行SqlCommand中的ExecuteNonQuery方法
        /// </summary>
        /// <param name="SqlText"></param>
        /// <param name="Size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns></returns>
        public int RunNonQuery(string SqlText, SqlParameter[] prams, Int32 Size)
        {
            SqlCommand cmd = CreateCommand(SqlText, prams, Size);
            int revalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            this.Close();
            return revalue;
        }

        /// <summary>
        /// 执行SqlCommand中的ExecuteScalar方法
        /// </summary>
        /// <param name="SqlText"></param>
        /// <returns></returns>
        public object RunScalar(string SqlText)
        {
            SqlCommand cmd = CreateCommand(SqlText);
            object revalue = cmd.ExecuteScalar();
            cmd.Dispose();
            this.Close();
            return revalue;
        }

        /// <summary>
        /// 执行SqlCommand中的ExecuteScalar方法
        /// </summary>
        /// <param name="SqlText"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public object RunScalar(string SqlText, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(SqlText, prams);
            object revalue = cmd.ExecuteScalar();
            cmd.Dispose();
            this.Close();
            return revalue;
        }


        /// <summary>
        /// 执行SqlCommand中的ExecuteScalar方法
        /// </summary>
        /// <param name="SqlText"></param>
        /// <param name="prams"></param>
        /// <param name="Size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns></returns>
        public object RunScalar(string SqlText, SqlParameter[] prams, Int32 Size)
        {
            SqlCommand cmd = CreateCommand(SqlText, prams, Size);
            object revalue = cmd.ExecuteScalar();
            cmd.Dispose();
            this.Close();
            return revalue;
        }

        /// <summary>
        /// 执行命今并返回SqlDataReader对象
        /// </summary>
        /// <param name="Sqlstr"></param>
        /// <returns></returns>
        public SqlDataReader RunReader(string SqlText)
        {
            return RunReader(SqlText, null, 1);
        }

        /// <summary>
        /// 执行存储过程并返回SqlDataReader对象
        /// </summary>
        /// <param name="SqlText"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public SqlDataReader RunReader(string SqlText, SqlParameter[] prams)
        {
            return RunReader(SqlText, prams, 0);
        }

        /// <summary>
        /// 执行命今并返回SqlDataReader对象
        /// </summary>
        /// <param name="SqlText"></param>
        /// <param name="prams"></param>
        /// <param name="Size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns></returns>
        public SqlDataReader RunReader(string SqlText, SqlParameter[] prams, Int32 Size)
        {
            SqlCommand cmd = CreateCommand(SqlText, prams, Size);
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }

        #endregion

        #region [SqlCommand]创建一个SqlCommand对象以此来执行存储过程private SqlCommand CreateCommand(string ProcName, SqlParameter[] prams)
        /// <summary>
        /// 创建一个SqlCommand对象以此来执行存储过程
        /// </summary>
        /// <param name="ProcName">存储过程的名称</param>
        /// <param name="prams">存储过程所需参数</param>
        /// <returns>返回SqlCommand对象</returns>
        private SqlCommand CreateCommand(string ProcName, SqlParameter[] prams)
        {
            this.Open();//确认链接打开

            SqlCommand cmd = new SqlCommand(ProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // 依次把参数传入存储过程
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }

            // 加入返回参数
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;
        }

        /// <summary>
        /// 创建一个SqlCommand对象以此来执行存储过程
        /// </summary>
        /// <param name="ProcName">存储过程的名称</param>
        /// <param name="prams">存储过程所需参数</param>
        /// <param name="Size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns>返回SqlCommand对象</returns>
        private SqlCommand CreateCommand(string ProcName, SqlParameter[] prams, Int32 Size)
        {
            this.Open();//确认链接打开

            SqlCommand cmd = new SqlCommand(ProcName, con);
            if (Size == 1)
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            // 依次把参数传入存储过程
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }

            // 加入返回参数
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;
        }

        #endregion

        #region [SqlCommand]执行传入SQL语句以创建SqlCommand对象private SqlCommand CreateCommand(string Sqlstr)
        /// <summary>
        /// 创建一个SqlCmmand对象以此来执行查询文本
        /// </summary>
        /// <param name="Sqlstr">SQL语句</param>
        /// <returns></returns>
        private SqlCommand CreateCommand(string Sqlstr)
        {
            this.Open(); //确认链接打开
            SqlCommand cmd = new SqlCommand(Sqlstr, con);//初始化具有查询文本和SqlCommand类的新实例
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        #endregion

        #region [DataSet]执行指定SQL查询文本命令并返回 DataSet对象public DataSet RunTextReturn(string Sqlstr)
        /// <summary>
        /// 执行指定SQL查询文本命令并返回 DataSet
        /// </summary>
        /// <param name="Sqlstr">执行查询读取数据的SQL语句</param>
        /// <returns></returns>
        public DataSet RunTextReturn(string Sqlstr)
        {
            SqlCommand cmd = CreateCommand(Sqlstr);
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            cmd.Dispose();
            dap.Dispose();
            this.Close();
            return ds;
        }

        #endregion

        #region [DataSet]执行指定SQL查询文本命令并返回带有表名的DataSet对象public DataSet RunTextReturn(string Sqlstr, string tbName)
        /// <summary>
        /// 执行指定SQL查询文本命令并返回带有表名的DataSet
        /// </summary>
        /// <param name="Sqlstr">执行查询读取数据的SQL语句</param>
        /// <param name="tbName">DataSet中的表名</param>
        /// <returns></returns>
        public DataSet RunTextReturn(string Sqlstr, string tbName)
        {
            SqlCommand cmd = CreateCommand(Sqlstr);
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dap.Fill(ds, tbName);
            cmd.Dispose();
            dap.Dispose();
            this.Close();
            return ds;
        }

        #endregion

        #region [DataSet]执行存储过程或查询命令文本，并且返回DataSet数据集public DataSet RunProcReturn(string ProcName, string tbName, Int32 Size)
        /// <summary>
        /// 执行存储过程或查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="ProcName">存储过程的名称或命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="tbName">数据表名称</param>
        /// <param name="Size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet RunProcReturn(string ProcName, string tbName, Int32 Size)
        {
            SqlDataAdapter dap = CreateDataAdapter(ProcName, null, Size);
            DataSet ds = new DataSet();
            dap.Fill(ds, tbName);
            dap.Dispose();
            this.Close();
            //得到执行成功返回值
            return ds;
        }

        #endregion

        #region [DataSet]执行指定的存储过程返回DataSet对象public DataSet RunProcReturn(string ProcName, string tbName)
        /// <summary>
        /// 执行指定的存储过程返回DataSet对象
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <param name="tbName">DataSet中的表名</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet RunProcReturn(string ProcName, string tbName)
        {
            SqlDataAdapter dap = CreateDataAdapter(ProcName, null, 1);
            DataSet ds = new DataSet();
            dap.Fill(ds, tbName);
            dap.Dispose();
            this.Close();
            return ds;
        }

        #endregion

        #region [DataSet]执行指定的存储过程返回DataSet对象public DataSet RunProcReturn(string ProcName)
        /// <summary>
        /// 执行指定的存储过程返回DataSet对象
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <param name="tbName">DataSet中的表名</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet RunProcReturn(string ProcName)
        {
            SqlDataAdapter dap = CreateDataAdapter(ProcName, null, 1);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            dap.Dispose();
            this.Close();
            return ds;
        }

        #endregion

        #region [DataSet]执行存储过程或查询命令文本，并且返回DataSet数据集public DataSet RunProcReturn(string ProcName, Int32 Size)
        /// <summary>
        /// 执行存储过程或查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="ProcName">存储过程的名称或命令文本</param>
        /// <param name="Size">值0表示为命令文本,值为非0表示存储过程的识别参数</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet RunProcReturn(string ProcName, Int32 Size)
        {
            SqlDataAdapter dap = CreateDataAdapter(ProcName, null, Size);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            dap.Dispose();
            this.Close();
            return ds;
        }

        #endregion

        #region [DataSet]执行存储过程或查询命令文本，并且返回DataSet数据集public DataSet RunProcReturn(string ProcName, SqlParameter[] prams, string tbName, Int32 Size)
        /// <summary>
        /// 执行存储过程或查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="ProcName">存储过程的名称或命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="tbName">数据表名称</param>
        /// <param name="Size">值0表示为命令文本,值为非0表示存储过程的识别参数</param>
        /// <returns></returns>
        public DataSet RunProcReturn(string ProcName, SqlParameter[] prams, string tbName, Int32 Size)
        {
            SqlDataAdapter dap = CreateDataAdapter(ProcName, prams, Size);
            DataSet ds = new DataSet();
            dap.Fill(ds, tbName);
            dap.Dispose();
            this.Close();
            //得到执行成功返回值
            return ds;
        }

        #endregion

        #region [DataSet]执行存储过程或查询命令文本，并且返回DataSet数据集public DataSet RunProcReturn(string ProcName, SqlParameter[] prams, Int32 Size)
        /// <summary>
        /// 执行存储过程或查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="ProcName">存储过程的名称或命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="Size">值0表示为命令文本,值为非0表示存储过程的识别参数</param>
        /// <returns></returns>
        public DataSet RunProcReturn(string ProcName, SqlParameter[] prams, Int32 Size)
        {
            SqlDataAdapter dap = CreateDataAdapter(ProcName, prams, Size);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            dap.Dispose();
            this.Close();
            //得到执行成功返回值
            return ds;
        }
        /// <summary>
        /// 执行存储过程或查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="ProcName">存储过程的名称或命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="Size">值0表示为命令文本,值为非0表示存储过程的识别参数</param>
        /// <returns></returns>
        public DataSet RunProcReturn(string ProcName, SqlParameter[] prams, SelectCommand selectCommand)
        {
            return this.RunProcReturn(ProcName, prams, (int)selectCommand);
        }

        #endregion

        #region [SqlDataAdapter]创建一个SqlDataAdapter对象以此来执行存储过程或命令文本private SqlDataAdapter CreateDataAdapter(string ProcName, SqlParameter[] prams, Int32 Size)
        /// <summary>
        /// 创建一个SqlDataAdapter对象以此来执行存储过程或命令文本
        /// </summary>
        /// <param name="ProcName">存储过程或命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="Size">值0表示为命令文本,值为非0表示存储过程的识别参数</param>
        /// <returns>返回SqlDataAdapter对象</returns>
        private SqlDataAdapter CreateDataAdapter(string ProcName, SqlParameter[] prams, Int32 Size)
        {
            this.Open();//确认链接打开

            SqlDataAdapter dap = new SqlDataAdapter(ProcName, con);
            if (Size == 0)
                dap.SelectCommand.CommandType = CommandType.Text;//执行类型：命令文本
            else
                dap.SelectCommand.CommandType = CommandType.StoredProcedure;//执行类型：存储过程
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    dap.SelectCommand.Parameters.Add(parameter);
            }
            //加入返回参数
            dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return dap;
        }

        #endregion



    }
    /// <summary>
    /// 数据库执行类型枚举
    /// </summary>
    public enum SelectCommand
    {
        /// <summary>
        /// 执行文本命令
        /// </summary>
        Text = 0,
        /// <summary>
        /// 执行存储过程
        /// </summary>
        StoredProcedure = 1
    }

}
