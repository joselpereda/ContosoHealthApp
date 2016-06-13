using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using zxdemoService.DataObjects;

namespace zxdemoService.Models
{
    public class zxdemoContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public zxdemoContext() : base(connectionStringName)
        {
        } 

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }

}
