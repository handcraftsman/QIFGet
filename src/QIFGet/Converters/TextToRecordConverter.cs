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

using System.Linq;

using QIFGet.Domain;
using QIFGet.NamedConstants;

namespace QIFGet.Converters
{
    public class TextToRecordConverter
    {
        public QIFRecord Convert(string qiftext)
        {
            var recordType = QIFRecordType
                .GetAll()
                .First(x => x.IsMatch(qiftext));
            return new QIFRecord(recordType, recordType.GetData(qiftext));
        }
    }
}