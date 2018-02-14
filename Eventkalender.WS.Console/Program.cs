using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eventkalender.WS.ConsoleApp.EventkalenderReference;

namespace Eventkalender.WS.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EventkalenderServiceSoapClient client = new EventkalenderServiceSoapClient();
            int i;           
         
            while (true)
            {
                Console.WriteLine("Hej Gringos, Välj vad du vill göra!");
                Console.WriteLine("Hämta specifik Nation: Tryck 1");
                Console.WriteLine("Hämta alla Nationer: Tryck 2");
                Console.WriteLine("Hämta specifikt Event: Tryck 3");
                Console.WriteLine("Hämta alla Events: Tryck 4");
                Console.WriteLine("Hämta specifik Person: Tryck 5");
                Console.WriteLine("Hämta alla Personer: Tryck 6");
                char userInput = Console.ReadKey().KeyChar;
                if (char.IsDigit(userInput))
                {
                    i = int.Parse(userInput.ToString());
                    Console.WriteLine("\nDu valde alternativ : {0}", i);
                    
                }
                else
                {
                    i = 0;
                    Console.WriteLine("\n Du satte inte in ett nummer, Vill du avsluta? Skriv J, Vill du börja om, skriv N");
                    userInput = Console.ReadKey().KeyChar;
                    
                   
                    if (userInput.ToString().ToUpper().Equals("J"))
                    {
                        Console.WriteLine("Avslutar denna feta konsollapplikation");
                        break;
                    }
                }
                if (i == 1)
                {
                    Console.WriteLine("Ange nationens ID: ");
                    userInput = Console.ReadKey().KeyChar;
                    i = int.Parse(userInput.ToString());
                    Nation n = client.GetNation(i);

                    if (n != null)
                    {
                        Console.WriteLine("\nNationens namn är: {0}", n.Name);
                        Console.WriteLine("Nationen har följande events: ");
                        Event[] events = n.Events;
                        for(int j=0; j < events.Length; j++)
                        {
                            Console.WriteLine(events[j].Name);
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("Det finns ingen nation med detta id: {0}", i);
                    }
                    
                }
                if(i == 2)
                {
                    Console.WriteLine("Följande är alla nationer: ");
                    Nation[] nations = client.GetNations();
                    for (int j = 0; j < nations.Length; j++)
                    {
                        Console.WriteLine(nations[j].Name);
                    }
                }
                if(i == 3)
                {
                    Console.WriteLine("Ange eventets ID: ");
                    userInput = Console.ReadKey().KeyChar;
                    i = int.Parse(userInput.ToString());
                    Event e = client.GetEvent(i);

                    if (e != null)
                    {
                        Console.WriteLine("\nEventets namn är: {0}", e.Name);
                        Console.WriteLine("{0} anordnar {1}", e.Nation.Name, e.Name);                      
      
                    }
                    else
                    {
                        Console.WriteLine("Det finns inget event med detta id: {0}", i);
                    }
                }
                if (i == 4)
                {
                    Console.WriteLine("Följande är alla nationer: ");
                    Event[] events = client.GetEvents();
                    for (i = 0; i < events.Length; i++)
                    {
                        Console.WriteLine(events[i].Name);
                    }
                }
                if(i == 5)
                {
                    Console.WriteLine("Ange personens ID: ");
                    userInput = Console.ReadKey().KeyChar;
                    i = int.Parse(userInput.ToString());
                    Person p = client.GetPerson(i);

                    if (p != null)
                    {
                        Console.WriteLine("\nPersonens namn är: {0}, {1}", p.FirstName, p.LastName);
                        Console.WriteLine("{0} ska gå på följande events:", p.FirstName);
                        Event []events = p.Events;
                        for (int j = 0; j < events.Length; j++)
                        {
                            Console.WriteLine(events[j].Name);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Det finns inget event med detta id: {0}", i);
                    }
                }
                if(i==6)
                {
                    Console.WriteLine("Följande är alla personer: ");
                    Person[] personer = client.GetPersons();
                    for (int j = 0; j < personer.Length; j++)
                    {
                        Console.WriteLine("{0} {1}",personer[j].FirstName, personer[j].LastName);
                    }
                }
                Console.WriteLine("\nVill du avsluta? Skriv J, Vill du börja om, skriv N");
                userInput = Console.ReadKey().KeyChar;
                
              
                if (userInput.ToString().ToUpper().Equals("J"))
                {
                    Console.WriteLine("Avslutar denna feta konsollapplikation");
                    break;
                }
                Console.WriteLine("\n");
            }
            
        }
    }
}
