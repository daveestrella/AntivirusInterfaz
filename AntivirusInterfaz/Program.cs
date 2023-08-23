using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntivirusInterfaz
{
    class Program
    {
        static void Main(string[] args)
        {
            int resp;
            List<string>[] Archivos = null; //Arreglo (del tamaño del número de archivos) de listas(con elementos igual al número de registros)
            List<string>[] aux; //Arreglo auxiliar que copia los valores de "Archivos" para no modificar el arreglo original
            string[] numregistros; //Arreglo con los numeros de registros de cada archivo
            int[,] respuesta; //Arreglo bidimensional con las respuestas que se presentan por consola
            int indice; //Entero que almacena el indice del archivo
            int pos;
            string virus;
            int registros;
            int i, j, k, r; //Contadores de bucles (r es un contador exclusivo para el arreglo "respuesta")

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("             ████       ████    ██   ██████████   ████████   ██        ██   ████████   ███████   ██    ██   ████████");
                Console.WriteLine("            ██  ██      ██ ██   ██       ██          ██       ██      ██       ██      ██    ██  ██    ██   ██");
                Console.WriteLine("           ██ ██ ██     ██  ██  ██       ██          ██        ██    ██        ██      ██   ██   ██    ██   ████████");
                Console.WriteLine("          ██      ██    ██   ██ ██       ██          ██         ██  ██         ██      ██  ██    ██    ██         ██");
                Console.WriteLine("         ██        ██   ██    ████       ██       ████████       ████       ████████   ██   ██   ████████   ████████\n\n\n");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("1. Ingresar Datos");
                Console.WriteLine("2. Ver Gráfico");
                Console.WriteLine("3. Modificar archivo");
                Console.WriteLine("4. Plantar Virus");
                Console.WriteLine("5. Salir\n\n");

                Console.Write("Que desea hacer?: ");
            
                resp = int.Parse(Console.ReadLine());

                switch (resp)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Número de Archivos: ");

                            Archivos = new List<string>[int.Parse(Console.ReadLine())];

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Número de Registros de cada archivo: ");

                            numregistros = Console.ReadLine().Split(' ');

                            for (i = 0; i < Archivos.Length; i++) //Se repite mientras sea menor al numero de archivos ingresado
                            {
                                Archivos[i] = new List<string>();
                                for (j = 0; j < int.Parse(numregistros[i]); j++) //Se llena la lista de cadenas vacias mientras sea menor al numero de registros ingresado
                                {
                                    Archivos[i].Add("");
                                }
                            }

                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            if (Archivos == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opcion no disponible todavia (Ingrese datos primero)");
                            }
                            else
                                Dibujar(Archivos);

                            Console.ReadKey();

                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            if (Archivos == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opcion no disponible todavia (Ingrese datos primero)");
                            }
                            else
                            {
                                Console.Write("Ingrese la posicion del archivo que desea modificar: ");
                                pos = int.Parse(Console.ReadLine()) - 1;

                                Console.Write("Cuántos registros: ");
                                registros = int.Parse(Console.ReadLine());

                                Archivos[pos] = new List<string>();

                                for (j = 0; j < registros; j++)
                                    Archivos[pos].Add("");
                            }

                            Console.ReadKey();

                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            if (Archivos == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opcion no disponible todavia (Ingrese datos primero)");
                            }
                            else
                            {

                                r = 0;
                                respuesta = new int[1, 2];

                                Console.Write("Ingrese la posicion inicial: ");
                                pos = int.Parse(Console.ReadLine()) - 1;

                                Console.Write("Ingrese el tipo de virus (A o B): ");
                                virus = Console.ReadLine().ToLower();


                                aux = new List<string>[Archivos.Length];
                                Clonar(Archivos, aux);

                                if (virus == "a") //Si el virus es tipo A
                                {

                                    for (j = 0; j < aux[pos].Count; j++) //Se repite mientras sea menor al numero de registros 
                                    {
                                        try
                                        {

                                            for (k = 0; k <= j; k++)
                                                if (aux[pos - k][j - k] != null && aux[pos + k][j - k] != null) //Comprueba que hay como expandir el triangulo por ambos lados
                                                {
                                                }

                                            for (k = 0; k <= j; k++) //Se marca los registros como infectados
                                            {
                                                aux[pos - k][j - k] = "infectado";
                                                aux[pos + k][j - k] = "infectado";
                                            }

                                        }

                                        catch (Exception e)
                                        {

                                        }
                                    }

                                    //Se almacena en la matriz de respuesta el maximo de registros infectados y el numero de registros no infectados
                                    respuesta[r, 0] = MaximoRegistros(aux);
                                    respuesta[r, 1] = RegistrosNoInfectados(aux);
                                    r++;

                                }

                                else //Si el virus es tipo B
                                {

                                    for (j = 0; j < aux[pos].Count; j++)  //Se repite mientras sea menor al numero de registros
                                    {
                                        try
                                        {

                                            for (k = 0; k <= j; k++) //Comprueba que hay como expandir el triangulo por la derecha (En el virus B no importa que este incompleto por la izquierda)
                                                if (aux[pos + k][j - k] != null)
                                                {
                                                }

                                            for (k = 0; k <= j; k++) //Se marca los registros como infectados
                                            {
                                                aux[pos + k][j - k] = "infectado";
                                                aux[pos - k][j - k] = "infectado";
                                            }

                                        }

                                        catch (Exception e)
                                        {

                                        }
                                    }

                                    //Se almacena en la matriz de respuesta el maximo de registros infectados y el numero de registros no infectados
                                    respuesta[r, 0] = MaximoRegistros(aux);
                                    respuesta[r, 1] = RegistrosNoInfectados(aux);
                                    r++;
                                }

                                Dibujar(aux);

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Máximo de registos infectados: {0}", respuesta[0, 0]);
                                Console.WriteLine("Registros no infectados: {0}", respuesta[0, 1]);
                            }

                            Console.ReadKey();
                            break;
                        }
                }

            } while (resp != 5);
        }

        public static void Clonar(List<string>[] fuente, List<string>[] destino)
        {
            int i, j;

            for (i = 0; i < fuente.Length; i++)
            {
                destino[i] = new List<string>();
                for (j = 0; j < fuente[i].Count; j++)
                    destino[i].Add(fuente[i][j]);
            }
        } //Funcion que copia lo elementos de un arreglo de listas a otro

        public static void Dibujar(List<string>[] datos) //Dibuja los archivos y sus registros (si estan infectados los pinta de otro color)
        {
            int i, j;

            Console.WriteLine("\n\n");
            for (i = 0; i < datos.Length; i++)
            {
                for (j = 0; j < datos[i].Count; j++)
                {
                    if (datos[i][j] == "")
                        Console.ForegroundColor = ConsoleColor.White;
                    else if(datos[i][j]=="infectado")
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write("██");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n\n");

        }
        public static void Imprimir(int[,] arr)
        {
            int i, j;

            for (i = 0; i < arr.GetLength(0); i++)
            {
                for (j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, 0] == 0)
                        break;
                    Console.Write(arr[i, j] + "  ");
                }


                Console.WriteLine();
            }
        } //Imprime un arreglo bidimensional

        public static int MaximoRegistros(List<string>[] arr)
        {
            int i, j;
            int maximo = 0;

            for (i = 0; i < arr.Length; i++)
                for (j = 0; j < arr[i].Count; j++)
                    if (arr[i][j] == "infectado")
                    {
                        if (j > maximo)
                            maximo = j;
                    }

            return (maximo + 1);
        } //Calcula el numero maximo de registros que se han infectado

        public static int RegistrosNoInfectados(List<string>[] arr)
        {
            int i, j;
            int total = 0;
            int numinfectados = 0;

            for (i = 0; i < arr.Length; i++)
                for (j = 0; j < arr[i].Count; j++)
                {
                    if (arr[i][0] == "infectado")
                    {
                        total++;

                        if (arr[i][j] == "infectado")
                            numinfectados++;
                    }

                    else
                        break;
                }


            return (total - numinfectados);
        } //Calcula el numero de registros que no han sido infectados
    }
}
