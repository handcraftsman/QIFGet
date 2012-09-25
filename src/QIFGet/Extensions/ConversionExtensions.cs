// * **************************************************************************
// * Copyright (c) Clinton Sheppard <sheppard@cs.unm.edu>
// *
// * This source code is subject to terms and conditions of the MIT License.
// * A copy of the license can be found in the License.txt file
// * at the root of this distribution.
// * By using this source code in any fashion, you are agreeing to be bound by
// * the terms of the MIT License.
// * You must not remove this notice from this software.
// *
// * source repository: https://github.com/handcraftsman/QIFGet
// * **************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

using QIFGet.API.Domain;
using QIFGet.Domain;
using QIFGet.Domain.NamedConstants;

namespace QIFGet.Extensions
{
    public static class ConversionExtensions
    {
        public static IEnumerable<Account> CombineIntoAccounts(this IEnumerable<Entry> entries)
        {
            return entries
                .Group((current, previous) => !current.IsAccountHeader || ReferenceEquals(current, previous))
                .Select(accountEntries => new Account(entries.ToList()));
        }

        public static IEnumerable<QIFTransaction> CombineIntoTransactions(this IEnumerable<QIFRecord> records)
        {
            return records
                .Group((current, previous) => previous.RecordType != QIFRecordType.TransactionEnd)
//                .Group((current, previous) => previous.RecordType == QIFRecordType.Content || current.RecordType.IsHeader)
                .Select(transactionRecords => new QIFTransaction(transactionRecords.ToList()));
        }

        public static IEnumerable<Entry> ConvertToEntries(this IEnumerable<QIFTransaction> transactions)
        {
            var contentTypes = QIFContentType.GetAll().ToList();
            foreach (var transaction in transactions)
            {
                var entry = new Entry
                    {
                        IsHeader = transaction.Records.Any(x => x.RecordType.IsHeader),
                        IsAccountHeader = transaction.Records.Any(x => x.RecordType == QIFRecordType.AccountHeader)
                    };

                foreach (var record in transaction.Records.Where(x => x.RecordType == QIFRecordType.Content))
                {
                    var record1 = record;
                    var contentType = contentTypes.FirstOrDefault(x => x.IsMatch(entry, record1));
                    if (contentType == null)
                    {
                        throw new ArgumentException("Don't know how to handle: " + record1.Data);
                    }
                    try
                    {
                        contentType.Update(entry, record1);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Unable to convert the following to a value on Entry: " + record1.Data, e);
                    }
                }
                yield return entry;
            }
        }

        public static QIFRecord ConvertToRecord(this string qiftext)
        {
            var recordType = QIFRecordType
                .GetAll()
                .First(x => x.IsMatch(qiftext));
            return new QIFRecord(recordType, recordType.GetData(qiftext));
        }

        public static DateTime GetDate(this string date)
        {
            var dateParts = date.Split(new[] { '/', '\'' }).Select(x => int.Parse(x)).ToArray();
            if (dateParts[2] < 100)
            {
                // y2k
                if (dateParts[2] > 70)
                {
                    dateParts[2] += 1900;
                }
                else
                {
                    dateParts[2] += 2000;
                }
            }
            return new DateTime(dateParts[2], dateParts[0], dateParts[1]);
        }
    }
}