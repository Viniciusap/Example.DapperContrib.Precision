using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Example.DapperContrib.App.Model
{
    [Table("TestPrecision")]
    public class TestPrecision
    {
        public TestPrecision()
        {
            Dapper.SqlMapper.AddTypeMap(typeof(string), System.Data.DbType.AnsiString);
            Dapper.SqlMapper.AddTypeMap(typeof(DateTime), System.Data.DbType.DateTime);
        }

        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public DateTime Date { 
            get { 
                return DateTime.Now; 
            } 
        }
        public bool Bool { get; set; }        
        [StringLength(10)]
        public string Description2 { get; set; }
        public decimal Decimal { get; set; }
    }

    [Table("TestPrecision")]
    public class TestNoPrecision
    {
        public TestNoPrecision()
        {

        }

        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date
        {
            get
            {
                return DateTime.Now;
            }
        }
        public bool Bool { get; set; }
        public string Description2 { get; set; }
        public decimal Decimal { get; set; }
    }
}
