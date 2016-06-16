using System;
using Exformatics.Engine;

class Test { 
    static public void Show(Process P) {
        Console.WriteLine("    Enabled: {0}", String.Join(", ", P.enabled()));
        Console.WriteLine("    Accepting: {0}", P.isAccepting());
    }

    static public void Execute(Process P, String e) {
        Console.WriteLine(". Executing {0}", e);
        P.execute(e);
        Show(P);
    }

    static public void Header(Process P, String title) {
        Console.WriteLine("* New graph: {0}", title);
        Show(P);
    }

    static public void Main()
    {
        Console.WriteLine("DCR Process Test");

        // Construct the process A -->* B
        var P = new Process();
        P.addEvent("A");
        P.addEvent("B");
        P.addRelation("A", Arrow.Condition, "B");

        // Try it out. 
        Header(P, "A -->* B");
        Execute(P, "A");

        // Construct the process A *--> B
        P = new Process();
        P.addEvent("A");
        P.addEvent("B");
        P.addRelation("A", Arrow.Response, "B");

        // Try it out.
        Header(P, "A *--> B");
        Execute(P, "A");
        Execute(P, "B");

        // Construct the process A -->% B -->* C
        P = new Process();
        P.addEvent("A");
        P.addEvent("B");
        P.addEvent("C");
        P.addRelation("A", Arrow.Exclude, "B");
        P.addRelation("B", Arrow.Condition, "C");

        Header(P, "A -->% B -->* C");
        Execute(P, "A");
        
        // Construct the process A -->% !B --<> C
        P = new Process();
        P.addEvent("A");
        P.addEvent("B");
        P.pending.Add("B");
        P.addEvent("C");
        P.addRelation("A", Arrow.Exclude, "B");
        P.addRelation("B", Arrow.Milestone, "C");

        Header(P, "A -->% !B --<> C");
        Execute(P, "A");

        // Construct the process A -->+ %B
        P = new Process();
        P.addEvent("A");
        P.addEvent("B");
        P.addRelation("A", Arrow.Include, "B");
        P.included.Remove("B");

        Header(P, "A -->+ %B");
        Execute(P, "A");
    }
}
