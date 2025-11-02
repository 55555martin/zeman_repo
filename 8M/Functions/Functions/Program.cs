using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Functions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Function> functions = new List<Function>();
            functions.Add(new Linear(5, 4));
            functions.Add(new AbsoluteLinear(5, -4));
            functions.Add(new LinearFractional(5, 4, 1, -1));
            functions.Add(new Quadratic(-2, 4, 12));
            double x = 3;
            Console.OutputEncoding = Encoding.UTF8; // Aby to zobrazovalo symbol nekonečna
            foreach (Function f in functions)
            {
                Console.WriteLine(f.ToString());
                Console.WriteLine($"f({x}) = {f.CalculateFx(x)}");
                Console.WriteLine();
            }
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
            result += MaxValue == double.PositiveInfinity ? "∞" : $"{MaxValue}";
            result += MaxOpen ? ")" : ">";
            return result;
        }
    }
    public abstract class Function
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        protected List<Interval> Domain { get; set; } // Definiční obor
        protected List<Interval> CoDomain { get; set; } // Obor hodnot
        public string ReturnDomain()
        {
            List<string> intervals = new List<string>();
            foreach (Interval interval in Domain)
            {
                intervals.Add(interval.ToString());
            }
            return String.Join('v', intervals);
        }
        public string ReturnCoDomain()
        {
            List<string> intervals = new List<string>();
            foreach (Interval interval in CoDomain)
            {
                intervals.Add(interval.ToString());
            }
            return String.Join('v', intervals);
        }
        public abstract double? CalculateFx(double x);
        public override string ToString()
        {
            return $"{Name}\nF(x) = {Description}\nDf = {ReturnDomain()}\nHf = {ReturnCoDomain()}";
        }
    }

    public class Linear : Function
    {
        private double a { get; }
        private double b { get; }
        public Linear(double a, double b)
        {
            Name = "Lineární funkce";
            this.a = a;
            this.b = b;
            Description = b >= 0 ? $"{a}x + {b}" : $"{a}x {b}";
            Domain = new List<Interval>();
            Domain.Add(new Interval(double.NegativeInfinity, double.PositiveInfinity, true, true));
            CoDomain = new List<Interval>();
            CoDomain.Add(new Interval(double.NegativeInfinity, double.PositiveInfinity, true, true));
        }
        public override double? CalculateFx(double x)
        {
            return a * x + b;
        }
    }

    public class AbsoluteLinear : Function
    {
        private double a { get; }
        private double b { get; }
        public AbsoluteLinear(double a, double b)
        {
            Name = "Lineární funkce v absolutní hodnotě";
            this.a = a;
            this.b = b;
            Description = b >= 0 ? $"|{a}x + {b}|" : $"|{a}x {b}|";
            Domain = new List<Interval>();
            Domain.Add(new Interval(double.NegativeInfinity, double.PositiveInfinity, true, true));
            CoDomain = new List<Interval>();
            CoDomain.Add(new Interval(0, double.PositiveInfinity, false, true));
        }
        public override double? CalculateFx(double x)
        {
            return Math.Abs(a * x + b);
        }
    }

    public class LinearFractional : Function
    {
        private double a { get; }
        private double b { get; }
        private double c { get; }
        private double d { get; }
        public LinearFractional(double a, double b, double c, double d)
        {
            Name = "Lineární lomená funkce";
            this.a = a;
            this.b = b;
            this.c = c; // c != 0
            this.d = d;
            Description = b >= 0 ? d >= 0 ? $"({a}x + {b})/({c}x + {d})" : $"({a}x + {b})/({c}x {d})" : $"({a}x {b})/({c}x {d})";
            Description += "\nhyperbola";
            Domain = new List<Interval>();
            Domain.Add(new Interval(double.NegativeInfinity, -d / c, true, true));
            Domain.Add(new Interval(-d / c, double.PositiveInfinity, true, true));
            CoDomain = new List<Interval>();
            CoDomain.Add(new Interval(double.NegativeInfinity, a / c, true, true));
            CoDomain.Add(new Interval(a / c, double.PositiveInfinity, true, true));
        }
        public override double? CalculateFx(double x)
        {
            if (x == -d/c)
                return null;
            return (a * x + b)/(c * x + d);
        }
    }

    public class Quadratic : Function
    {
        private double a { get; }
        private double b { get; }
        private double c { get; }
        private double vrchol_x { get; }
        private double vrchol_y { get; }
        public Quadratic(double a, double b, double c)
        {
            Name = "Kvadratická funkce";
            this.a = a; // a != 0
            this.b = b;
            this.c = c;
            vrchol_x = -b / (2*a);
            vrchol_y = Convert.ToDouble(this.CalculateFx(vrchol_x));
            Description = b >= 0 ? c >= 0 ? $"{a}x^2 + {b}x + {c}" : $"{a}x^2 + {b}x {c}" : $"{a}x^2 {b}x {c}";
            Description += "\nparabola";
            Domain = new List<Interval>();
            Domain.Add(new Interval(double.NegativeInfinity, double.PositiveInfinity, true, true));
            CoDomain = new List<Interval>();
            if (a < 0)
                CoDomain.Add(new Interval(double.NegativeInfinity, vrchol_y, true, false));
            else
                CoDomain.Add(new Interval(vrchol_y, double.PositiveInfinity, false, true));
        }
        public override double? CalculateFx(double x)
        {
            return a * Math.Pow(x, 2) + b * x + c;
        }
    }
}
