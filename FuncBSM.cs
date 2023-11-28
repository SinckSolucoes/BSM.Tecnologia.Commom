using Benner.Tecnologia.Common;
using Benner.Tecnologia.Common.Security;
using Benner.Tecnologia.Metadata;
using Benner.Tecnologia.Business;
using BSM.Tecnologia.Commom.Parametros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Collections;
using Benner.Tecnologia.Metadata.Entities;

namespace BSM.Tecnologia.Commom
{
    public static class FuncBSM
    {
        ///////----------- > VALIDAÇÕES <-------------------//////

        /// <summary>
        /// Valida uma data através de uma string
        /// </summary>
        /// <param name="data">String de data no formato dd/mm/yyyy</param>
        /// <returns>Retorna o status false se possui alguma inconsistência ou true se todos as informações necessárias foram informadas</returns>
        public static bool ValidarData(string data)
        {
            Regex r = new Regex(@"(\d{2}\/\d{2}\/\d{4})");
            return r.Match(data).Success;
        }

        /// <summary>
        /// Valida uma string como CNPJ
        /// </summary>
        /// <param name="cnpj">string representando os digitos do CNPJ</param>
        /// <returns></returns>
        public static bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Valida uma string como CPF
        /// </summary>
        /// <param name="cpf">string representando os digitos do CPF</param>
        /// <returns></returns>
        public static bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        ///////----------- > CONVERSÕES <-------------------//////

        /// <summary>
        /// Realiza criptografia em MD5
        /// </summary>
        /// <param name="input">Informação que será criptografada</param>
        /// <returns>Retorna a informação criptografada em MD5</returns>
        public static string GerarMD5(string input)
        {

            // Cria uma nova intância do objeto que implementa o algoritmo para criptografia MD5
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Criptografa a informação passada
            byte[] infoCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Cria um StringBuilder para passarmos os bytes gerados
            StringBuilder strBuilder = new StringBuilder();

            // Converte cada byte em um valor hexadecimal e adiciona ao string builder e formata em um hexadecimal string.
            for (int i = 0; i < infoCriptografado.Length; i++)
            {
                strBuilder.Append(infoCriptografado[i].ToString("x2"));
            }

            // Retorna o valor criptografado como string
            return strBuilder.ToString();

        }

        /// <summary>
        /// Converte uma Lista de Classes em um DataTable.
        /// </summary>
        /// <typeparam name="T">Tipo da Classe</typeparam>
        /// <param name="items">Objeto do tipo List<T></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// Método para converter uma informação em DateTime
        /// </summary>
        /// <typeparam name="value">Dado que será convertido</typeparam>
        /// <returns>Retorna o valor convertido em DateTime</returns>  
        public static DateTime ToDateTime(this object value)
        {
            if (value != null && value != DBNull.Value) { return Convert.ToDateTime(value); }
            else { return default(DateTime); }
        }

        /// <summary>
        /// Método para converter uma informação em double
        /// </summary>
        /// <typeparam name="value">Dado que será convertido</typeparam>
        /// <returns>Retorna o valor convertido em double</returns>  
        public static Double ToDouble(this object value)
        {
            if (value != null && value != DBNull.Value) { return Convert.ToDouble(value); }
            else { return default(Double); }
        }

