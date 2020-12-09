using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; //para pilas
using System.Threading; //para el manejo del tiempo entre indicacciones
using System.IO; //para el manejo de archivos.

//Fuentes de info:
//https://docs.microsoft.com/es-es/dotnet/csharp/
//"Cómo programar en C#. Segunda edición", Harvey M. Deitel y Paul J. Deitel. Mexico 2007, Pearson Educación.
//https://es.stackoverflow.com/
//https://stackoverrun.com/


namespace Otro_Final
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Bienvenido al gestior de patentes.\nCrearemos una pila donde almacenaremos los datos que ingreses.");
            Stack pila_patentes = new Stack();
            string guardamos;
            menu();

            int seleccion()
            {
                int opcion;
                int[] posibles = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0 };
                do
                {
                    Console.WriteLine("Selecciona una opción del menú.");
                    Console.WriteLine("1. Borrar pila.");
                    Console.WriteLine("2. Agregar patente a pila.");
                    Console.WriteLine("3. Eliminar patente."); //Elimina CIMA
                    Console.WriteLine("4. Mostrar las patentes almacenadas.");
                    Console.WriteLine("5. Mostrar la primer patente ingresada."); // PISO de pila
                    Console.WriteLine("6. Mostrar la última patente ingresada."); // CIMA de pila
                    Console.WriteLine("7. Mostrar la cantidad de patentes almacenadas.");
                    Console.WriteLine("8. Buscar patente.");  
                    Console.WriteLine("9. Función"); //Inventar una función. 
                    Console.WriteLine("0. Salir del programa.");
                    opcion = Convert.ToInt32(Console.ReadLine());
                    return opcion;
                } while (posibles.Contains(opcion) == false);
            }

            void menu()
            {
                switch (seleccion())
                {

                    case 1:
                        pila_patentes.Clear();
                        Console.WriteLine("Se han eliminado todas las patentes de la pila.");
                        menu();
                        break;
                    case 2:
                        agregar(ref pila_patentes);
                        menu();
                        break;
                    case 3:
                        eliminar_elemento(ref pila_patentes);
                        menu();
                        break;
                    case 4:
                        mostrar_patentes(ref pila_patentes);
                        menu();
                        break;
                    case 5:
                        mostrar_primera(ref pila_patentes);
                        menu();
                        break;
                    case 6:
                        mostrar_ultima(ref pila_patentes);
                        menu();
                        break;
                    case 7:
                        Console.WriteLine("La pila tiene: {0} patentes cargadas.", cantidad_elementos(ref pila_patentes));
                        menu();
                        break;
                    case 8:
                        buscar(ref pila_patentes);
                        menu();
                        break;
                    case 9:
                        Console.WriteLine("Agregar Función 9");
                        break;
                    case 0:
                        Console.WriteLine("Antes de salir, deseas guardar la información en un archivo? si/no");
                        guardamos = Console.ReadLine().ToLower();
                        if (guardamos == "si")
                        {
                            guardar_info(ref pila_patentes);
                            Thread.Sleep(1500);
                            salir();
                        }
                        else
                        {
                            salir();
                        }
                        break;

                }
                Console.ReadLine();
            }
        }

        static void salir()
        {
            Console.WriteLine("Muchas gracias.");
            Thread.Sleep(500);
            Environment.Exit(0);
        }

        static void agregar(ref Stack pila)
        {
            string patente;
            string seguir = "si";
            Console.WriteLine("Las patentes a ingresar deben respetar las siguientes condiciones:\n\tLos primeros 3 caracteres deben ser LETRAS\n\tLos últimos 3 caracteres deben ser NÚMEROS\n\tDebe contener 6 (seis) caracteres en total.");
            do
            {
                Console.WriteLine("Ingrese una patente: \n");
                patente = Convert.ToString(Console.ReadLine());
                while (patente.Length != 6)
                {
                    Console.WriteLine("Ingrese una patente que respete las condiciones: \n");
                    patente = Convert.ToString(Console.ReadLine());
                }
                try
                {
                    string primeros = patente.Substring(0, 3);
                    string segundos = patente.Substring(3);
                    int numeros = int.Parse(segundos);
                    if ((primeros.All(char.IsLetter)) && (numeros.GetType() == Type.GetType("System.Int32")))
                    //Validamos que los primeros 3 caracteres sean letras y los 3 últimos sean números. 
                    {
                        patente = (cantidad_elementos(ref pila)+1).ToString() + "-" + patente.ToString();
                        //Para asignarle la posisción a la patente, usamos la función que cuenta los elementos que contiene
                        //la pila. Sumamos 1, para que no inicie en 0. 
                        pila.Push(patente);
                    }
                    Console.WriteLine("Desea seguir ingresando patentes? si/no.\n ");
                    seguir = Console.ReadLine();
                    seguir.ToLower();
                    
                }
                catch (FormatException e)
                {
                    Console.WriteLine("La patente no respeta las condiciones establecidas.\n");
                    Console.WriteLine("Desea volver a ingresar la patente? si/no: ");
                    seguir = Console.ReadLine();
                    seguir.ToLower();
                    
                }

            } while (seguir == "si");
            Console.WriteLine("Se han agregado las patentes exitosamente.\n");
            
        }

        static void eliminar_elemento(ref Stack pila)
        {
            string continuar;
            try
            {
                string patente = (string)pila.Pop();
                Console.WriteLine("Se ha eliminado el elemento {0}\n", patente);
            }
            catch (System.InvalidOperationException e)
            
            {
                Console.WriteLine("La pila está vacía: no puedes eliminar elementos.\n");
                Console.WriteLine("Desea agregar patentes en la pila? si/no\n");
                continuar = Console.ReadLine();
                continuar.ToLower();
                if (continuar=="si")
                {
                    agregar(ref pila);
                }
                else
                {
                    salir();
                }
            }

        }

        static void mostrar_patentes(ref Stack pila)
        {
            if (cantidad_elementos(ref pila)> 0)
            {
                foreach (string dato in pila)
                {
                    Console.WriteLine(dato,"\n");   
                }
            }
            else
            {
                Console.WriteLine("La pila no tiene elementos.\n");
                Console.ReadLine();
            }
        }

        static void mostrar_primera(ref Stack pila)
        {
            
            int cont = 0;
            foreach (string dato in pila)
            {
                cont++;
                if (cont==cantidad_elementos(ref pila))
                //Recorremos la pila y al llegar la cantidad máxima de elementos, imprimimos el elemento
                //este es el piso de la pila. 
                {
                    Console.WriteLine("El primer elemento ingresado es: {0}\n", dato);  
                }
            }
        }

        static void mostrar_ultima(ref Stack pila)
        {
            Console.WriteLine(pila.Peek());
        }

        static int cantidad_elementos(ref Stack pila)
        {
            int cantidad = pila.Count;
            //Retornamos el valor así lo seguimos utilizando en otras funciones. 
            return cantidad;
        }
        
        static void buscar(ref Stack pila)
        {
            string patente;
            string agregamos;
            Stack auxiliar = new Stack();
            Console.WriteLine("Ingrese la patente que desea buscar: ");
            patente = Console.ReadLine();
            patente.ToLower();
            try
            {
                string primeros = patente.Substring(0, 3);
                string segundos = patente.Substring(3);
                int numeros = int.Parse(segundos);
                if ((primeros.All(char.IsLetter)) && (numeros.GetType() == Type.GetType("System.Int32")))
                {
                    foreach (string dato in pila)
                    {
                        auxiliar.Push(dato.Remove(0, 2));
                    }
                    if (auxiliar.Contains(patente))
                    {
                        Console.WriteLine("La patente: {0}, se encuentra en la pila.", patente);
                    }
                    else
                    {
                        Console.WriteLine("La pantente: {0}, NO está en la pila.\n", patente);
                        Console.WriteLine("Desea agregar la patente a la pila? si/no\n");
                        agregamos = Console.ReadLine().ToLower();
                        if (agregamos == "si")
                        {
                            patente = (cantidad_elementos(ref pila) + 1).ToString() + "-" + patente.ToString();
                            pila.Push(patente);
                            Console.WriteLine("Se han agregado las patentes exitosamente.\n");
                        }
                        else
                        {
                            Console.WriteLine("Te redirigiremos al menú principal.\n");
                        }
                    }
                }
                
            }
            catch
            {
                Console.WriteLine("El formato de la patente no coincide con el solicitado para esta pila\n");
                buscar(ref pila);
            }
        }
        static void guardar_info(ref Stack pila)
        {
            string agregar;
            string archivo;
            
            Console.WriteLine("Ingrese el nombre del archivo donde desea guardar los datos.\n");
            archivo = Console.ReadLine()+".txt";
            
            string ubicacion = @"C:\Users\Usuario\Desktop\Final_EEDD\"+archivo;
            try
            {
                if (File.Exists(ubicacion))
                {
                    Console.WriteLine("El archivo {0} ya existe.", archivo);
                    Console.WriteLine("Deseas sobreeescribir el archivo? si/no\n");
                    agregar = Console.ReadLine().ToLower();
                    if (agregar == "si")
                    {
                        Console.WriteLine("Los datos anteriores se perderán.");
                        StreamWriter sobreescribo = new StreamWriter(ubicacion);
                        foreach (string dato in pila)
                        {
                            sobreescribo.WriteLine(dato.Remove(0, 2));
                        }
                        Console.WriteLine("Se han guardado los archivos.");
                        sobreescribo.Close();
                    }
                    if (agregar == "no")
                    {
                        StreamWriter writer = File.AppendText(ubicacion);
                        foreach (string dato in pila)
                        {
                            writer.WriteLine(dato.Remove(0, 2));
                        }
                        Console.WriteLine("Agregaremos los datos al archivo.");
                        writer.Close();
                    }
                }
                else
                {
                    Console.WriteLine("El archivo no existe.");
                    Console.WriteLine("Crearemos el archivo {0}", archivo);
                    StreamWriter writer = File.AppendText(ubicacion);
                    foreach (string dato in pila)
                    {
                        writer.WriteLine(dato.Remove(0, 2));
                    }
                    writer.Close();
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Error: verifique la dirección donde sea guardar los datos.");
                guardar_info(ref pila);
            }

        }
    }
}
