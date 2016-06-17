/*
 * Example use of the DCR Process Engine (debois@exformatics.com)
 *
 *  Copyright(C) 2016 Exformatics A/S 
 *
 *  This program is free software: you can redistribute it and/or modify it
 *  under the terms of the GNU Affero General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or (at your
 *  option) any later version.
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT
 *  ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 *  FITNESS FOR A PARTICULAR PURPOSE.See the GNU Affero General Public License
 *  for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see<http://www.gnu.org/licenses/>.
 */



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
