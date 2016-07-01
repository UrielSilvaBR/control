using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnvioEmail.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                Utilidades.EnvioEmail.EmailHelper.SendMail("urielbr@gmail.com", "teste envio Email", "Teste envio Email", AppDomain.CurrentDomain.BaseDirectory + "/anexos/arquivo.pdf");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                Utilidades.EnvioEmail.EmailHelper.SendMailTemplate("urielbr@gmail.com", "teste envio Email", "Boa Tarde.<br /><br /> Segue a proposta solicitada. <br /><br />Atenciosamente,", AppDomain.CurrentDomain.BaseDirectory + "/anexos/arquivo.pdf");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
