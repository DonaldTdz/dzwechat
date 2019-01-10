using Abp.Data;
using Abp.EntityFrameworkCore;
using HC.DZWechat.IntegralDetails;
using HC.DZWechat.IntegralDetails.DomainService;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.EntityFrameworkCore.Repositories
{
    public class IntegralDetailsRepository : DZWechatRepositoryBase<IntegralDetail, Guid>, IIntegralDetailsRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;
        public IntegralDetailsRepository(IDbContextProvider<DZWechatDbContext> dbContextProvider
            , IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }
        private void EnsureConnectionOpen()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
                 {
                    {"ContextType", typeof(DZWechatDbContext) },
                    {"MultiTenancySide", MultiTenancySide }
                 });
        }

        /// <summary>
        /// 获取积分变化统计（按月）
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<IntegralDetailDto>> GetIntegralDetailByMonthAsync(DateTime startTime, DateTime endTime)
        {
            EnsureConnectionOpen();
            MySqlParameter[] sql = new MySqlParameter[]
            {
                new MySqlParameter("@startTime",startTime),
                new MySqlParameter("@endTime",endTime),
            };
            var command = CreateCommand(@"select CONCAT(t1.Yearin,'-',if(t1.Monthin>9,t1.Monthin,CONCAT('0',t1.Monthin))) Months,
                     IFNULL(t1.GrowIntegral,0) GrowIntegral,IFNULL(t2.DepleteIntegral,0) DepleteIntegral
                     from(
                     select Yearin,Monthin, SUM(Integral) as GrowIntegral
                     from(
                    select year(i.CreationTime) as Yearin, month(i.CreationTime) as Monthin, i.Integral
                    from integraldetails i where i.CreationTime>=@startTime and i.CreationTime < @endTime and i.Integral>0
                    ) temp group by Yearin, Monthin
                    ) t1 left join(
                    select Yearin,Monthin, SUM(Integral) as DepleteIntegral
                    from(
                   select year(i.CreationTime) as Yearin, month(i.CreationTime) as Monthin, i.Integral
                   from integraldetails i where i.CreationTime>=@startTime and i.CreationTime < @endTime and i.Integral<0
                    ) temp group by Yearin, Monthin) t2 on t1.Yearin = t2.Yearin and t1.Monthin = t2.Monthin
                   ", CommandType.Text, sql);
            using (command)
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<IntegralDetailDto>();
                    while (dataReader.Read())
                    {
                        var integral = new IntegralDetailDto();
                        integral.GroupName = dataReader["Months"].ToString();
                        integral.GrowIntegral = (decimal)dataReader["GrowIntegral"];
                        integral.DepleteIntegral = (decimal)dataReader["DepleteIntegral"];
                        result.Add(integral);
                    }
                    return result;
                }
            }

        }
    }
}
