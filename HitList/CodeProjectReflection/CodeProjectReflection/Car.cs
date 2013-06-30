namespace CodeProjectReflection
{
    internal class Car : ICar
    {
        public string Colour;

        public int Speed { get; private set; }

        public void Accelerate(int accelerateBy)
        {
            Speed += accelerateBy;
        }

        public bool IsMoving()
        {
            return Speed > 0;
        }

        public Car()
        {
            Colour = "White";
            Speed = 0;
        }

        public Car(string colour, int speed)
        {
            Colour = colour;
            Speed = speed;
        }

        public double CalculateKpl(int startKilometres, int endKilometres, double litres)
        {
            return (endKilometres - startKilometres)/litres;
        }
    }
}
