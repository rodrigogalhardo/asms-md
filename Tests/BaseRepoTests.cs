﻿using System.Transactions;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    public class BaseRepoTests
    {
        private TransactionScope scope;

        [SetUp]
        public virtual void SetUp()
        {
            scope = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        [TearDown]
        public virtual void TearDown()
        {
            scope.Dispose();
        }

    }
}