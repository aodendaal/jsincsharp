using System;
using Microsoft.JScript;
using System.CodeDom.Compiler;

namespace JScriptHostingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string script = @"package blahPackage {
                                class blahClass {
                                    function add(a, b) {
                                        return a + b;
                                    }
                                }
                            }";

            using (var provider = new JScriptCodeProvider())
            {
                var results = provider.CompileAssemblyFromSource(new CompilerParameters(), script);
                var assembly = results.CompiledAssembly;

                var myblahtype = assembly.GetType("blahPackage.blahClass");
                var myblah = Activator.CreateInstance(myblahtype);

                var result = myblahtype.InvokeMember("add", System.Reflection.BindingFlags.InvokeMethod, null, myblah, new object[] { 2, 3 });

                Console.WriteLine("Output : " + result.ToString());

                Console.ReadLine();
            }
        }
    }
}
