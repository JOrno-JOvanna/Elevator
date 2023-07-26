using System;

namespace ElevatorSimulated
{
    internal class Floor
    {
        public int CurrentFloorCabin1 { get; set; }
        public ElevatorState Cabin1State { get; set; }
        public int CurrentFloorCabin2 { get; set; }
        public ElevatorState Cabin2State { get; set; }
        public bool CallButton { get; set; }

        public void PressCallButton(int floor, ElevatorCabin cabin1, ElevatorCabin cabin2)
        {
            if (CallButton)
            {
                if ((Math.Abs(CurrentFloorCabin1 - floor) > Math.Abs(CurrentFloorCabin2 - floor)) || (cabin1.isMoving))
                {
                    CallCabinOnFloor();
                    Console.WriteLine("Едет кабина 2");
                    cabin2.CabinMoving(floor);
                    Cabin2State = cabin2.State;
                    cabin2.CurrentFloor = floor;
                }
                else if (Math.Abs(CurrentFloorCabin1 - floor) < Math.Abs(CurrentFloorCabin2 - floor))
                {
                    Console.WriteLine("Едет кабина 1");
                    cabin1.CabinMoving(floor);
                    Cabin1State = cabin1.State;
                    cabin1.CurrentFloor = floor;
                }
                else if ((CurrentFloorCabin1 == CurrentFloorCabin2) || (CurrentFloorCabin1 == CurrentFloorCabin2 & !cabin1.isMoving))
                {
                    Console.WriteLine("Едет кабина 1");
                    cabin1.CabinMoving(floor);
                    Cabin1State = cabin1.State;
                    cabin1.CurrentFloor = floor;
                }
                CallButton = false;
            }
        }

        public void CallCabinOnFloor()
        {

        }
    }
}
