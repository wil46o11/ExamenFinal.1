using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinal
{
    internal class Program
    {
        // =======================
        // ESTRUCTURAS DE DATOS
        // =======================

        // Turnos: 0 = mañana, 1 = tarde
        static string[,] nombres = new string[2, 20];
        static string[,] combos = new string[2, 20];
        static double[,] precios = new double[2, 20];

        // Menú de combos
        static string[] menuNombres = { "Combo 1: Café + Pan", "Combo 2: Jugo + Sándwich", "Combo 3: Té + Galletas" };
        static double[] menuPrecios = { 5.50, 7.00, 4.00 };
        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("==== SISTEMA DE RESERVAS - CAFETERÍA ====");
                Console.WriteLine("1. Mostrar menú de combos");
                Console.WriteLine("2. Registrar reserva");
                Console.WriteLine("3. Cancelar reserva");
                Console.WriteLine("4. Listar reservas por turno");
                Console.WriteLine("5. Buscar reserva por nombre");
                Console.WriteLine("6. Reporte de ingresos");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1: MostrarMenu(); break;
                    case 2: RegistrarReserva(); break;
                    case 3: CancelarReserva(); break;
                    case 4: ListarReservas(); break;
                    case 5: BuscarReserva(); break;
                    case 6: ReporteIngresos(); break;
                }

                if (opcion != 7)
                {
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcion != 7);
        }

        // ======================
        // FUNCIONES DEL SISTEMA
        // ======================

        static void MostrarMenu()
        {
            Console.WriteLine("\n=== MENÚ DE COMBOS ===");
            for (int i = 0; i < menuNombres.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menuNombres[i]} - S/. {menuPrecios[i]}");
            }
        }

        static void RegistrarReserva()
        {
            Console.WriteLine("\n=== REGISTRAR RESERVA ===");
            Console.Write("Turno (0 = mañana, 1 = tarde): ");
            int turno = int.Parse(Console.ReadLine());

            int cupo = BuscarCupo(turno);

            if (cupo == -1)
            {
                Console.WriteLine("No hay cupos disponibles.");
                return;
            }

            Console.Write("Nombre del estudiante: ");
            string nombre = Console.ReadLine();

            MostrarMenu();
            Console.Write("Seleccione combo: ");
            int c = int.Parse(Console.ReadLine()) - 1;

            nombres[turno, cupo] = nombre;
            combos[turno, cupo] = menuNombres[c];
            precios[turno, cupo] = menuPrecios[c];

            Console.WriteLine("Reserva registrada correctamente.");
        }

        static int BuscarCupo(int turno)
        {
            for (int i = 0; i < 20; i++)
            {
                if (string.IsNullOrEmpty(nombres[turno, i]))
                    return i;
            }
            return -1;
        }

        static void CancelarReserva()
        {
            Console.WriteLine("\n=== CANCELAR RESERVA ===");
            Console.Write("Turno (0 = mañana, 1 = tarde): ");
            int turno = int.Parse(Console.ReadLine());

            Console.Write("Nombre del estudiante: ");
            string nombre = Console.ReadLine();

            for (int i = 0; i < 20; i++)
            {
                if (nombres[turno, i] == nombre)
                {
                    nombres[turno, i] = null;
                    combos[turno, i] = null;
                    precios[turno, i] = 0;

                    Console.WriteLine("Reserva cancelada.");
                    return;
                }
            }

            Console.WriteLine("Reserva no encontrada.");
        }

        static void ListarReservas()
        {
            Console.WriteLine("\n=== LISTAR RESERVAS ===");
            Console.Write("Turno (0 = mañana, 1 = tarde): ");
            int turno = int.Parse(Console.ReadLine());

            Console.WriteLine("\n--- Reservas del turno ---");
            for (int i = 0; i < 20; i++)
            {
                if (!string.IsNullOrEmpty(nombres[turno, i]))
                {
                    Console.WriteLine($"{i + 1}. {nombres[turno, i]} - {combos[turno, i]} - S/. {precios[turno, i]}");
                }
            }
        }

        static void BuscarReserva()
        {
            Console.WriteLine("\n=== BUSCAR RESERVA ===");
            Console.Write("Nombre del estudiante: ");
            string nombre = Console.ReadLine();

            for (int t = 0; t < 2; t++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (nombres[t, i] == nombre)
                    {
                        string turnoTxt = t == 0 ? "Mañana" : "Tarde";
                        Console.WriteLine($"Encontrado en turno {turnoTxt}: {combos[t, i]} - S/. {precios[t, i]}");
                        return;
                    }
                }
            }

            Console.WriteLine("No se encontró la reserva.");
        }

        static void ReporteIngresos()
        {
            Console.WriteLine("\n=== REPORTE DE INGRESOS ===");

            double mañana = 0, tarde = 0;

            for (int i = 0; i < 20; i++)
            {
                mañana += precios[0, i];
                tarde += precios[1, i];
            }

            Console.WriteLine($"Ingresos turno mañana: S/. {mañana}");
            Console.WriteLine($"Ingresos turno tarde: S/. {tarde}");
            Console.WriteLine($"Total del día: S/. {mañana + tarde}");
        }
    }
    
}
