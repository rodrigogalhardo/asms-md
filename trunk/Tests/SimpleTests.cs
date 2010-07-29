﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Ciloci.Flee;
using ILCalc;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void Test()
        {
            Console.WriteLine(DateTime.Now.Date.ToShortDateString());
        }

        [Test]
        public void TestTrim()
        {
            Console.WriteLine("he,llo,".TrimEnd(new[] { ',' }));
        }

        [Test]
        public void Dynamic()
        {
            dynamic x = 3;
            x = x + x;
            TestingTools.IsEqualTo(x, 6);
        }

        [Test]
        public void Enumint()
        {
            Assert.IsTrue((int)Haiza.Hasfields == 2);
        }

        [Test]
        public void Convert()
        {
            double r;
            Console.WriteLine(double.TryParse(null, out r));
            Console.WriteLine(r);

        }

        [Test]
        public void Calc2()
        {
            var calc = new CalcContext<decimal>();
            try
            {
                Console.WriteLine(calc.Evaluate("32/0"));
            }
            catch (DivideByZeroException)
            {

            }
        }

        [Test]
        public void Reg()
        {
            var re = new Regex(@"suma\(i\d+\)");
            var ss = "suma(i2) suma(i2) suma suma(i123) suma(asfda) suma(i) suma(i1+1) suma( i1 )".Replace(" ", "");
            var mc = re.Matches(ss);

            foreach (Match mt in mc)
            {
                ss = ss.Replace(mt.Value, " 1 ");
                Console.WriteLine(mt.ToString());
            }
            Console.WriteLine(ss);
        }

        [Test]
        public void CalcMethod()
        {
            var calc = new CalcContext<decimal>();
            calc.Constants.Add("i2", 23);
            calc.Functions.Add("sumai", o => o);
            calc.Culture = CultureInfo.InvariantCulture;

            Console.WriteLine(calc.Evaluate("sumai(i2)"));
        }

        [Test]
        public void Calc()
        {
            var w = new Stopwatch();
            w.Start();
            var calc = new CalcContext<decimal>();
            calc.Constants.Add("j", 5);
            calc.Constants.Add("j12", 2);
            calc.Constants.Add("j1", 3);
            Console.WriteLine(calc.Evaluate("j"));
            Console.WriteLine(calc.Evaluate("j+j12"));
            Console.WriteLine(calc.Evaluate("j+j1/2+j"));
            w.Stop();
            Console.WriteLine(w.Elapsed);
        }

        [Test]
        public void Ciloci()
        {
            var w = new Stopwatch();
            w.Start();
            var context = new ExpressionContext();

            // Define an int variable
            context.Variables["a"] = 5;
            context.Variables["a1"] = 2;
            context.Variables["a12"] = 3;

            Console.WriteLine(context.CompileGeneric<decimal>("a+a1+a12").Evaluate());
            w.Stop();
            Console.WriteLine(w.Elapsed);
        }
    }

    public enum Haiza
    {
        Registered = 1,
        Hasfields,
        Hascoefficients,
    }
}