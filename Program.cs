using System;
using System.Threading.Tasks;

namespace ElevatorConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ElevatorCabin cabin1 = new ElevatorCabin();
            ElevatorCabin cabin2 = new ElevatorCabin();

            cabin1.CurrentFloor = 1;
            cabin2.CurrentFloor = 1;

            Floor[] totalfloors = new Floor[20];

            for (int i = 0; i < totalfloors.Length; i++)
            {
                totalfloors[i] = new Floor();
            }


            while (true)
            {
                for (int i = 0; i < totalfloors.Length; i++)
                {
                    totalfloors[i].CurrentFloorCabin1 = cabin1.CurrentFloor;
                    totalfloors[i].CurrentFloorCabin2 = cabin2.CurrentFloor;
                }

                bool running = true;

                Console.WriteLine("Вы вызываете лифт на этаже: ");
                if(Int16.TryParse(Console.ReadLine(), out Int16 passenger1currentfloor))
                {
                    try
                    {
                        totalfloors[passenger1currentfloor - 1].CallButton = true;
                        totalfloors[passenger1currentfloor - 1].PressCallButton(passenger1currentfloor, cabin1, cabin2);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine("В доме только 20 этажей: с 1 по 20");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Нужно число");
                    continue;
                }

                Console.WriteLine("Введите нужный этаж:");
                Task delay = Task.Delay(7000);
                Task<string> index = Task.Run(() => Console.ReadLine());
                Task complete = await Task.WhenAny(delay, index);

                if (complete == index)
                {
                    string number = await index;
                    do
                    {
                        if (Int16.TryParse(number, out Int16 floor))
                        {
                            if (floor >= 1 & floor <= 20)
                            {
                                Console.WriteLine("Нажмите Enter чтобы закрыть двери или подождите 5 секунд");
                                if (totalfloors[passenger1currentfloor - 1].Cabin1State == ElevatorState.DoorsOpened & cabin1.CurrentFloor == passenger1currentfloor)
                                {
                                    delay = Task.Delay(5000);
                                    index = Task.Run(() => ReadLineWithTimeout());
                                    complete = await Task.WhenAny(delay, index);
                                    if (complete == index)
                                    {
                                        cabin1.PressFloorButton(floor);
                                    }
                                    else
                                    {
                                        cabin1.PressFloorButton(floor);
                                    }
                                }
                                else
                                {
                                    delay = Task.Delay(5000);
                                    index = Task.Run(() => ReadLineWithTimeout());
                                    complete = await Task.WhenAny(delay, index);
                                    if (complete == index)
                                    {
                                        cabin2.PressFloorButton(floor);
                                    }
                                    else
                                    {
                                        cabin2.PressFloorButton(floor);
                                    }
                                }
                                running = false;
                            }
                            else
                            {
                                Console.WriteLine("В доме только 20 этажей: с 1 по 20");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Нужно число");
                            continue;
                        }
                    }
                    while (running);
                }
                else
                {
                    if (totalfloors[passenger1currentfloor - 1].Cabin1State == ElevatorState.DoorsOpened & cabin1.CurrentFloor == passenger1currentfloor)
                    {
                        cabin1.PressCloseButton();
                    }
                    else
                    {
                        cabin2.PressCloseButton();
                    }
                }

                Console.WriteLine($"Текущий этаж кабины 1: {cabin1.CurrentFloor}\nТекущий этаж кабины 2: {cabin2.CurrentFloor}");
            }
        }

        static string ReadLineWithTimeout()
        {
            // Метод для чтения ввода пользователя с таймаутом
            string input = "";
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    input += key.KeyChar;
                }
            }
            return input;
        }
    }
}
