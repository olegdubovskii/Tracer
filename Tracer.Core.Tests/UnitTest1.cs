namespace Tracer.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TwoMethods()
        {
            ITracer tracer = new Tracer();
            Foo foo = new(tracer);
            Test test = new(tracer);
            foo.TestMethod();
            test.TestMethod();
            var _compl = tracer.GetTraceResult().Threads.ElementAt(0);
            Assert.AreEqual(2, tracer.GetTraceResult().Threads.ElementAt(0).methods.Count());
        }

        [TestMethod]
        public void TwoThreads()
        {
            ITracer tracer = new Tracer();
            Foo foo = new(tracer);
            Test test = new(tracer);
            Thread thread = new Thread(new ThreadStart(foo.TestMethod));
            thread.Start();
            test.TestMethod();
            Assert.AreEqual(2, tracer.GetTraceResult().Threads.Count);
        }

        [TestMethod]
        public void TestWithInnerMethods()
        {
            ITracer tracer = new Tracer();
            Foo foo = new(tracer);
            Inner inner = new(tracer);
            foo.TestMethod();
            inner.TestMethod();
            var temp = tracer.GetTraceResult().Threads.ElementAt(0).methods;
            Assert.AreEqual(3, temp.ElementAt(0).InnerMethods.Count+temp.ElementAt(1).InnerMethods.Count+temp.Count);
        }


        public class Foo
        {
            private ITracer _tracer;

            public Foo(ITracer tracer)
            {
                _tracer = tracer;
            }

            public void TestMethod()
            {
                _tracer.StartTrace();
                _tracer.StopTrace();
            }
        }
        public class Test
        {
            private ITracer _tracer;

            public Test(ITracer tracer)
            {
                _tracer = tracer;
            }

            public void TestMethod()
            {
                _tracer.StartTrace();
                _tracer.StopTrace();
            }
        }

        public class Inner
        {
            private ITracer _tracer;
            private Test test1;
            public Inner(ITracer tracer)
            {
                _tracer = tracer;
                test1 = new Test(tracer);  
            }

            public void TestMethod()
            {
                _tracer.StartTrace();
                test1.TestMethod();
                _tracer.StopTrace();
            }
        }
    }
}