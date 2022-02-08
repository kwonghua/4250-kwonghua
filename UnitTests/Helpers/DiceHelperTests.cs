﻿using System;
using System.Collections.Generic;
using System.Text;
using Mine.Helpers;
using NUnit.Framework;
using Mine.Models;

namespace UnitTests.Helpers
{   
    [TestFixture]
    public class DiceHelperTests
    {
        [Test]
        public void RollDice_Invalid_Roll_Zero_Should_Return_Zero()
        {
            // Arrange

            // Act
            var result = DiceHelper.RollDice(0, 1);

            // Reset

            // Assert
            Assert.AreEqual(0, result);

        }

    }
}
