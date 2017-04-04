using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.CSharp;
using UtilityDelta.Shared.Dto;
using UtilityDelta.Shared.Interface;

namespace UtilityDelta.Client.ServiceLocator
{
    public class DynamicProxyBuilder : IDynamicProxyBuilder
    {
        public Type BuildProxy(Type classToProxy, string proxyName)
        {
            var interfaceClassFullName = classToProxy.FullName;

            //Build this assembly with the proxy class implementation of the provided interface class.
            var assemblies = GetAssemblyReferences();
            var sourceCode = BuildSourceCode(classToProxy, proxyName);

            //Compiler options
            var compilerPreferences = new CompilerParameters();
            foreach (var assemblyReference in assemblies)
            {
                compilerPreferences.ReferencedAssemblies.Add(assemblyReference);
            }
            compilerPreferences.GenerateInMemory = true;
            compilerPreferences.GenerateExecutable = false;
            compilerPreferences.IncludeDebugInformation = true;

            var cSharpProvider = new CSharpCodeProvider();
            var results = cSharpProvider.CompileAssemblyFromSource(compilerPreferences, sourceCode);

            if (results.Errors.HasErrors)
            {
                throw new Exception("Errors compiling dynamic proxy assembly for interface: " + interfaceClassFullName);
            }

            var dynamicAssembly = results.CompiledAssembly;

            //Instatiate the interface implementation class by finding it in the assembly
            return dynamicAssembly.GetType(proxyName, true, true);
        }

        private static string BuildSourceCode(Type interfaceClass, string proxyClassName)
        {
            var sourceCode = new StringBuilder();

            sourceCode.AppendLine($"public class {proxyClassName} : {interfaceClass.FullName}");
            sourceCode.AppendLine("{");

            var processor = typeof(IServiceWrapper).FullName;
            //Dependancy injected service wrapper
            sourceCode.AppendLine($"    private readonly {processor} _service;");

            sourceCode.AppendLine(
                $"    public {proxyClassName}({processor} service)");
            sourceCode.AppendLine("    {");
            sourceCode.AppendLine("        _service = service;");
            sourceCode.AppendLine("    }");

            foreach (var method in interfaceClass.GetMethods())
            {
                var fisrtParameter = method.GetParameters()[0];
                var parameterTypeAndName = TypeName(fisrtParameter.ParameterType) + " " + fisrtParameter.Name;

                sourceCode.AppendLine("    public async " + TypeName(method.ReturnType) + " " + method.Name + "(" + parameterTypeAndName + ")");
                sourceCode.AppendLine("    {");

                sourceCode.AppendLine(
                    $"        return await _service.ProcessService<{TypeName(method.ReturnType.GenericTypeArguments[0])}>(\"{interfaceClass.Assembly.GetName().Name}\", \"{interfaceClass.Name}\", \"{method.Name}\", {fisrtParameter.Name});");
                sourceCode.AppendLine("    }"); //End function
            }
            sourceCode.AppendLine("}");

            return sourceCode.ToString();
        }

        private static List<string> GetAssemblyReferences()
        {
            var result = new List<string>
            {
                typeof(Type).Assembly.Location,
                typeof(DataContractAttribute).Assembly.Location,
                typeof(ActionNotSupportedException).Assembly.Location,
                typeof(IServiceWrapper).Assembly.Location,
                typeof(DtoPerformOperation).Assembly.Location,
                typeof(ITestService).Assembly.Location
            };

            return result;
        }

        /// <summary>
        ///     Gets back the name of the type so it can be added to source code.
        ///     Supports generic types by recursively calling this function.
        /// </summary>
        /// <param name="providedType"></param>
        /// <returns></returns>
        private static string TypeName(Type providedType)
        {
            var result = new StringBuilder();

            if (providedType.IsGenericType)
            {
                result.Append(providedType.Namespace + ".");
                result.Append(providedType.Name.Substring(0, providedType.Name.IndexOf("`", StringComparison.Ordinal)));
                result.Append("<");

                var firstType = true;
                foreach (var genericType in providedType.GetGenericArguments())
                {
                    if (firstType)
                    {
                        firstType = false;
                    }
                    else
                    {
                        result.Append(", ");
                    }
                    result.Append(TypeName(genericType));
                }

                result.Append(">");
            }
            else
            {
                result.Append(providedType.FullName.Replace("+", "."));
            }

            return result.ToString();
        }
    }
}