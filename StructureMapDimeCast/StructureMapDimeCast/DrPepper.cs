using StructureMap;

namespace StructureMapDimeCast
{
    public class DrPepper : ISoda
    {
        public int Calories { get; private set; }
        public string Name { get; private set; }
    }
}