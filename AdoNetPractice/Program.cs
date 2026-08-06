using AdoNetPractice.Data;

namespace AdoNetPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            dbConnectionFactory.ConnectToDatabase();
        }
    }
}
