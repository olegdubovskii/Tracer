using Tracer.Core;
using Tracer.Serialization;
using Tracers = Tracer.Core.Tracer;

namespace Tracer.Example;
public class Foo
{
    private Bar _bar;
    private ITracer _tracer;

    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(_tracer);
    }
    
    public void MyMethod()
    {
        _tracer.StartTrace();
        _bar.InnerMethod();
        FooMethod();
        _tracer.StopTrace();
    }

    public void FooMethod()
    {
        _tracer.StartTrace();
        Console.WriteLine("Hello from foo!");
        Thread.Sleep(10);
        _tracer.StopTrace();
    }

}

public class Bar
{
    private ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }
    
    public void InnerMethod()
    {
        _tracer.StartTrace();
        InnerestMethod();
        _tracer.StopTrace();
    }

    private void InnerestMethod()
    {
        _tracer.StartTrace();
        Console.WriteLine("Hello from Bar!");
        _tracer.StopTrace();
    }
}

public class Th
{
    private ITracer _tracer;

    public Th(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void A()
    {
        Method1();
        Method2();
    }

    private void Method1()
    {
        _tracer.StartTrace();
        Thread.Sleep(100);
        _tracer.StopTrace();
    }

    private void Method2()
    {
        _tracer.StartTrace();
        Thread.Sleep(200);
        _tracer.StopTrace();
    }
}

public class MainClass
{
    public void Main()
    {
        var tracer = new Tracers();

        var temp = new Foo(tracer);
        Thread thread = new(new ThreadStart(temp.MyMethod));
        thread.Start();
        var th = new Th(tracer);
        th.A();
        var smth = tracer.GetTraceResult();
        
        var serializator = new Serializator();
        serializator.SerializeXml(smth);
        serializator.SerializeJson(smth);
        serializator.SerializeYaml(smth);
    }
}