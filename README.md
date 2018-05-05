# Aspect Castle Facility [![Build status](https://ci.appveyor.com/api/projects/status/8h0yrxi8ry914p7p?svg=true)](https://ci.appveyor.com/project/hogaf/castle-facilities-aspect)


## Introduction
The Aspect Castle Facility uses a declarative approach to register interceptors by defining Advices and Pointcuts.


## Features
* Declarative approach for applying interceptors.
* No direct interception registration required for each type.
* No proxy object would be created for no matching pointcut.

## Examples

Consider the following example. [(inspired from AOP introduction of Castle Windsor)](https://github.com/castleproject/Windsor/blob/master/docs/orphan-introduction-to-aop-with-castle.md)
```csharp
public interface ISomething
    {
        int Augment(Int32 input);
        void DoSomething(String input);
    }

class Something : ISomething
    {
        [Log]
        public int Augment(int input)
        {
            return input + 1;
        }

        public void DoSomething(string input)
        {
            Console.WriteLine("I'm doing something: " + input);
        }
    }
```

Instead of calling for `.Interceptors()` while registering the component :

```csharp
container.Register(
		Component.For<IInterceptor>()
		.ImplementedBy<LogAdvice>()
		.Named("myinterceptor"));

	container.Register(
		Component.For<ISomething>()
		.ImplementedBy<Something>()
		.Interceptors(InterceptorReference.ForKey("myinterceptor")).Anywhere);
```

You can declaratively intercept method calls :

```csharp
    var container = new WindsorContainer();
    container.Register(Component.For<ISomething>().ImplementedBy<Something>());

    container.AddFacility<AspectFacility>(x =>

                {
                    x.AddAttributePointcut<LogAttribute, LogAdvice>();
                });

    var something = container.Resolve<ISomething>();
                
    //It's going to get intercepted because of applied Log attribute
    something.Augment(10);
```

As you can verify the actual implementation of the interceptor is in the LogAdvice class.

This example certifies a couple of things, the first is component registration has been decoupled from Aspect declaration, the other one is that once you remove the `LogAttribute`, no proxy objects would be created by Castle `DynamicProxy` and it is not required to change the component registration section anymore.

Here are the simple implementation of the LogAdvice and LogAttribute classes :

```csharp
 [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
 public sealed class LogAttribute : Attribute
 {
 }

class LogAdvice : AttributeInterceptor<LogAttribute>
    {
        protected override void InnerIntercept(IInvocation invocation)
        {
            Console.WriteLine($"interceptor called before '{invocation.Method.Name}'");
            invocation.Proceed();
            Console.WriteLine($"interceptor called after '{invocation.Method.Name}'");
        }
    }
```

## NuGet Package
* https://www.nuget.org/packages/Castle.Facilities.Aspect/ [![NuGet](https://img.shields.io/nuget/v/Castle.Facilities.Aspect.svg)](https://www.nuget.org/packages/Castle.Facilities.Aspect/)