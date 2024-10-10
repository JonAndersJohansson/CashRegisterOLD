using CashRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CashRegister
{
    public class Menus
    {
        NewCostumer newcostumer = new NewCostumer();

        public List<string> listMainMenu = new List<string>
        {
            "1. Ny kund", "2. Administratörverktyg", "Avsluta"
        };
        public List<string> listAdminMenu = new List<string>
        {
            "1. Ändra PLU/Namn/Pris på produkt", "2. Lägg till ny produkt", "3. Ta bort produkt", "4. Lägg till kampanj", "5. Ta bort kampanj", "Tillbaka"
        };


        public void MainMenu()
        {
            switch (DisplayList(listMainMenu))
            {
                case 0:
                    newcostumer.Customer();
                    break;
                case 1:
                    AdminMenu();
                    break;
                case 2:
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Ogiltigt alternativ, försök igen.");
                    Thread.Sleep(2000);  // Pausar kort innan vi rensar
                    break;
            }
        }
        public void AdminMenu()
        {
            ProductHandeling productHandeling = new ProductHandeling();

            switch (DisplayList(listAdminMenu))
            {
                case 0:
                    productHandeling.ChangeProduct();
                    break;
                case 1:
                    productHandeling.AddProduct();
                    break;
                case 2:
                    productHandeling.RemoveProduct();
                    break;
                case 3:
                    Console.WriteLine("Val 4");
                    break;
                case 4:
                    Console.WriteLine("Val 5");
                    break;
                case 5:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Ogiltigt alternativ, försök igen.");
                    Thread.Sleep(2000);  // Pausar kort innan vi rensar
                    break;
            }
        }

        public int DisplayList<T>(List<T> lista)
        {
            int selectedIndex = 0;
            bool running = true;

            while (running)
            {
                Console.Clear();
                MenuGraphics();
                //if (lista is List<Products>)
                //{
                //    Console.WriteLine("Välj produkt och tryck Enter:");
                //}
                //else
                //{
                //    Console.WriteLine("Använd upp/ned piltangenterna och tryck Enter:");
                //}
                Console.WriteLine();

                // Visa butiker och markera vald butik
                for (int i = 0; i < lista.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        // Kolla om strängen är "Avsluta" eller "Tillbaka" och sätt färgen till röd
                        if (lista[i].ToString() == "Avsluta" || lista[i].ToString() == "Tillbaka")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ResetColor(); // Återställ färg för andra poster
                        }
                    }

                    Console.WriteLine(lista[i]);

                    // Återställ färger efter att ha markerat vald butik
                    Console.ResetColor();
                }

                // Lägg till en rad för att avsluta programmet

                //if (selectedIndex == lista.Count)
                //{
                //    Console.BackgroundColor = ConsoleColor.Blue;
                //    Console.ForegroundColor = ConsoleColor.White;
                //}
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("Exit");
                //Console.ResetColor();


                // Läs tangenttryckning
                var keyInfo = Console.ReadKey(true);

                // Hantera piltangenterna med enklare uttryck
                // Vad är fördelen med detta sätt?
                // (vi slipper 'try catch'!)
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex--; // Minska indexet
                    if (selectedIndex < 0)
                    {
                        selectedIndex = lista.Count; // Om vi går förbi början, hoppa till sista alternativet (Exit)
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex++; // Öka indexet
                    if (selectedIndex >= lista.Count)
                    {
                        selectedIndex = 0; // Om vi går förbi slutet, hoppa till första alternativet
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return selectedIndex;
                }
            }
            return -1;
        }
        public void MenuGraphics()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("             ██╗  ██╗ ██████╗  ██████╗ ██████╗ ");
            Console.WriteLine("             ██║ ██╔╝██╔═══██╗██╔═══██╗██╔══██╗");
            Console.WriteLine("             █████╔╝ ██║   ██║██║   ██║██████╔╝");
            Console.WriteLine("             ██╔═██╗ ██║   ██║██║   ██║██╔═══╝ ");
            Console.WriteLine("             ██║  ██╗╚██████╔╝╚██████╔╝██║     ");
            Console.WriteLine("             ╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ®  ");
            Console.WriteLine("                    ~   Örnsköldsvik   ~");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }
}
