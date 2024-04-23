using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace CSharpDLLCompiler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            {
                string  _filePath      = Assembly.GetExecutingAssembly().Location;
                string? _directoryPath = Path.GetDirectoryName(_filePath);
                string  _targetFolder  = Path.Combine(_directoryPath, "CompileFolder");

                DirectoryInfo d           = new DirectoryInfo(_targetFolder);
                string[]      sourceFiles = d.EnumerateFiles("*.cs", SearchOption.AllDirectories).Select(a => a.FullName).ToArray();

                List<SyntaxTree> trees = new List<SyntaxTree>();

                foreach (string file in sourceFiles)
                {
                    string     code = File.ReadAllText(file);
                    SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
                    trees.Add(tree);
                }

                string                  _assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);
                List<MetadataReference> _references   = new List<MetadataReference>();
                _references.Add(MetadataReference.CreateFromFile(Path.Combine(_assemblyPath, "System.Console.dll")));
                _references.Add(MetadataReference.CreateFromFile(Path.Combine(_assemblyPath, "System.Runtime.dll")));
                _references.Add(MetadataReference.CreateFromFile(Path.Combine(_assemblyPath, "System.dll")));
                _references.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
                _references.Add(MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location));
                _references.Add(MetadataReference.CreateFromFile(typeof(CSharpSyntaxTree).Assembly.Location));
                _references.Add(MetadataReference.CreateFromFile(typeof(Attribute).Assembly.Location));
                _references.Add(MetadataReference.CreateFromFile(typeof(Object).Assembly.Location));
                _references.Add(MetadataReference.CreateFromFile(typeof(ValueType).Assembly.Location));
                _references.Add(MetadataReference.CreateFromFile(typeof(IList<>).Assembly.Location));
                _references.Add(MetadataReference.CreateFromFile(Path.Combine(_targetFolder, "Google.Protobuf.dll")));

                
 
                CSharpCompilation compilation = CSharpCompilation.Create(
                    "qwerty.dll",
                    trees,
                    _references,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

                EmitResult result = compilation.Emit(Path.Combine(_directoryPath, "qwerty.dll"));

                if (result.Success)
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    foreach (Diagnostic _error in result.Diagnostics)
                    {
                        Console.WriteLine(_error);
                    }

                    Console.WriteLine("NO");
                }
            }

            return;

            if (args is not { Length: > 0 })
            {
                string   _filePath       = Assembly.GetExecutingAssembly().Location;
                string?  _directoryPath  = Path.GetDirectoryName(_filePath);
                string   _inDir          = Path.Combine(_directoryPath, "CompileFolder");
                string[] _files          = Directory.GetFiles(_inDir, "*.cs");
                string   _outPath        = Path.Combine(_inDir, "ProtoBuf.dll");
                string   _includeDllPath = Path.Combine(_directoryPath, "Google.Protobuf.dll");
                CompilerDll(_files, _outPath, _includeDllPath);
            }
            else
            {
                string _inDir   = string.Empty;
                string _outPath = string.Empty;

                for (int _i = 0; _i < args.Length; _i++)
                {
                    string[]? _splitArray = args[_i].Split('=');

                    if ((_splitArray == null) || (_splitArray.Length < 1))
                    {
                        Console.WriteLine("参数正确，请输入-i=./ -o=./");

                        continue;
                    }

                    if (_splitArray[0].Equals("-i"))
                    {
                        _inDir = _splitArray[1];
                    }
                    else if (_splitArray[0].Equals("-o"))
                    {
                        _outPath = _splitArray[1];
                    }
                }

                if (string.IsNullOrEmpty(_inDir))
                {
                    Console.WriteLine("输入路径为空，请检查！");

                    return;
                }

                if (string.IsNullOrEmpty(_outPath))
                {
                    Console.WriteLine("输出路径为空，请检查！");

                    return;
                }

                string[] _files          = Directory.GetFiles(_inDir, "*.cs");
                string   _filePath       = Assembly.GetExecutingAssembly().Location;
                string?  _directoryPath  = Path.GetDirectoryName(_filePath);
                string   _includeDllPath = Path.Combine(_directoryPath, "Google.Protobuf.dll");
                CompilerDll(_files, _outPath, _includeDllPath);
            }
        }

        public static void CompilerDll(string[] classPathArray, string outPath, string dllPath)
        {
            //CodeDomProvider _compiler = CodeDomProvider.CreateProvider("CSharp");

            ////设置编译参数
            //CompilerParameters _paras = new CompilerParameters();

            ////引入第三方dll
            //_paras.ReferencedAssemblies.Add("System.dll");
            //_paras.ReferencedAssemblies.Add("System.RunTime.dll");
            //_paras.ReferencedAssemblies.Add(dllPath);

            ////是否内存中生成输出
            //_paras.GenerateInMemory = false;

            ////是否生成可执行文件
            //_paras.GenerateExecutable = false;

            ////输出的Dll目录
            //_paras.OutputAssembly = outPath;

            ////编译代码
            //CompilerResults _result = _compiler.CompileAssemblyFromFile(_paras, classPathArray);

            //foreach (object? _item in _result.Errors)
            //{
            //    // 错误信息
            //    Console.WriteLine(_item.ToString());
            //}
        }
    }
}
