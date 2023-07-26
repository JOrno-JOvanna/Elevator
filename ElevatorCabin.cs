using System;
using System.Threading;

namespace ElevatorSimulated
{
    internal class ElevatorCabin
    {
        public int CurrentFloor { get; set; }
        private ElevatorState state;
        public int Number { get; set; }
        public bool isMoving { get; set; }

        public ElevatorState State
        {
            get => state;
            set
            {
                state = value;
                switch (state)
                {
                    case ElevatorState.GoingUp:
                        Console.WriteLine("Лифт движется вверх.");
                        break;
                    case ElevatorState.GoingDown:
                        Console.WriteLine("Лифт движется вниз.");
                        break;
                    case ElevatorState.DoorsOpening:
                        Console.WriteLine("Двери лифта открываются...");
                        break;
                    case ElevatorState.DoorsClosing:
                        Console.WriteLine("Двери лифта закрываются...");
                        break;
                    case ElevatorState.DoorsOpened:
                        Console.WriteLine("Двери лифта открыты.");
                        break;
                }
            }
        }

        public void PressFloorButton(int floor, string passenger)
        {
            // Метод для нажатия кнопки этажа на кабине лифта
            Console.WriteLine($"{passenger} нажимает на {floor} этаж");
            State = ElevatorState.DoorsClosing;
            CabinMoving(floor);
        }

        public void PressCloseButton()
        {
            // Метод для нажатия кнопки закрытия дверей на кабине лифта
            State = ElevatorState.DoorsClosing;
        }

        public void PressOpenButton()
        {
            // Метод для нажатия кнопки открытия дверей на кабине лифта
            State = ElevatorState.DoorsOpening;
            State = ElevatorState.DoorsOpened;
        }

        public void PressDispatcherButton()
        {
            // Метод для нажатия кнопки вызова диспетчера на кабине лифта

        }

        public void CabinMoving(int floor)
        {
            // Метод для обработки движения кабины между дверьми
            isMoving = true;
            if (CurrentFloor < floor)
            {
                State = ElevatorState.GoingUp;
                for (int i = CurrentFloor; i < floor; i++)
                {
                    Console.WriteLine($"Кабина {Number}: {i}=>{i + 1}");
                    CurrentFloor = i + 1;
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"Кабина {Number} приехала на этаж {floor}");
                CurrentFloor = floor;
                State = ElevatorState.DoorsOpening;
                State = ElevatorState.DoorsOpened;
                CabinStopped();
            }
            else if (CurrentFloor > floor)
            {
                State = ElevatorState.GoingDown;
                for (int i = CurrentFloor; i > floor; i--)
                {
                    Console.WriteLine($"Кабина {Number}: {i}=>{i - 1}");
                    CurrentFloor = i - 1;
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"Кабина {Number} приехала на этаж {floor}");
                CurrentFloor = floor;
                State = ElevatorState.DoorsOpening;
                State = ElevatorState.DoorsOpened;
                CabinStopped();
            }
            else if (CurrentFloor == floor)
            {
                PressOpenButton();
            }

        }

        public void CabinStopped()
        {
            // Метод для обработки остановки кабины
            isMoving = false;
        }
    }

    public enum ElevatorState
    {
        GoingUp,
        GoingDown,
        DoorsOpening,
        DoorsClosing,
        DoorsOpened
    }
}
