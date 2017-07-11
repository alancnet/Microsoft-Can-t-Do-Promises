# Microsoft Can't Do Promises

Okay, this is just a bit of a rant, because Microsoft seems to adopt every programming paradigm out there, but just rename it.
`Map` is `Select` in Linq, `console.log` is `Console.WriteLine`, etc etc. In this case, `asynchronous functions` are called 
`Tasks`. How do they compare? Poorly. In JavaScript, you can create a promise, or async function, and it will execute once.
You can bind to the result as much as you like, and you'll always get the same result. With tasks, not so much. You can only
bind to it once, and hopefully you do it before it's complete.

```c#
using System;
using System.Threading.Tasks;

namespace TestAsync
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var r = SayHello();
            r.ContinueWith((str) => {
                Console.WriteLine("This prints: {0}", str.Result);
                r.ContinueWith((str2) => {
                    Console.WriteLine("This does not print: {0}", str2.Result);
                });
            });
        }
        static Task<string> SayHi() {
            var ret = new TaskCompletionSource<string>();
            ret.SetResult("Hello World");
            return ret.Task;
        }
        static async Task<string> SayHello() {
            return await SayHi();
        }
    }
}
```

Why doesn't it print twice?!

Oh, oh! Then there's this lovely error message:

`RunSynchronously may not be called on a task that has already completed.`

WHY? Why, oh great and glorious Microsoft, should my program crash if I attempt this simple operation? Why not just return
immediately if the task is already complete? Do you want me to check `.IsCompleted` first, then call `RunSynchronously`?
_Can you say "Race condition?"_

Okay.. Rant over. No one will ever see this. ☠

-alancnet
