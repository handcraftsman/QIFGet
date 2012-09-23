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

using FluentAssert;

using NUnit.Framework;

using QIFGet.Converters;
using QIFGet.NamedConstants;

namespace QIFGet.Tests.Converters
{
    public class TextToRecordConverterTests
    {
        [TestFixture]
        public class Given_text_containing_only_a_QIF_end_of_section_code
        {
            private TextToRecordConverter _textToRecordConverter;

            [SetUp]
            public void Before_each_test()
            {
                _textToRecordConverter = new TextToRecordConverter();
            }

            [Test]
            public void Should_return_a_SectionEnd_record()
            {
                const string input = "^";
                var record = _textToRecordConverter.Convert(input);
                record.RecordType.ShouldBeEqualTo(QIFRecordType.SectionEnd);
                record.Data.ShouldBeEqualTo("");
            }
        }

        [TestFixture]
        public class Given_text_starting_with_a_QIF_header_account_code
        {
            private TextToRecordConverter _textToRecordConverter;

            [SetUp]
            public void Before_each_test()
            {
                _textToRecordConverter = new TextToRecordConverter();
            }

            [Test]
            public void Should_return_an_AccountHeader_record_with_the_contents_of_the_text()
            {
                const string input = "!Account";
                var record = _textToRecordConverter.Convert(input);
                record.RecordType.ShouldBeEqualTo(QIFRecordType.AccountHeader);
                record.Data.ShouldBeEqualTo("");
            }
        }

        [TestFixture]
        public class Given_text_starting_with_a_QIF_header_option_code
        {
            private TextToRecordConverter _textToRecordConverter;

            [SetUp]
            public void Before_each_test()
            {
                _textToRecordConverter = new TextToRecordConverter();
            }

            [Test]
            public void Should_return_a_OptionHeader_record_with_the_contents_of_the_text()
            {
                const string input = "!Option:AutoSwitch";
                var record = _textToRecordConverter.Convert(input);
                record.RecordType.ShouldBeEqualTo(QIFRecordType.OptionHeader);
                record.Data.ShouldBeEqualTo("AutoSwitch");
            }
        }

        [TestFixture]
        public class Given_text_starting_with_a_QIF_header_type_code
        {
            private TextToRecordConverter _textToRecordConverter;

            [SetUp]
            public void Before_each_test()
            {
                _textToRecordConverter = new TextToRecordConverter();
            }

            [Test]
            public void Should_return_a_TypeHeader_record_with_the_contents_of_the_text()
            {
                const string input = "!Type:Cash";
                var record = _textToRecordConverter.Convert(input);
                record.RecordType.ShouldBeEqualTo(QIFRecordType.TypeHeader);
                record.Data.ShouldBeEqualTo("Cash");
            }
        }

        [TestFixture]
        public class Given_text_that_does_not_contain_a_QIF_header_or_section_end_code
        {
            private TextToRecordConverter _textToRecordConverter;

            [SetUp]
            public void Before_each_test()
            {
                _textToRecordConverter = new TextToRecordConverter();
            }

            [Test]
            public void Should_return_a_Content_record_with_the_contents_of_the_text()
            {
                const string input = "NChecking";
                var record = _textToRecordConverter.Convert(input);
                record.RecordType.ShouldBeEqualTo(QIFRecordType.Content);
                record.Data.ShouldBeEqualTo("NChecking");
            }
        }
    }
}