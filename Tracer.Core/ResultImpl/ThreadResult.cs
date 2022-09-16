﻿namespace Tracer.Core;

public class ThreadResult
{
    public int Id { get; }
    //public string Time { get; }
    public int Time { get;}
    public IReadOnlyList<MethodResult> methods { get; }

    public ThreadResult() {}
    
    public ThreadResult(int id, int time, IReadOnlyList<MethodResult> methods)
    {
        Id = id;
        Time = time;
        this.methods = methods;
    }
}