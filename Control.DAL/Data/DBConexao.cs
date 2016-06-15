using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Data
{
    public class DBConexao
    {
        public enum ConnectionProfile
        {
            Local = 0,
            Homolog = 1,
            Prod = 2
        }

        public static ConnectionProfile GetConnectionProfile()
        {            
            string urlSite = ConfigurationManager.AppSettings["URLSite"];

            if (urlSite.IndexOf("localhost") >= 0)
                return ConnectionProfile.Local;
            else if (urlSite.IndexOf("gtwave") >= 0 || urlSite.IndexOf("cnab") >= 0)
                return ConnectionProfile.Homolog;
            else
                return ConnectionProfile.Prod;
        }

        public static string GetConnectionString(bool blnPooling)
        {
            string strConn = "";
            int intConexao = int.Parse(ConfigurationManager.AppSettings["Conexao"]);

            switch (intConexao)
            {
                case 1:

                    //Timepix					
                    strConn = @"user id=sa; password=*talento2010; data source=192.168.2.111,1433; initial catalog=RPS; ";
                    break;

                case 2:                    
                    //Local PC
                    strConn = @"Server=.\SQLEXPRESS;Database=Control;Integrated Security=SSPI;";
                    break;

                case 3:

                    //Conex�o de produ��o Gtwave
                    //strConn = @"Data Source=192.99.149.206;Initial Catalog=control;Persist Security Info=True;User ID=userControl;Password=Ncc123";
                    strConn = @"Data Source=192.99.149.206;Initial Catalog=dbControl;Persist Security Info=True;User ID=userdbControl;Password=Ncc123";
                    //strConn = @"Data Source=192.99.149.206;Initial Catalog=dbControl;Persist Security Info=True;User ID=nfs_control;Password=vDy20v3#";
                    break;

                case 4:
                    //Local Gtwave
                    //Conex�o de homologa��o na Arisp (testes da migra��o em produ��o)
                    strConn = @"User ID=cepe;Password=123;Data Source=SERVER2012;Initial Catalog=Control;";
                    break;

                case 5:

                    //svn externo
                    strConn = @"user id=sa; password=*talento2010; data source=timepix.ddns.net; initial catalog=CPEDIDOS;";
                    break;

                case 6:

                    //Conex�o de produ��o na Arisp para debugar
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=200.169.101.76; initial catalog=CPEDIDOS;";
                    break;

                case 7:

                    //Conex�o de Natanael Silva - Desenvolvimento
                    strConn = @"Server=tcp:ckl86ymzyx.database.windows.net,1433;Database=NFS_control;User ID=meiabandeirada@ckl86ymzyx;Password=j0t@qu3st;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
                    break;
            }

            //Pooling
            //strConn += blnPooling == true ? @" Min Pool Size=20; Max Pool Size=300; Connect Timeout=300; Connection Reset=True;" : @" Pooling=false; Connect Timeout=45;";

            return strConn;
        }

        public static string GetConnectionString()
        {
            return GetConnectionString(true);
        }

        /// <summary>
        /// Retorna objeto de transa��o
        /// </summary>
        /// <param name="SQLTrans"></param>
        /// <returns></returns>
        public static SqlTransaction GetConnectionTrans()
        {

            SqlConnection sConn = new SqlConnection(GetConnectionString());
            sConn.Open();
            return sConn.BeginTransaction();

        }

        #region Metodos MySQl e Outras Conex�es
        public static string GetConnectionStringPenhora(bool blnPooling)
        {
            string strConn = "";
            int intConexao = int.Parse(ConfigurationManager.AppSettings["Conexao"]);

            switch (intConexao)
            {
                case 1:

                    //Timepix					
                    strConn = @"user id=sa; password=*talento2010; data source=192.168.2.111,1433; initial catalog=penhoraOnline; ";
                    break;

                case 2:

                    strConn = @"user id=sa; password=*talento2010; data source=192.168.2.111,1433; initial catalog=penhoraOnline2; ";
                    break;

                case 3:

                    //Conex�o de produ��o na Arisp
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=172.16.0.86; initial catalog=penhoraOnline;";
                    break;

                case 4:

                    //Conex�o de homologa��o na Arisp (testes da migra��o em produ��o)
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=172.16.0.86; initial catalog=penhoraOnline_prod2;";
                    break;

                case 5:

                    //svn externo
                    strConn = @"user id=sa; password=*talento2010; data source=timepix.ddns.net; initial catalog=penhoraOnline;";
                    break;

                case 6:

                    //Conex�o de produ��o na Arisp para debugar
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=200.169.101.76; initial catalog=penhoraOnline;";
                    break;

            }

            //Pooling
            strConn += blnPooling == true ? @" Connect Timeout=300; Connection Reset=True;" : @" Pooling=false; Connect Timeout=45;";

            return strConn;
        }


        public static string GetConnectionStringPenhora()
        {
            return GetConnectionStringPenhora(true);
        }


        public static string GetConnectionStringMensagem(bool blnPooling)
        {
            string strConn = "";
            int intConexao = int.Parse(ConfigurationManager.AppSettings["Conexao"]);

            switch (intConexao)
            {
                case 1:

                    //Timepix					
                    strConn = @"user id=sa; password=*talento2010; data source=192.168.2.111,1433; initial catalog=MENSAGEM_PRODUCAO; ";
                    break;

                case 2:

                    strConn = @"user id=sa; password=*talento2010; data source=192.168.2.222,1433; initial catalog=penhoraOnline2; ";
                    break;

                case 3:

                    //Conex�o de produ��o na Arisp
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=172.16.0.86; initial catalog=MENSAGEM_PRODUCAO;";
                    break;

                case 4:

                    //Conex�o de homologa��o na Arisp (testes da migra��o em produ��o)
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=172.16.0.86; initial catalog=MENSAGEM;";
                    break;

            }

            //Pooling
            strConn += blnPooling == true ? @" Connect Timeout=300; Connection Reset=True; " : @" Pooling=false; Connect Timeout=45;";

            return strConn;
        }


        public static string GetConnectionStringMensagem()
        {
            return GetConnectionStringMensagem(true);
        }

        /// <summary>
        /// Retorna string de conex�o para MySQL
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        /// <returns></returns>
        public static string GetConnectionStringMySQL(string Server, string UserId, string Password, string Database)
        {
            string strConn = "";
            string port = "";
            string[] arrServer = null;
            int intConexao = int.Parse(ConfigurationManager.AppSettings["Conexao"]);

            switch (intConexao)
            {
                case 1:
                case 2:
                case 3:

                    //Cria array para caso a porta tenha sido informada
                    arrServer = Server.Split(new Char[] { ':' });

                    //Se a porta foi informada
                    if (arrServer.Length > 0)
                    {
                        port = "port=" + arrServer[1] + ';';
                    }

                    strConn = @"server=" + arrServer[0] + "; " + port + " user id=" + UserId + "; password=" + Password + "; database=" + Database + ";";
                    break;

            }

            return strConn;
        }


        public static string GetConnectionStringDECDOI(bool blnPooling)
        {
            string strConn = "";
            int intConexao = int.Parse(ConfigurationManager.AppSettings["Conexao"]);

            switch (intConexao)
            {
                case 1:
                case 2:
                    //Timepix					
                    strConn = @"user id=sa; password=*talento2010; data source=192.168.2.111,1433; initial catalog=DEC_PROD; ";
                    break;

                case 3:
                case 4:
                    //Conex�o de produ��o na Arisp
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=172.16.0.86; initial catalog=DEC;";
                    break;

                case 6:

                    //Conex�o de produ��o na Arisp para debugar
                    strConn = @"user id=Cpedidos; password=*ar2011!; data source=200.169.101.76; initial catalog=DEC;";
                    break;

            }

            //Pooling
            strConn += blnPooling == true ? @" Min Pool Size=5; Max Pool Size=300; Connect Timeout=300; Connection Reset=True;" : @" Pooling=false; Connect Timeout=45;";

            return strConn;
        }

        #endregion
    }
}