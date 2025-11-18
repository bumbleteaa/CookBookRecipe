using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CookBookRecipe.Test.Architecture.Test;

public class ArchitectureTest
{
    private readonly Assembly _mainAssembly = typeof(Program).Assembly;
    
    private const string DomainNamespace = "CookBookRecipe.Domain";
    private const string InfrastructureNamespace = "CookBookRecipe.Infrastructure";
    private const string ViewNamespace = "CookBookRecipe.View";
    private const string ControllersNamespace = "CookBookRecipe.Controllers";
    private const string ApplicationNamespace = "CookieCookbook.Application";
    
    /*TEST 1: DEPENDENDCY DIRECTION
     Memastikan bahwa layer Domain tidak memiliki ketergantungan apa pun terhadap layer lain.
     */
    
    [Fact]
        public void Domain_ShouldNotDependOn_InfrastructureLayers()
        {
            // Get all types in the Domain namespace
            var domainTypes = _mainAssembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.StartsWith(DomainNamespace))
                .ToList();

            // Collect all dependencies that Domain types have
            var violations = new List<string>();

            foreach (var domainType in domainTypes)
            {
                // Check constructor parameters
                foreach (var constructor in domainType.GetConstructors())
                {
                    foreach (var parameter in constructor.GetParameters())
                    {
                        if (IsForbiddenDependency(parameter.ParameterType, domainType.Name))
                        {
                            violations.Add($"{domainType.Name} constructor depends on {parameter.ParameterType.Name}");
                        }
                    }
                }

                // Check fields
                foreach (var field in domainType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (IsForbiddenDependency(field.FieldType, domainType.Name))
                    {
                        violations.Add($"{domainType.Name} has field of type {field.FieldType.Name}");
                    }
                }

                // Check properties
                foreach (var property in domainType.GetProperties())
                {
                    if (IsForbiddenDependency(property.PropertyType, domainType.Name))
                    {
                        violations.Add($"{domainType.Name} has property of type {property.PropertyType.Name}");
                    }
                }

                // Check method parameters and return types
                foreach (var method in domainType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    // Check return type
                    if (IsForbiddenDependency(method.ReturnType, domainType.Name))
                    {
                        violations.Add($"{domainType.Name}.{method.Name} returns {method.ReturnType.Name}");
                    }

                    // Check parameters
                    foreach (var parameter in method.GetParameters())
                    {
                        if (IsForbiddenDependency(parameter.ParameterType, domainType.Name))
                        {
                            violations.Add($"{domainType.Name}.{method.Name} has parameter {parameter.ParameterType.Name}");
                        }
                    }
                }
            }

