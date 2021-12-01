﻿using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using static FluentAssertions.FluentActions;

namespace ObjectPrinting.Tests
{
    [TestFixture]
    public class ConfigTests
    {
        [Test]
        public void ShouldThrowInvalidOperationException_WhenIgnoreAfterIgnore()
        {
            Invoking(() => Config<Person>.CreateConfig().Ignore().Type<int>().And.Type<int>().Configure())
                .Should().Throw<InvalidOperationException>();
            
            Invoking(() => Config<Person>.CreateConfig().Ignore().Property(x => x.Age).And.Property(x => x.Age).Configure())
                .Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void ShouldThrowInvalidOperationException_WhenSetPrintingAndIgnore()
        {
            Invoking(() => Config<Person>.CreateConfig().Ignore().Type<int>().SetAlternativeSerialisation().For<int>().WithMethod(x => 1 + x).Configure())
                .Should().Throw<InvalidOperationException>();
            Invoking(() => Config<Person>.CreateConfig().Ignore().Property(x => x.Age).SetAlternativeSerialisation().For(x => x.Age).WithMethod(x => 1 + x).Configure())
                .Should().Throw<InvalidOperationException>();
            Invoking(() => Config<Person>.CreateConfig().Ignore().Type<int>().SetAlternativeSerialisation().For(x => x.Age).WithMethod(x => 1 + x).Configure())
                .Should().Throw<InvalidOperationException>();
            
            Invoking(() => Config<Person>.CreateConfig().SetAlternativeSerialisation().For<int>().WithMethod(x => (1 - x).ToString()).Ignore().Type<int>().Configure())
                .Should().Throw<InvalidOperationException>();
            Invoking(() => Config<Person>.CreateConfig().SetAlternativeSerialisation().For(x => x.Age).WithMethod(x => (1 - x).ToString()).Ignore().Property(x => x.Age).Configure())
                .Should().Throw<InvalidOperationException>();
            Invoking(() => Config<Person>.CreateConfig().SetAlternativeSerialisation().For(x => x.Age).WithMethod(x => (1 - x).ToString()).Ignore().Type<int>().Configure())
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void ShouldThrowInvalidOperationException_WhenIgnoreAndSettingCulture()
        {
            Invoking(() => Config<Person>.CreateConfig().SetCulture().For<int>(CultureInfo.CurrentCulture).SetCulture().For<int>(CultureInfo.CurrentCulture).Configure())
                .Should().Throw<InvalidOperationException>();
            Invoking(() => Config<Person>.CreateConfig().SetCulture().For<int>(CultureInfo.CurrentCulture).Ignore().Type<int>().Configure())
                .Should().Throw<InvalidOperationException>();
            Invoking(() => Config<Person>.CreateConfig().SetCulture().For<int>(CultureInfo.CurrentCulture).Ignore().Property(x => x.Age).Configure())
                .Should().Throw<InvalidOperationException>();
            
            Invoking(() => Config<Person>.CreateConfig().Ignore().Type<int>().SetCulture().For<int>(CultureInfo.CurrentCulture).Configure())
                .Should().Throw<InvalidOperationException>();
        }
    }
}