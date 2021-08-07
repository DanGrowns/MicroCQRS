# TinyCqrs
A small CQRS Library based on the [CQRS In Practice](https://www.pluralsight.com/courses/cqrs-in-practice) course by Vladimir Khorikov. Some of the code in this library was extended directly from Vladimir's examples in the course.


## The basics

CQRS stands for Command, Query Responsibility Segregation. The primary principle is to create a barrier between the commands in your system (writing) from queries (reading).

It is often described alongside [event sourcing](https://martinfowler.com/eaaDev/EventSourcing.html) but CQRS can still be used to reduce the complexity of CRUD apps and make it easier to aim towards the [single responsibility principle](https://en.wikipedia.org/wiki/Single-responsibility_principle).

> A sample application will be created to help describe these concepts but it is not available yet.

## Conventions

- It is good practice to seperate your read models from your write models. In the sample code, Entity Framework is used for writing to the system and Dapper is used for reads. Write models are stored in the Domain project, with read models in a sub folder 'Read'.


## Setup

Extension methods have been provided for Microsoft Dependency Injection to configure all of your handlers with one line, added to startup.cs (ASP.NET Core), or program.cs (Blazor Web Assembly) 

> **services.ConfigureCqrsObjects()**

An overload exists for finding the objects in another project by specifying one of the types in that project.

> **services.ConfigureCqrsObjects(typeof(T))**

There is also an additional overload for finding by passing in an assembly to search. If your query and command handlers exist in more than one project, you will need to run the above line for each of those projects.

If you do not want a handler to be registered, you can ignore it by applying the **[CqrsIgnore]** attribute.

## Interfaces

- **IQueryHandler\<TQuery, TResult\>**
- **IQueryHandlerAsync\<TQuery, TResult\>**
- **IQuery\<TResult\>**
- **ICmdHandler\<TCmd\>**
- **ICmdHandlerAsync\<TCmd\>**
- **ICmdResult**

**IQuery** enforces design time type checking against TResult to ensure they are both the same type.

**ICmdResult** is always returned from command handlers which is useful for having a standard way of success or a list of failures after a command or chain of commands has run.

## Command pipelines

Commands can be chained together in a pipeline using the decorator pattern. Each command handler in the chain must use the attribute **[CqrsDecorator]**, except the final handler, which uses multiple **[CqrsDecoratedBy(T)]** attributes to tell the HandlerRegistrar the order that command handlers should be in.

## Abstract helper classes

A few abstract classes have been provided, which your handlers can be based on. 

These are -
- **NextOnSuccessDecorator\<TCmd\>**
- **NextOnSuccessDecoratorAsync\<TCmd\>**

When set as the base class for a command handler which is to act as a decorator, it will execute the code of that handler and the only continue to run the next handler in the chain, if itself succeeded. 

- **TryCatchHandler\<TCmd\>**
- **TryCatchHandlerAsync\<TCmd\>**

When set as the base class for a command handler which exists by itself, (or, is the final in the chain) will wrap the execution in a try catch block and add the exception (if reached) to the ICmdResult.

## TinyCqrs.FluentValidation

If you use FluentValidation, this provides the extension method **ThisOrNext(ValidationResult, TCmd)** which behaves the same as **NextOnSuccessDecorator**, except it is a more succinct expression.