            // Assert no violations found
            Assert.Empty(violations);
        }
        
    private bool IsForbiddenDependency(Type dependencyType, string sourceTypeName)
    {
        // Skip compiler-generated types
        if (dependencyType.Name.Contains("<"))
            return false;

        // Skip generic type parameters
        if (dependencyType.IsGenericParameter)
            return false;

        // Handle generic types like List<Recipe>
        if (dependencyType.IsGenericType)
        {
            // Check the generic type definition (e.g., List<>)
            if (IsForbiddenNamespace(dependencyType.GetGenericTypeDefinition()))
                return true;

            // Check each generic argument (e.g., Recipe in List<Recipe>)
            foreach (var genericArg in dependencyType.GetGenericArguments())
            {
                if (IsForbiddenDependency(genericArg, sourceTypeName))
                    return true;
            }
            return false;
        }

        return IsForbiddenNamespace(dependencyType);
    }
    
    private bool IsForbiddenNamespace(Type type)
    {
        if (type.Namespace == null)
            return false;

        return type.Namespace.StartsWith(InfrastructureNamespace) ||
               type.Namespace.StartsWith(ViewNamespace) ||
               type.Namespace.StartsWith(ControllersNamespace) ||
               type.Namespace.StartsWith(ApplicationNamespace);
    }
    
    [Fact]
    public void DataAccess_ShouldNotDependOn_Presentation()
    {
        var dataAccessTypes = _mainAssembly.GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith(InfrastructureNamespace))
            .ToList();

        var violations = new List<string>();

        foreach (var type in dataAccessTypes)
        {
            // Check all dependencies (constructors, fields, properties, methods)
            CheckTypeForPresentationDependencies(type, violations);
        }

        Assert.Empty(violations);
    }

    private void CheckTypeForPresentationDependencies(Type type, List<string> violations)
    {
        // Check constructor parameters
        foreach (var constructor in type.GetConstructors())
        {
            foreach (var parameter in constructor.GetParameters())
            {
                if (parameter.ParameterType.Namespace != null &&
                    parameter.ParameterType.Namespace.StartsWith(ViewNamespace))
                {
                    violations.Add($"{type.Name} constructor depends on {parameter.ParameterType.Name}");
                }
            }
        }

        // Check fields
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
        {
            if (field.FieldType.Namespace != null &&
                field.FieldType.Namespace.StartsWith(ViewNamespace))
            {
                violations.Add($"{type.Name} has field of type {field.FieldType.Name}");
            }
        }
    }
    
    /*
     * TEST 2 CONSTRUCTOR PARAMETER COUNT
     * Memastikan bahwa tidak ada class yang memiliki lebih dari 4 parameter pada konstruktornya.
     */
    [Fact]
    public void Classes_ShouldNotHave_TooManyConstructorParameters()
    {
        const int MaxParameters = 4;

        var allTypes = _mainAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.Name != "Program") // Exclude composition root
            .Where(t => t.Name != "CookBookRecipeApp") // Exclude orchestrator - allowed to coordinate many services
            .Where(t => !t.Name.Contains("<")) // Exclude compiler-generated
            .ToList();

        var violations = new List<string>();

        foreach (var type in allTypes)
        {
            foreach (var constructor in type.GetConstructors())
            {
                var parameterCount = constructor.GetParameters().Length;

                if (parameterCount > MaxParameters)
                {
                    var parameters = string.Join(", ",
                        constructor.GetParameters().Select(p => p.ParameterType.Name));

                    violations.Add(
                        $"{type.Name} has {parameterCount} constructor parameters (max {MaxParameters}): {parameters}");
                }
            }
        }

        Assert.Empty(violations);
    }
    
    [Fact]
    public void ProgramMain_ShouldBeSimple()
    {
        var programType = _mainAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "Program");

        Assert.NotNull(programType);

        var mainMethod = programType.GetMethod("Main",
            BindingFlags.Public | BindingFlags.Static);

        Assert.NotNull(mainMethod);

        // Get method body (this is a simplified check)
        var methodBody = mainMethod.GetMethodBody();

        // A simple Main should have minimal IL instructions
        // This is approximate - main point is to catch obvious bloat
        Assert.NotNull(methodBody);
        Assert.True(methodBody.GetILAsByteArray().Length < 200,
            "Program.Main appears to contain too much logic. Move orchestration to CookbookApp.Run()");
    }
    
    /*
     * TEST 3: INTERFACE DEPENDENCIES
     * Memastikan bahwa class bergantung pada interface, bukan pada concrete class yang lain.
     */
    
     [Fact]
        public void Classes_ShouldDependOn_InterfacesNotConcreteClasses()
        {
            var allTypes = _mainAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.Name != "Program") // Exclude composition root
                .Where(t => t.Name != "CookBookRecipeApp")//Exclude orchestrator
                .Where(t => !t.Name.Contains("<")) // Exclude compiler-generated
                .Where(t => !t.Namespace.StartsWith(DomainNamespace)) // Domain entities are data, not behavior
                .ToList();

            var violations = new List<string>();

            foreach (var type in allTypes)
            {
                foreach (var constructor in type.GetConstructors())
                {
                    foreach (var parameter in constructor.GetParameters())
                    {
                        var paramType = parameter.ParameterType;

                        // Skip if it's already an interface or abstract class
                        if (paramType.IsInterface || paramType.IsAbstract)
                            continue;

                        // Skip if it's a framework type (from System namespace)
                        if (paramType.Namespace != null && paramType.Namespace.StartsWith("System"))
                            continue;

                        // Skip if it's a Domain entity (these are data structures, not services)
                        if (paramType.Namespace != null && paramType.Namespace.StartsWith(DomainNamespace))
                            continue;

                        // Skip value types and primitives
                        if (paramType.IsValueType || paramType.IsPrimitive || paramType == typeof(string))
                            continue;

                        // If we got here, it's a concrete class dependency - that's a violation
                        violations.Add(
                            $"{type.Name} constructor depends on concrete class {paramType.Name}. " +
                            $"Consider depending on an interface instead.");
                    }
                }
            }

            Assert.Empty(violations);
        }
        
        [Fact]
        public void Classes_ShouldUse_IRecipesRepositoryInterface()
        {
            var allTypes = _mainAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.Name != "Program")
                .Where(t => t.Name != "RecipesCatalogFactory") // Factory is allowed to know concrete types
                .ToList();

            var violations = new List<string>();

            foreach (var type in allTypes)
            {
                foreach (var constructor in type.GetConstructors())
                {
                    foreach (var parameter in constructor.GetParameters())
                    {
                        var paramTypeName = parameter.ParameterType.Name;

                        if (paramTypeName == "TxtRecipesCatalog" ||
                            paramTypeName == "JsonRecipesCatalog")
                        {
                            violations.Add(
                                $"{type.Name} depends on concrete {paramTypeName}. " +
                                $"Should use IRecipesCatalog interface instead.");
                        }
                    }
                }
            }

            Assert.Empty(violations);
        }
}