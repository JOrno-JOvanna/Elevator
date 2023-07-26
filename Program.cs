using System;
using System.Threading.Tasks;

namespace ElevatorSimulated
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ElevatorCabin cabin1 = new ElevatorCabin();
            ElevatorCabin cabin2 = new ElevatorCabin();
            cabin1.Number = 1;
            cabin2.Number = 2;
            cabin1.CurrentFloor = 1;
            cabin2.CurrentFloor = 1;
            string passenger1 = "Пассажир 1";
            string passenger2 = "Пассажир 2";

            Floor[] totalfloors = new Floor[20];

            for (int i = 0; i < totalfloors.Length; i++)
            {
                totalfloors[i] = new Floor();
                totalfloors[i].CurrentFloorCabin1 = cabin1.CurrentFloor;
                totalfloors[i].CurrentFloorCabin2 = cabin2.CurrentFloor;
            }

            Task task1 = SimulatedActions(totalfloors, cabin1, cabin2, 1, 14, passenger1);
            Task task2 = SimulatedActions(totalfloors, cabin1, cabin2, 15, 1, passenger2);
            await Task.WhenAll(task1, task2);
            Console.ReadLine();
        }

        static async Task SimulatedActions(Floor[] totalfloors, ElevatorCabin cabin1, ElevatorCabin cabin2, int passengerfloor, int neededfloor, string passenger)
        {
            for (int i = 0; i < totalfloors.Length; i++)
            {
                totalfloors[i].CurrentFloorCabin1 = cabin1.CurrentFloor;
                totalfloors[i].CurrentFloorCabin2 = cabin2.CurrentFloor;
            }

            Console.WriteLine($"{passenger} вызывает лифт на этаже {passengerfloor}");
            totalfloors[passengerfloor - 1].CallButton = true;
            totalfloors[passengerfloor - 1].PressCallButton(passengerfloor, cabin1, cabin2);

            await Task.Delay(1000);

            if (totalfloors[passengerfloor - 1].Cabin1State == ElevatorState.DoorsOpened & cabin1.CurrentFloor == passengerfloor)
            {
                Console.WriteLine($"{passenger} заходит в кабину 1");
                cabin1.PressFloorButton(neededfloor, passenger);
            }
            else
            {
                Console.WriteLine($"{passenger} заходит в кабину 2");
                cabin2.PressFloorButton(neededfloor, passenger);
            }
            Console.WriteLine($"{passenger} выходит из лифта");
        }
    }
}
