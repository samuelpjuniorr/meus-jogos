using System;
using System.IO;
using System.Text;

namespace MeusJogos.Infra.CrossCutting.IoC
{
    public class Global
    {
        #region Singleton

        //Variaveis Privadas
        private static volatile Global i_instance = null;
        private static readonly Global _global = new Global();

        /// <summary>
        /// Singleton
        /// </summary>
        public static Global Instance
        {
            get
            {
                if (i_instance == null)
                {
                    lock (_global)
                    {
                        if (i_instance == null)
                        {
                            i_instance = new Global();
                        }
                    }
                }
                return i_instance;
            }
        }

        //Inicialização Privada
        private Global()
        {
            ConnectionString = "";
        }

        private string ConnectionString;
        private string Secret;


        private readonly string PathLog = String.Format(@"{0}\\{1}\\", System.AppDomain.CurrentDomain.BaseDirectory, "Temp");


        public string GetConnectionString()
        {
            return ConnectionString;
        }

        public string GetSecret()
        {
            return Secret;
        }

        public void SetSecret(string str)
        {
            Secret = str;
        }

        public void SetConnectionString(string str)
        {
            ConnectionString = str;
        }



        #endregion

        #region Log



        public void GravarArquivo(String path, String conteudo, String nomeArquivo, Boolean append = false)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                StreamWriter l_SrLog = new StreamWriter(Path.Combine(path, nomeArquivo), append, Encoding.UTF8);
                l_SrLog.WriteLine(conteudo);
                l_SrLog.Flush();
                l_SrLog.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string LerArquivo(string path)
        {
            string arquivo = "";

            if (File.Exists(path))
            {

                using (StreamReader sr = new StreamReader(path))
                {
                    arquivo = sr.ReadToEnd();

                }
            }

            return arquivo;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="a_Metodo"></param>
        /// <param name="a_Log"></param>
        public void GravaLog(String a_Metodo, String a_Log)
        {
            try
            {
                Global.Instance.RegistraMensagem(a_Metodo, a_Log);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        /// <summary>
        /// Registra Mensagem TXT caso nao consiga registrar no banco
        /// </summary>
        /// <param name="a_Metodo"></param>
        /// <param name="a_Log"></param>
        public void RegistraMensagem(String a_Metodo, String a_Log)
        {
            try
            {
                if (!Directory.Exists(PathLog))
                {
                    Directory.CreateDirectory(PathLog);
                }
                StreamWriter l_SrLog = new StreamWriter(PathLog + "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt", true, Encoding.UTF8);
                l_SrLog.WriteLine(DateTime.Now.ToString("dd/MM/yy HH:mm") + " - " + a_Metodo + " " + a_Log + "\n");
                l_SrLog.Flush();
                l_SrLog.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}
