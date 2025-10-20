using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace Functions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class Interval
    {
        public double MinValue { get; }
        public double MaxValue { get; }
        public bool MinOpen { get; }
        public bool MaxOpen { get; }
        public Interval(double minValue, double maxValue, bool minOpen, bool maxOpen)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            MinOpen = minOpen;
            MaxOpen = maxOpen;
        }
        public override string ToString()
        {
            string result = "";
            result += MinOpen ? "(" : "<";
            result += MinValue == double.NegativeInfinity ? "-∞;" : $"{MinValue};";
            result += MaxOpen ? ")" : ">";
            result += MaxValue == double.PositiveInfinity ? "∞;" : $"{MaxValue};";
            return result;
        }
    }
    public abstract class Function
    {
        public string Name { get; }
        public string Description { get; }
        protected List<Interval> Domain { get; } // Definiční obor
        protected List<Interval> CoDomain { get; } // Obor hodnot
        public string ReturnDomain()
        {
            return String.Join<Interval>('∪', Domain.ToArray());
        }
        public string ReturnCoDomain()
        {
            return String.Join<Interval>('∪', CoDomain.ToArray());
        }
        public abstract double CalculateFx(double x);
        public override string ToString()
        {
            return $"{Name} F(x) = {Description}\nDf = {ReturnDomain()}, Hf = {ReturnCoDomain()}";
        }
    }
}
