using System;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    public static class TestingTools
    {
        public static void ShouldEqual(this object o, object to)
        {
            Assert.AreEqual(to, o);
        }

        public static void IsTrue(this bool o)
        {
            Assert.IsTrue(o);
        }

        public static User AsUser(this string name)
        {
            return new User()
                       {
                           Name = name,
                           Password = name + "pass",
                           Roles = new[]
                                       {
                                           new Role(){Id = 1},
                                           new Role(){Id = 2},
                                       }
                       };
        }

        public static void IsNotNull(this object o)
        {
            Assert.IsNotNull(o);
        }

        public static void IsNull(this object o)
        {
            Assert.IsNull(o);
        }

        public static ViewResult ShouldBeViewResult(this ActionResult o)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(ViewResult));
            return o as ViewResult;
        }

        public static ViewResult ShouldBeCreate(this ViewResult o)
        {
            Assert.AreEqual("create", o.ViewName);
            return o;
        }

        public static void ShouldBeContentOk(this ActionResult o)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(ContentResult));
            Assert.IsTrue((o as ContentResult).Content == "ok");
        }

        public static ContentResult ShouldBeContent(this ActionResult o)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(ContentResult));
            return o as ContentResult;
        }

        public static void ShouldRedirectToAction(this ActionResult o, string action)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(RedirectToRouteResult));
            Assert.AreEqual(action, (o as RedirectToRouteResult).RouteValues["action"].ToString());
        }
    }
}