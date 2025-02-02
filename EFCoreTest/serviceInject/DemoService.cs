namespace DotNetTry.serviceInject
{
    internal class DemoService : IDemoService
    {

        public DemoService(IConfiguration config)
        {
            Name = "Demo Name";
        }
        public string Name { get; set; }


        public string Description()
        {
            Console.WriteLine($"Name ->{Name}");
            return Name;
        }

        public void GetXyz(string xyz)
        {
            Console.WriteLine(xyz);
        }
    }
}
