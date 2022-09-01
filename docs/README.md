# TcOpen

## Quick links

* [Articles](articles/intro.md)
* [Api](https://docs.tcopengroup.org/api/index.html)
* [Application templates](https://github.com/TcOpenGroup/tcopen-app-templates)
* [Convetions](articles/Conventions/Conventions.md)
* [Components conventions](articles/Conventions/Component.md)


## What is TcOpen?

Open Source TwinCAT 3.1 code by Automation Professionals for Automation Professionals.  

This is intended to be a continuous development project aimed to provide standard classes to start building deployable applications.

Developed on [GitHub](https://github.com/TcOpenGroup/TcOpen) and delivered as [NuGet](https://www.nuget.org/packages?q=TcOpenGroup) packages](https://www.nuget.org/packages?q=TcOpenGroup).

## TcOpen is a framework

We believe we can do better with a framework than without one. Here are the main reasons: a framework unifies the way that we create and interpret a software solution. It can relieve the programmer of various tasks. Allowing him to focus on solving actual problems, instead of having to program the condiments (alarms, state initiating/management, coordination, etc.).

A framework also helps to organize the program's structure — making it easier and faster to navigate and apply the changes for other programmers. It also helps to craft better components and ultimately deliver better applications. Without entering into technical details about how the framework is structured: we put a lot of effort and thought into making it sufficiently generic/abstract in order for it to suit as many applications as possible.

We understand that imposing a framework requires more than learning the programming language. We believe the advantages of the framework make it well worth the input!

We focus on these areas:

- Coordination (coordination primitives, sequences, etc.)
- Observability (automated alarm systems, logging, hierarchical events organization)
- Hierarchical program structures
- Automation of monotonous tasks (I/O binding)
- Reusability
- Data-driven applications

## TcOpen is Object-Oriented

Besides the practical aspect of this project, there is yet another one, as we understand it — to bring a breath of fresh air to PLC programming; we will focus on Object-Oriented design to the maximum extent it is allowed in the TwinCAT 3 implementation of IEC-61131-3. The object-oriented approach allows for componentization that is simply not possible with the non-oop paradigm.
TcOpen will be written in ST-OOP. We plan to have a solid replacement for SFC in the ST implementation.

## TcOpen compatibility

### Beckhoff HMI 

The Inxton and MTS teams will focus on creating UI user controls for WPF, and then Blazor technology. We are committed to maintaining compatibility/cooperability with Beckhoff's HMI. The community is warmly welcome to contribute to Beckhoff HMI components. A maintainer with solid experience is needed here.

### CoDeSys systems

There is no explicit promise of portability to other CoDeSys platforms. Still, we will seek to manage abstractions to keep the TcOpen code base adaptable to other CoDeSys systems.

### Non-CoDeSys systems

There is no intention to support codebase portability, nor interfacing to other non-CoDeSys platforms (Siemens, AB, and the like).
