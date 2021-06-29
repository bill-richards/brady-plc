# generator-report-compiler Development Steps

This document outlines the steps taken to develop the solution.

- Create Git repository containing [README.md](../README.md), [.gitignore](../.gitignore), and [xml-docs](../xml-docs/)
- Create [solution file](../brady-plc.sln)

```crt
dotnet new sln -n brady-plc
```

- Create [project](./generator-report-compiler.csproj)

```crt
dotnet new console -n generator-report-compiler -f netcoreapp3.1 -lang C#
```

- Create [test project](../generator-report-compiler-tests/generator-report-compiler-tests.csproj)

```crt
dotnet new nunit -n generator-report-compiler-tests -f netcoreapp3.1 -lang C#
```

- Add projects to solution and set `RootNamespace` in both
- Create objects to model expected input ([01-Basic.xml](../xml-docs/01-Basic.xml))
- Create tests to ensure that deserialization of the object model works