        /// <summary>
        /// Método para converter um DataTable em Objeto do tipo models
        /// </summary>
        /// <typeparam name="T">Modelo da classe que será convertido</typeparam>
        /// <param name="dt">DataTable que será convertido</param>
        /// <returns>Retorna um models </returns>   
        public static List<T> ToList<T>(DataTable dt)
        {

            // Cria as variáveis
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();

            // Realiza a conversão
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            if (row[pro.Name].ToString().Equals("")) { pro.SetValue(objT, null, null); }
                            else
                            {
                                if (row[pro.Name].GetType().Equals(typeof(int))) { pro.SetValue(objT, row[pro.Name].ToLong(), null); }
                                else { pro.SetValue(objT, row[pro.Name], null); }
                            } // Finaliza if
                        } // Finaliza try
                        catch { throw; }
                    } // Finaliza if
                } // Finaliza foreach
                return objT;
            }).ToList();

        }

        /// <summary>
        /// Método para converter uma informação em Long
        /// </summary>
        /// <typeparam name="value">Dado que será convertido</typeparam>
        /// <returns>Retorna o valor convertido em long</returns>                
        private static long ToLong(this object value)
        {
            if (value != null && value != DBNull.Value) { return Convert.ToInt64(value); }
            else { return default(long); }
        }

        ///////----------- > E-MAIL <-------------------//////

        /// <summary>
        /// Envia um e-mail com uma mensagem padrão pré-definida
        /// </summary>
        /// <param name="destinatario">Quem irá receber o e-mail</param>
        /// <param name="assunto">Assunto do e-mail</param>
        /// <param name="conteudo">Corpo do e-mail</param>
        /// <returns></returns>
        public static bool EnviarEmail(Handle destinatario, string assunto, string conteudo)
        {
            Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios destino = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(destinatario.Value);

            if (null == destino.Email || destino.Email == "" || null == destino.Nome || destino.Nome == "")
                throw new ArgumentException("Cadastro do usuario incompleto. Verifique os campos 'Servidor' e 'Porta' na aba Avançado.");

            string titulo = assunto;

            string estlioPadrao = "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:107%;font-size:15px;font-family:\"Verdana\",sans-serif;'><span style='font-family:\"Verdana\",sans-serif;'>";
            string fechaEstiloPadrao = "</span></p>";

            DateTime agora = DateTime.Now;
            DateTime meiodia = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);
            DateTime seisDaNoite = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 18, 0, 0);
            DateTime seisDaManha = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 6, 0, 0);

            StringBuilder str = new StringBuilder();

            try
            {
                str.AppendLine(estlioPadrao);

                if (DateTime.Compare(agora, meiodia) < 0 && DateTime.Compare(seisDaManha, agora) < 0)
                {
                    str.AppendLine($"Bom dia,<br/>");
                }
                else if (DateTime.Compare(meiodia, agora) <= 0 && DateTime.Compare(agora, seisDaNoite) < 0)
                {
                    str.AppendLine($"Boa tarde,<br/>");
                }
                else
                {
                    str.AppendLine($"Boa noite,<br/>");
                }

                str.AppendLine(fechaEstiloPadrao);

                str.AppendLine(estlioPadrao);
                str.AppendLine("<p>");
                str.AppendLine(conteudo);
                str.AppendLine("</p>");

                string body = str.ToString();

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = destino.Email;
                mailMessage.Subject = titulo;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message.ToString());
            }
        }

        /// <summary>
        /// Envia um e-mail com o corpo do e-mail sendo um template de um arquivo .html
        /// </summary>
        /// <param name="destinatario">Quem irá receber o e-mail<</param>
        /// <param name="assunto">Assunto do e-mail</param>
        /// <param name="caminhoHtml">Caminho do arquivo HTML com o template do e-mail</param>
        /// <param name="substituir">Dicionario de chaves para substituir por variaveis do template HTML</param>
        /// <returns></returns>
        public static bool EnviarEmailTemplate(Handle destinatario, string assunto, string caminhoHtml, Dictionary<string, string> substituir)
        {
            Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios destino = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(destinatario.Value);

            if (null == destino.Email || destino.Email == "" || null == destino.Nome || destino.Nome == "")
                throw new ArgumentException("Cadastro do usuario incompleto. Verifique os campos 'Servidor' e 'Porta' na aba Avançado.");

            string titulo = assunto;

            try
            {
                string html = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + caminhoHtml);

                foreach (KeyValuePair<string, string> valores in substituir)
                {
                    html = SubstituirUltimaOcorrencia(html, valores.Key, valores.Value);
                }

                string body = html.ToString();

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = destino.Email;
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao enviar e-mail: " + e.Message.ToString());
            }
        }

        public static bool EnviarEmailTemplateCaminhoCompleto(Handle destinatario, string assunto, string caminhoHtml, Dictionary<string, string> substituir)
        {
            Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios destino = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(destinatario.Value);

            if (null == destino.Email || destino.Email == "" || null == destino.Nome || destino.Nome == "")
                throw new ArgumentException("Cadastro do usuario incompleto. Verifique os campos 'Servidor' e 'Porta' na aba Avançado.");

            string titulo = assunto;

            try
            {
                string html = File.ReadAllText(caminhoHtml);

                foreach (KeyValuePair<string, string> valores in substituir)
                {
                    html = SubstituirUltimaOcorrencia(html, valores.Key, valores.Value);
                }

                string body = html.ToString();

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = destino.Email;
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao enviar e-mail: " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Envia um e-mail com o corpo do e-mail sendo um template de um arquivo .html
        /// </summary>
        /// <param name="destinatario">Quem irá receber o e-mail<</param>
        /// <param name="assunto">Assunto do e-mail</param>
        /// <param name="caminhoHtml">Caminho do arquivo HTML com o template do e-mail</param>
        /// <param name="substituir">Dicionario de chaves para substituir por variaveis do template HTML</param>
        /// <returns></returns>
        public static bool EnviarEmailTemplate(Handle destinatario, string assunto, string diretorioBase, string caminhoHtml, Dictionary<string, string> substituir)
        {
            Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios destino = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(destinatario.Value);

            if (null == destino.Email || destino.Email == "" || null == destino.Nome || destino.Nome == "")
                throw new ArgumentException("Cadastro do usuario incompleto. Verifique os campos 'Servidor' e 'Porta' na aba Avançado.");

            string titulo = assunto;

            try
            {
                string html = File.ReadAllText(diretorioBase + caminhoHtml);

                foreach (KeyValuePair<string, string> valores in substituir)
                {
                    html = SubstituirUltimaOcorrencia(html, valores.Key, valores.Value);
                }

                string body = html.ToString();

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = destino.Email;
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao enviar e-mail: " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Envia um e-mail com o corpo do e-mail sendo um template de um arquivo .html
        /// </summary>
        /// <param name="emailDestinatario">Quem irá receber o e-mail<</param>
        /// <param name="assunto">Assunto do e-mail</param>
        /// <param name="caminhoHtml">Caminho do arquivo HTML com o template do e-mail</param>
        /// <param name="substituir">Dicionario de chaves para substituir por variaveis do template HTML</param>
        /// <returns></returns>
        public static bool EnviarEmailTemplate(string emailDestinatario, string assunto, string caminhoHtml, Dictionary<string, string> substituir)
        {
            try
            {
                string html = File.ReadAllText(caminhoHtml);

                foreach (KeyValuePair<string, string> valores in substituir)
                {
                    html = SubstituirUltimaOcorrencia(html, valores.Key, valores.Value);
                }

                string body = html.ToString();

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = emailDestinatario;
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao enviar e-mail: " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Envia um e-mail com o corpo do e-mail sendo um template de um arquivo .html
        /// </summary>
        /// <param name="destinatario">Quem irá receber o e-mail<</param>
        /// <param name="assunto">Assunto do e-mail</param>
        /// <param name="caminhoHtml">Caminho do arquivo HTML com o template do e-mail</param>
        /// <param name="substituir">Dicionario de chaves para substituir por variaveis do template HTML</param>
        /// <returns></returns>
        public static bool EnviarEmailTemplate(Handle remetente, Handle destinatario, string assunto, string caminhoHTML, Dictionary<string, string> substituir)
        {
            try
            {
                Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios de = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(remetente.Value);
                Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios para = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(destinatario.Value);

                var fromAddress = new MailAddress(de.SMTPUserAuth);
                var toAddress = new MailAddress(para.Email);

                string html = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + caminhoHTML);

                foreach (KeyValuePair<string, string> valores in substituir)
                {
                    html = SubstituirUltimaOcorrencia(html, valores.Key, valores.Value);
                }

                string body = html.ToString();

                //var smtp = new SmtpClient
                //{
                //    Host = de.SMTPServer,
                //    Port = (int)de.SMTPPort,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(de.SMTPUserAuth, de.SMTPPassword)
                //};

                //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddress, toAddress);
                //message.Subject = assunto;
                //message.Body = body;
                //message.BodyEncoding = System.Text.Encoding.UTF8;
                //message.IsBodyHtml = true;
                //message.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;

                //smtp.Send(message);

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = para.Email;
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao enviar e-mail: " + e.Message.ToString());
            }
        }

        public static bool EnviarEmailTemplate(string handleRemetente, string emailDestinatario, string assunto, string caminhoHTML, Dictionary<string, string> substituir)
        {
            try
            {
                Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios de = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(new Handle(handleRemetente));

                var fromAddress = new MailAddress(de.SMTPUserAuth);
                var toAddress = new MailAddress(emailDestinatario);

                string html = File.ReadAllText(caminhoHTML);

                foreach (KeyValuePair<string, string> valores in substituir)
                {
                    html = SubstituirUltimaOcorrencia(html, valores.Key, valores.Value);
                }

                string body = html.ToString();

                //var smtp = new SmtpClient
                //{
                //    Host = de.SMTPServer,
                //    Port = (int)de.SMTPPort,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(de.SMTPUserAuth, Scramble.GetScrambled(de.SMTPPassword))
                //};

                //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddress, toAddress);
                //message.Subject = assunto;
                //message.Body = body;
                //message.BodyEncoding = System.Text.Encoding.UTF8;
                //message.IsBodyHtml = true;
                //message.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;

                //smtp.Send(message);

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = emailDestinatario;
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao enviar e-mail: " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Envia um e-mail com o corpo do e-mail sendo um template de um arquivo .html
        /// </summary>
        /// <param name="destinatario">Quem irá receber o e-mail<</param>
        /// <param name="assunto">Assunto do e-mail</param>
        /// <param name="caminhoHtml">Caminho do arquivo HTML com o template do e-mail</param>
        /// <param name="substituir">Dicionario de chaves para substituir por variaveis do template HTML</param>
        /// <returns></returns>
        public static bool EnviarEmailTemplate(Handle remetente, Handle destinatario, string assunto, string diretorioBase, string caminhoHTML, Dictionary<string, string> substituir)
        {
            try
            {
                Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios de = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(remetente.Value);
                Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios para = Benner.Tecnologia.Metadata.Entities.ZGrupoUsuarios.GetFirstOrDefault(destinatario.Value);

                var fromAddress = new MailAddress(de.SMTPUserAuth);
                var toAddress = new MailAddress(para.Email);

                string html = File.ReadAllText(diretorioBase + caminhoHTML);

                foreach (KeyValuePair<string, string> valores in substituir)
                {
                    html = SubstituirUltimaOcorrencia(html, valores.Key, valores.Value);
                }

                string body = html.ToString();

                //var smtp = new SmtpClient
                //{
                //    Host = de.SMTPServer,
                //    Port = (int)de.SMTPPort,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(de.SMTPUserAuth, de.SMTPPassword)
                //};

                //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddress, toAddress);
                //message.Subject = assunto;
                //message.Body = body;
                //message.BodyEncoding = System.Text.Encoding.UTF8;
                //message.IsBodyHtml = true;
                //message.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;

                //smtp.Send(message);

                Benner.Tecnologia.Business.MailMessage mailMessage = new Benner.Tecnologia.Business.MailMessage();
                mailMessage.SendTo = para.Email;
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Send();

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao enviar e-mail: " + e.Message.ToString());
            }
        }

        public static string GetEmailTemplate(string diretorioBase, string caminhoHTML, Dictionary<string, string> substituir)
        {
            try
            {
                string text = File.ReadAllText(diretorioBase + caminhoHTML);
                foreach (KeyValuePair<string, string> item in substituir)
                {
                    text = SubstituirUltimaOcorrencia(text, item.Key, item.Value);
                }

                string body = text.ToString();
                return body;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao obter conteúdo do e-mail. " + ex.Message.ToString());
            }
        }
        ///////----------- > GERAL <-------------------//////

        /// <summary>
        /// Método para comparar uma informação string x criptografada MD5
        /// </summary>
        /// <param name="input">Informação sem criptografia</param>
        /// <param name="hash">Informação criptografada</param>
        /// <returns>Retorna a informação criptografada em MD5</returns>
        static bool ComparaMD5(string input, string hash)
        {
            // Chama o método para criptografar a informação
            string hashOfInput = GerarMD5(input);

            // Cria um StringComparer para comparar as hashes
            StringComparer Comparar = StringComparer.OrdinalIgnoreCase;

            // Retorna o status da validação da comparação
            return 0 == Comparar.Compare(hashOfInput, hash) ? true : false;

        }

        /// <summary>
        /// Método para converter Base64 em string
        /// </summary>
        /// <param name="dados">Informação que será convertida</param>
        /// <returns></returns>
        public static string ConverterBase64ToStringUTF8(string dados)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(dados));
        }

        /// <summary>
        /// Retorna a diferença em dias entre duas datas
        /// </summary>
        /// <param name="charInterval">String que recebe o "d" para comparar por dia, "m" por mês ou "y" por ano</param>
        /// <param name="dttFromDate">DateTime da data inicial</param>
        /// <param name="dttToDate">DateTime da data final</param>
        /// <returns>Retorna o valor de um periodo entre duas datas. Pode ser por ano, mês ou dia</returns>
        public static long DateDiff(string charInterval, DateTime dttFromDate, DateTime dttToDate)
        {
            TimeSpan tsDuration;
            tsDuration = dttToDate - dttFromDate;

            if (charInterval == "d")
            {
                // Resultado em Dias
                return tsDuration.Days;
            }
            else if (charInterval == "m")
            {
                // Resultado em Meses
                double dblValue = 12 * (dttFromDate.Year - dttToDate.Year) + dttFromDate.Month - dttToDate.Month;
                return Convert.ToInt32(Math.Abs(dblValue));
            }
            else if (charInterval == "y")
            {
                // Resultado em Anos
                return Convert.ToInt32((tsDuration.Days) / 365);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para remover os acentos
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RetirarAcentos(string texto)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = texto.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letra in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letra) != UnicodeCategory.NonSpacingMark)
                { sbReturn.Append(letra); }
            }

            return sbReturn.ToString();
        }

        /// <summary>
        /// Verifica se string possui apenas número
        /// </summary>
        /// <returns>Retorna true se houver somente números caso contrário irá retornar false</returns>
        public static bool SomenteNumero(string texto)
        {
            if (texto.Where(c => char.IsLetter(c)).Count() > 0) { return false; }
            else { return true; }
        }

        /// <summary>
        /// Substitui a ocorrencia de uma string por outra string
        /// </summary>
        /// <param name="Source">Texto completo</param>
        /// <param name="Find">String que se deseja remover</param>
        /// <param name="Replace">String que se deseja inserir</param>
        /// <returns></returns>
        public static string SubstituirUltimaOcorrencia(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }

        /// <summary>
        /// Duplica um numero de telefone na GN_PESSOATELEFONES conforme um outro tipo
        /// </summary>
        /// <param name="tipoADuplicar">Novo cadastro a ser incluido conforme o tipo de telefone desejado</param>
        /// <param name="tipoReferencia">Tipo de telefone para ser usado como referencia na hora de duplicar</param>
        public static void DuplicarNumeroTelefone(long tipoADuplicar, long tipoReferencia)
        {
            Query query = new Query("SELECT * FROM GN_PESSOATELEFONES WHERE DDD IS NOT NULL AND TIPO=" + tipoReferencia + " AND TELEFONE NOT IN (SELECT A.TELEFONE FROM (SELECT TELEFONE, COUNT(*) QTD FROM GN_PESSOATELEFONES WHERE PESSOA IN (SELECT PESSOA FROM GN_PESSOATELEFONES WHERE TIPO=" + tipoReferencia + ") AND TIPO IN(" + tipoReferencia + "," + tipoADuplicar + ") GROUP BY TELEFONE HAVING COUNT(*)>1) A)");
            var telefones = query.Execute();

            if (telefones.Count == 0)
                return;
            try
            {
                foreach (EntityBase numero in telefones)
                {
                    try
                    {
                        using (TransactionContext tc = new TransactionContext())
                        {
                            RawEntityCommand entityCommand = new RawEntityCommand();
                            entityCommand.CommandText = "INSERT INTO GN_PESSOATELEFONES (HANDLE, DDD, PESSOA, TELEFONE, TIPO) VALUES (:HANDLE, :DDD, :PESSOA, :TELEFONE, :TIPO)";
                            Query query2 = new Query("SELECT MAX(HANDLE) HANDLE FROM GN_PESSOATELEFONES");
                            var ultimoTelefone = query2.Execute();

                            foreach (EntityBase num in ultimoTelefone)
                            {
                                int han = Convert.ToInt32(num.Fields["HANDLE"].ToString()) + 1;
                                entityCommand.Parameters.Add(new Benner.Tecnologia.Common.Parameter("HANDLE", han.ToString()));
                                break;
                            }

                            entityCommand.Parameters.Add(new Benner.Tecnologia.Common.Parameter("DDD", numero.Fields["DDD"].ToString()));
                            entityCommand.Parameters.Add(new Benner.Tecnologia.Common.Parameter("PESSOA", numero.Fields["PESSOA"].ToString()));
                            entityCommand.Parameters.Add(new Benner.Tecnologia.Common.Parameter("TELEFONE", numero.Fields["TELEFONE"].ToString()));
                            entityCommand.Parameters.Add(new Benner.Tecnologia.Common.Parameter("TIPO", tipoADuplicar));

                            Entity.Execute(entityCommand);
                            tc.Complete();
                        }
                    }
                    catch { throw; }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro: " + ex.Message);
            }
        }

        /// <summary>
        /// Bom dia, Boa tarde e Boa noite conforme o horario atual.
        /// </summary>
        /// <returns>Se horario atual estiver entre:<para> 06:00 - 12:00 (Bom dia) </para><para> 12:00 - 18:00 (Boa tarde) </para><para> 18:00 - 06:00 (Boa noite) </para></returns>
        public static string CumprimentoConformeHorarioAtual()
        {
            if (DateTime.Compare(DateTime.Now, new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0)) < 0 && DateTime.Compare(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 6, 0, 0), DateTime.Now) < 0)
                return "Bom dia";
            else if (DateTime.Compare(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0), DateTime.Now) <= 0 && DateTime.Compare(DateTime.Now, new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 18, 0, 0)) < 0)
                return "Boa tarde";

            return "Boa Noite";
            
        }

        ///////----------- > Buscar Arquivo <-------------------//////

        /// <summary>
        /// Busca todas as imagens em um diretorio e retorna uma lista em base64, por padrão exclui a imagem após leitura
        /// </summary>
        /// <param name="caminho"></param>
        /// <param name="excluirImagemAposLeitura"></param>
        /// <returns></returns>
        public static Dictionary<string, FileField> BuscarImagens(string caminho, long maximoImagens = 50)
        {
            Dictionary<string, FileField> imagens = new Dictionary<string, FileField>();

            if (File.Exists(caminho))
                new Exception("Caminho Inexistente");

            try
            {
                DirectoryInfo diretorio = new DirectoryInfo(caminho);
                FileInfo[] arquivos = diretorio.GetFiles();

                int cont = 0;
                foreach (FileInfo a in arquivos)
                {
                    if (cont <= maximoImagens)
                    {
                        cont++;
                        string caminhoCompleto = Path.Combine(caminho, a.Name);
                        byte[] imagB = File.ReadAllBytes(caminhoCompleto);
                        //var stream = new FileStream(caminhoCompleto, FileMode.Open);
                        var mStream = new MemoryStream(imagB);
                        var arquivo = new FileField();
                        arquivo.Content = mStream;
                        arquivo.Name = a.Name;
                        //string img64 = Convert.ToBase64String(imagB);

                        imagens.Add(a.Name, arquivo);

                    }
                    else
                    {
                        break;
                    }

                }

            }
            catch (Exception e)
            {

            }

            return imagens;
        }

        public static string SafeSubstring(this string value, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (startIndex < 0 || length < 0)
                return value;
            return value.Length < length + startIndex ? value.Substring(startIndex, value.Length - startIndex) : value.Substring(startIndex, length);
        }

        ///////----------- > PARAMETROS < -------------//////

        public static string BuscaParametro(string parametro)
        {
            IParametrosDao param = ParametrosDao.CreateInstance();
            return param.BuscaParametro(parametro);
        }


        public static int ConverteHandleNumeroSemanaBenner(long handle)
        {
            switch (handle)
            {
                case 20: return 0;
                case 21: return 1;
                case 22: return 2;
                case 23: return 3;
                case 24: return 4;
                case 25: return 5;
                case 26: return 6;
            }

            return -1;
        }

        public static int ConverteNumeroSemanaHandleBenner(long handle)
        {
            switch (handle)
            {
                case 0: return 20;
                case 1: return 21;
                case 2: return 22;
                case 3: return 23;
                case 4: return 24;
                case 5: return 25;
                case 6: return 26;
            }

            return -1;
        }

        /// <summary>
        /// Recebe uma string pura e retorna a string formatada (com máscara de CPF ou CNPJ conforme a quantidade de caracteres da string)
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static string FormatarDocumentoCpfOuCNPJ(string documento)
        {
            if (string.IsNullOrEmpty(documento))
            {
                return string.Empty;
            }

            // Remover caracteres não numéricos
            string numero = new string(documento.Where(char.IsDigit).ToArray());

            if (numero.Length == 11)
            {
                // CPF: formatar como XXX.XXX.XXX-XX
                return $"{numero.Substring(0, 3)}.{numero.Substring(3, 3)}.{numero.Substring(6, 3)}-{numero.Substring(9)}";
            }
            else if (numero.Length == 14)
            {
                // CNPJ: formatar como XX.XXX.XXX/XXXX-XX
                return $"{numero.Substring(0, 2)}.{numero.Substring(2, 3)}.{numero.Substring(5, 3)}/{numero.Substring(8, 4)}-{numero.Substring(12)}";
            }
            else
            {
                // Documento inválido ou com tamanho incorreto
                return documento;
            }
        }

        public static string RemoverFormatacaoDocumentoCpfOuCNPJ(string documentoFormatado)
        {
            if (string.IsNullOrEmpty(documentoFormatado))
            {
                return string.Empty;
            }

            // Remover caracteres não numéricos
            string numero = new string(documentoFormatado.Where(char.IsDigit).ToArray());

            return numero;
        }


        ///////----------- > Interface < -------------//////

        public static string MontaWidgetTileHTML(string titulo, string valor, string cor = null, IconesWes? icone = null, TamanhoWidget tamanhoWidget = TamanhoWidget.Pequeno)
        {
            if (valor == null || valor == "")
                valor = "0";
            if (titulo == null || titulo == "")
                titulo = "Título";

            var sb = new StringBuilder();
            sb.Append("<div class=\"widget-body ");
            sb.Append(RetornaCssTamanhoWidget(tamanhoWidget));
            sb.Append($" widget portlet-none\" data-widget-id=\"K9_{titulo.Replace(" ", "").ToUpper()}\" style=\"\">");

            sb.AppendLine("     <div class=\"wes-tile\">");
            sb.AppendLine($"         <a data-tilecolor=\"green\" class=\"dashboard-stat dashboard-stat-v2 wes-default-tile tilecolor command-name ");

            if (cor != null && cor.Length > 0)
                sb.Append(cor);
            else
                sb.Append("green");
            sb.Append("\">");

            sb.AppendLine("             <div class=\"visual\">");
            sb.AppendLine("                 <i class=\"");

            if (icone != null)
                sb.Append(RetornaCssIcones(icone.Value));
            else
                sb.Append("fa fa-check");
            sb.Append("\"></i>");

            sb.AppendLine("             </div>");
            sb.AppendLine("             <div class=\"details\">");
            sb.AppendLine("                 <div class=\"number\">");
            sb.AppendLine("                     <span class=\"tilevalue\">");
            sb.AppendLine($"                        <span style=\"font-size: 2.3rem;\">{valor}</span>");
            sb.AppendLine("                     </span>");
            sb.AppendLine("                 </div>");
            sb.AppendLine($"                <div class=\"desc tiledescription\">{titulo}</div>");
            sb.AppendLine("             </div>");
            sb.AppendLine("         </a>");
            sb.AppendLine("     </div>");
            sb.AppendLine("</div>");

            return sb.ToString();
        }

        public static string RetornaCssTamanhoWidget(TamanhoWidget tamanhoWidget)
        {
            switch (tamanhoWidget)
            {
                case TamanhoWidget.Pequeno:
                    return "col-md-3";
                case TamanhoWidget.Medio:
                    return "col-md-6";
                case TamanhoWidget.Grande:
                    return "col-md-9";
                case TamanhoWidget.Maximo:
                    return "col-md-12";
                default:
                    return "col-md-3";
            }
        }

        public static string RetornaCssIcones(IconesWes icone)
        {
            switch (icone)
            {
                case IconesWes.fa_dollar:
                    return "fa fa-dollar";
                case IconesWes.fa_truck:
                    return "fa fa-truck";
                case IconesWes.fa_check:
                    return "fa fa-check";
                case IconesWes.fa_plus:
                    return "fa fa-plus";
                case IconesWes.fa_cube:
                    return "fa fa-cube";
                case IconesWes.fa_cubes:
                    return "fa fa-cubes";
                default:
                    return "fa fa-check";
            }
        }

        public enum TamanhoWidget
        {
            Pequeno,
            Medio,
            Grande,
            Maximo
        }

        public enum IconesWes
        {
            fa_dollar,
            fa_truck,
            fa_check,
            fa_plus,
            fa_cube,
            fa_cubes
        }
    }
}
