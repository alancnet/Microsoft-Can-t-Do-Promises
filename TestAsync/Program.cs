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
