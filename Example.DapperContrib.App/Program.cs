using Dapper;
using Dapper.Contrib.Extensions;
using Example.DapperContrib.App.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Example.DapperContrib.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperContribPrecision;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            TestPrecision[] testPrecisions = new TestPrecision[500];
            for (int i = 0; i < 500; i++)
            {
                testPrecisions[i] = new TestPrecision();
                testPrecisions[i].Description = "Mussum Ipsum, cacilds vidis litro abertis. Mauris";
                testPrecisions[i].Bool = true;
                testPrecisions[i].Decimal = 1818181;
                testPrecisions[i].Description2 = "Mussum Ips";
            }


            TestNoPrecision[] testNoPrecisions = new TestNoPrecision[500];
            for (int i = 0; i < 500; i++)
            {
                testNoPrecisions[i] = new TestNoPrecision();
                testNoPrecisions[i].Description = "Mussum Ipsum, cacilds vidis litro abertis. Mauris";
                testNoPrecisions[i].Bool = true;
                testNoPrecisions[i].Decimal = 1818181;
                testNoPrecisions[i].Description2 = "Mussum Ips";
            }

            var noPrecisionHeat = new TestNoPrecision();
            noPrecisionHeat.Description = "Mussum Ipsum, cacilds vidis litro abertis. Mauris";
            noPrecisionHeat.Bool = true;
            noPrecisionHeat.Decimal = 1818181;
            noPrecisionHeat.Description2 = "Mussum Ips";


            //Aquecendo a consulta
            Console.WriteLine("'Aquecendo' o banco de dados");
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                var d3 = DateTime.Now.ToString("O");
                connection.Insert<TestNoPrecision[]>(testNoPrecisions);
                var d4 = DateTime.Now.ToString("O");
                var dd2 = DateTime.Parse(d4) - DateTime.Parse(d3);
                Console.WriteLine(dd2);
                await Task.Delay(5000);
            }

            Console.WriteLine("Teste sem precisão");
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                var d3 = DateTime.Now.ToString("O");
                connection.Insert<TestNoPrecision[]>(testNoPrecisions);
                var d4 = DateTime.Now.ToString("O");
                var dd2 = DateTime.Parse(d4) - DateTime.Parse(d3);

                Console.WriteLine(dd2);

                await Task.Delay(5000);                
            }

            Console.WriteLine("Teste com precisão na Declaração antes do Insert");
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                Dapper.SqlMapper.AddTypeMap(typeof(string), System.Data.DbType.AnsiString);
                Dapper.SqlMapper.AddTypeMap(typeof(DateTime), System.Data.DbType.DateTime);
                var d3 = DateTime.Now.ToString("O");
                connection.Insert<TestNoPrecision[]>(testNoPrecisions);
                var d4 = DateTime.Now.ToString("O");
                var dd2 = DateTime.Parse(d4) - DateTime.Parse(d3);

                Console.WriteLine(dd2);

                await Task.Delay(5000);
            }

            Console.WriteLine("Teste com precisão em Anotações");
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                var d1 = DateTime.Now.ToString("O");
                connection.Insert<TestPrecision[]>(testPrecisions);
                var d2 = DateTime.Now.ToString("O");
                var dd1 = DateTime.Parse(d2) - DateTime.Parse(d1);

                Console.WriteLine(dd1);

                await Task.Delay(5000);
            }

            Console.WriteLine("Teste com precisão em Parâmetros e SQL escrito");
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                var sql = @"Insert into TestPrecision VALUES (@Description,@Date, @Bool, @Description2, @Decimal)";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Description", "Mussum Ipsum, cacilds vidis litro abertis. Mauris", System.Data.DbType.AnsiString, size: 50);
                parameter.Add("@Date", DateTime.Now, System.Data.DbType.DateTime);
                parameter.Add("@Bool", true, System.Data.DbType.Boolean);
                parameter.Add("@Description2", "Mussum Ips", System.Data.DbType.AnsiString, size: 10);
                parameter.Add("@Decimal", 0, System.Data.DbType.Decimal);


                var d1 = DateTime.Now.ToString("O");
                for (int i = 0; i < 500; i++)
                {
                    connection.Execute(sql, parameter, commandType: CommandType.Text);
                }
                var d2 = DateTime.Now.ToString("O");
                var dd1 = DateTime.Parse(d2) - DateTime.Parse(d1);


                Console.WriteLine(dd1);

                await Task.Delay(5000);
            }

            Console.ReadLine();

        }
    }
}
