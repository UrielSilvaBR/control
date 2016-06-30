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
    }
}
