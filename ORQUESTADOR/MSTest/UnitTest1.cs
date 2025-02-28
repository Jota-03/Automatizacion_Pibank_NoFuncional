//15/10/2024
//VERSION 4.0
//AJUSTES CARGUE DE EVIDENCIAS

using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace Orquestador
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext? testContextInstance;

        // 1-LOCAL    2-REMOTO
        int? inicializar = 2;
        public string? CasoID = "1220091";

        public TestContext TestContext
        {
            get { return testContextInstance!; }
            set { testContextInstance = value; }
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            if (inicializar == 2)
            {
                // Extrae el ID del caso de prueba de las propiedades del TestContext
                CasoID = this.TestContext.Properties["__Tfs_TestCaseId__"]?.ToString() ?? "ValorPorDefecto";
            }
        }

        [TestMethod]
        public void Ejecucion()
        {
            string ruta = @"D:\AUTOMATIZACION_PIBANK_NO_FUNCIONAL\proyecto_jmeter\bin";
            string casoID = CasoID ?? "ValorPorDefecto";
            string rutaEvidencias = @"D:\AUTOMATIZACION_PIBANK_NO_FUNCIONAL\outputData\reporte" + casoID;
            Console.WriteLine($"Ruta de evidencias: {rutaEvidencias}");
            Program.crearArchivo(rutaEvidencias);
            Program.eliminarArchivo(ruta + @"\results001", rutaEvidencias);
            bool estado = Program.Ejecutar(@"jmeter -n -t TESTCASE\CasoPrueba" + casoID + @".jmx -l results001 -e -o " + rutaEvidencias, ruta);
            Assert.AreEqual(true, true);
            string rutaEvidencia = Program.Comprimir(rutaEvidencias);
            TestContext.AddResultFile(@rutaEvidencia);
        }
    }
}
