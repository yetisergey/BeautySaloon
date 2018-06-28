namespace Initializer
{
    using Data;
    using System;
    using System.Data.Entity;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi! Select an action:");

            var menu = new[]
                           {
                               new Tuple<string, Action>("Update DataBase", UpdateDatabase)
                           };

            for (var i = 0; i < menu.Length; i++)
            {
                var option = menu[i];
                Console.WriteLine("{0} - {1}", i, option.Item1);
            }

            Console.WriteLine("Select (0,1,2):");

            var options = (Console.ReadLine() ?? string.Empty).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var option in options)
            {
                int optionIndex;

                if (int.TryParse(option, out optionIndex) && optionIndex >= 0 && optionIndex < menu.Length)
                {
                    Console.WriteLine("Run \"{0}\"...", menu[optionIndex].Item1);
                    menu[optionIndex].Item2();
                }
            }

            Console.WriteLine("Done. Press Enter to exit");
            Console.ReadLine();
        }

        private static void UpdateDatabase()
        {
            var migrator = new MigrateDatabaseToLatestVersion<SaloonContext, Data.Migrations.Configuration>();
            migrator.InitializeDatabase(new SaloonContext());
            Console.WriteLine("Database is updated");
        }
    }
}