using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Asincrono
{
    class Program
    {
        static void Main(string[] args)
        {
            CAssync pruebaAsincrona = new CAssync("Alex Geovany Soler");

            string resultado = pruebaAsincrona.EjecutarPrueba().Result;

            Console.WriteLine(resultado);
        }
    }

    public class CAssync
    {
        private double duracion;

        public string Nombre { get; set; }
        public double Duracion { get { return duracion; }}

        public CAssync(string pNombre)
        {
            Nombre = pNombre;
        }

        public async Task<string> EjecutarPrueba()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var tarea1 = Proceso1();
            var tarea2 = Proceso2(Nombre);
            await Task.WhenAll(tarea1, tarea2);
            stopWatch.Stop();

            duracion = stopWatch.ElapsedMilliseconds/1000;

            return string.Format("Resultado prueba 1: {0} \n"+
                                  "Resultado prueba 2: {1} \n"+
                                  "Duracion: {2}",
                                   tarea1.Result, tarea2.Result, duracion);
        }

        private async Task<int> Proceso1()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 40;
            });
        }

        private async Task<string> Proceso2(string nombre)
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);
                return $"Hola {nombre}";
            }).ContinueWith((saludo) =>
            {
                return $"Ejemplo de saludo: {saludo.Result}.";
            });
        }
    }
}