using System;
using System.Collections.Generic;

class Program
{
    static string[] categorias = {
        "Ficción", "Ciencia", "Historia", "Arte", "Infantil"
    };

    static LinkedList<string> socios = new LinkedList<string>();
    static Queue<string> filaEspera = new Queue<string>();
    static Stack<string> historial = new Stack<string>();

    static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("  SISTEMA DE BIBLIOTECA — UNIDAD 1");
        Console.WriteLine("==========================================");

        MostrarCatalogo();
        BuscarCategoria("Arte");
        BuscarCategoria("Música");
        Console.WriteLine();

        InscribirSocio("Ana Ramírez");
        InscribirSocio("Luis Torres");
        InscribirSocio("Sofía Gómez");
        DarDeBajaSocio("Luis Torres");
        MostrarSocios();
        Console.WriteLine();

        PedirLibro("Ana Ramírez", "Cien años de soledad");
        PedirLibro("Sofía Gómez", "Cien años de soledad");
        PedirLibro("Marco Díaz",  "Cien años de soledad");
        EntregarSiguiente();
        CancelarSolicitud("Marco Díaz");
        CancelarSolicitud("Karla Ruiz");
        Console.WriteLine();

        RegistrarPrestamo("Ana Ramírez: Cien años de soledad");
        RegistrarPrestamo("Sofía Gómez: El Principito");
        DeshacerUltimoPrestamo();
        DeshacerUltimoPrestamo();
        DeshacerUltimoPrestamo(); // probamos historial vacío
        MostrarHistorial();
    }

    // ── Módulo Catálogo (Array) ──────────────────────────────────
    static void MostrarCatalogo()
    {
        Console.WriteLine("--- Catálogo de categorías ---");
        for (int i = 0; i < categorias.Length; i++)
            Console.WriteLine($"  [{i}] {categorias[i]}");
    }

    // Extensión 1: buscar una categoría en el arreglo
    static void BuscarCategoria(string nombre)
    {
        for (int i = 0; i < categorias.Length; i++)
        {
            if (categorias[i].Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"'{nombre}' encontrada en el índice [{i}]");
                return;
            }
        }
        Console.WriteLine($"'{nombre}' no existe en el catálogo");
    }

    // ── Módulo Socios (LinkedList) ───────────────────────────────
    static void InscribirSocio(string nombre)
    {
        socios.AddLast(nombre);
        Console.WriteLine($"Socio inscrito: {nombre}");
    }

    static void DarDeBajaSocio(string nombre)
    {
        if (socios.Contains(nombre))
        {
            socios.Remove(nombre);
            Console.WriteLine($"Socio dado de baja: {nombre}");
        }
    }

    static void MostrarSocios()
    {
        Console.WriteLine("--- Socios activos ---");
        foreach (string s in socios)
            Console.WriteLine($"  {s}");
        Console.WriteLine($"Total: {socios.Count}");
    }

    // ── Módulo Fila de espera (Queue) ────────────────────────────
    static void PedirLibro(string solicitante, string titulo)
    {
        filaEspera.Enqueue(solicitante);
        Console.WriteLine($"{solicitante} solicitó: {titulo}");
    }

    static void EntregarSiguiente()
    {
        if (filaEspera.Count > 0)
        {
            string siguiente = filaEspera.Dequeue();
            Console.WriteLine($"Libro entregado a: {siguiente}");
            Console.WriteLine($"En espera todavía: {filaEspera.Count}");
        }
    }

    // Extensión 2: cancelar solicitud verificando con Contains primero
    static void CancelarSolicitud(string nombre)
    {
        if (filaEspera.Contains(nombre))
        {
            // Queue no tiene Remove directo: se reconstruye sin el elemento
            Queue<string> nuevaFila = new Queue<string>();
            foreach (string persona in filaEspera)
            {
                if (persona != nombre)
                    nuevaFila.Enqueue(persona);
            }
            filaEspera = nuevaFila;
            Console.WriteLine($"Solicitud de {nombre} cancelada");
        }
        else
        {
            Console.WriteLine($"{nombre} no tiene solicitud activa en la fila");
        }
    }

    // ── Módulo Historial (Stack) ─────────────────────────────────
    static void RegistrarPrestamo(string registro)
    {
        historial.Push(registro);
        Console.WriteLine($"Préstamo registrado: {registro}");
    }

    // Extensión 3: validar antes de deshacer para evitar InvalidOperationException
    static void DeshacerUltimoPrestamo()
    {
        if (historial.Count > 0)
        {
            string deshecho = historial.Pop();
            Console.WriteLine($"Préstamo deshecho: {deshecho}");
        }
        else
        {
            Console.WriteLine("No hay préstamos para deshacer");
        }
    }

    static void MostrarHistorial()
    {
        Console.WriteLine("--- Historial (más reciente primero) ---");
        foreach (string h in historial)
            Console.WriteLine($"  {h}");
    }
}
