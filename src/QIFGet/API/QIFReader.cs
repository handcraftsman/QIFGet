using System.Collections.Generic;
using System.Linq;

using QIFGet.API.Domain;
using QIFGet.Extensions;

namespace QIFGet.API
{
    public interface IQIFReader
    {
        IEnumerable<Account> ReadFrom(IEnumerable<string> qifData);
    }

    public class QIFReader : IQIFReader
    {
        public IEnumerable<Account> ReadFrom(IEnumerable<string> qifData)
        {
            var accounts = qifData
                .Select(x => x.ConvertToRecord())
                .CombineIntoTransactions()
                .ConvertToEntries()
                .CombineIntoAccounts();
            return accounts;
        }
    }
}