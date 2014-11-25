namespace Slyce.IntelliMerge.UnitTesting.Resources.CSharp
{
    public class Class1
    {
        public void Method()
        {
            // Method Text
            Console.WriteLine();
        }

        public interface Interface1
        {
            int this[int i] { set; get; }
            int I { set; get; }
            void Method1();
            event EventHandler<EventArgs> Event1;
        }
    }
}