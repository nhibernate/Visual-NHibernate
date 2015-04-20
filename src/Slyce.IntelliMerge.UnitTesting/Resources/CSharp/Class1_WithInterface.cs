namespace Slyce.IntelliMerge.UnitTesting.Resources.CSharp
{
    public class Class1
    {
        public interface Interface1
        {
            int I { get; set; }
            int this[int i] { get; set; }
            event EventHandler<EventArgs> Event1;
            void Method1();
        }

        public void Method()
        {
            // Method Text
            Console.WriteLine();
        }
    }
}