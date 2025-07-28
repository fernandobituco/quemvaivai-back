using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Infrastructure.Contexts
{
    public class DapperQueryContext
    {
        public string Schema { get; }
        public string TablePrefix { get; }

        public DapperQueryContext(IConfiguration configuration)
        {
            Schema = configuration["Database:Schema"] ?? "public";
            TablePrefix = configuration["Database:TablePrefix"] ?? string.Empty;
        }

        public string Table(string tableName)
        {
            return $"{Schema}.\"{TablePrefix}{tableName}\"";
        }
    }
}
