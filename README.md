![TcOpen logo](assets/logo/TcOpenWide.png)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/TcOpenGroup/TcOpen/graphs/commit-activity)
[![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)](https://github.com/TcOpenGroup/TcOpen/blob/dev/LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://github.com/TcOpenGroup/TcOpen/pulls)
[![Open Source? Yes!](https://badgen.net/badge/Open%20Source%20%3F/Yes%21/blue?icon=github)](https://github.com/TcOpenGroup/TcOpen)
[![TcOpen Slack ](https://img.shields.io/badge/Slack-channel-ff69b4.svg)](https://tcopendevelopment.slack.com/)
[![Join the chat at https://gitter.im/dotnet/coreclr](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/TcOpenGroup/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Awesome Badges](https://img.shields.io/badge/badges-awesome-green.svg)](https://github.com/TcOpenGroup/TcOpen#awesome-twincat-3-projects-)
[![Build Status](https://dev.azure.com/petokurhajec0964/tc3/_apis/build/status/TcOpenGroup.TcOpen?branchName=dev)](https://dev.azure.com/petokurhajec0964/tc3/_build/latest?definitionId=6&branchName=dev)

# TcOpen

**Open Source TwinCAT 3 code by Automation Professionals for Automation Professionals.**

TcOpen aims to provide standard classes to start building any **real deployable application**. TcOpen will help you by providing well-tested components you can use in your project free of charge.  

## Why?

We want to introduce modern software development practices to the PLC world so developers can shift from low-value work to high-value work. OpenSource offers a solid ground upon which many can build and bring ideas from all over the world.

## Goals

- Introduce software engineering patterns to PLC development.
- Modular, extensible, and observable code and applications.
- Bring the TwinCAT community together.
- Basic libraries and components for every project.
- Cultivate a place for sharing knowledge.

## What is TwinCAT?

TwinCAT software system turns almost any compatible PC into a real-time controller with a multi-PLC system, NC axis control, programming environment, and operating station. TwinCAT replaces conventional PLC and NC/CNC controllers. It runs in Visual Studio with CodeSys.

# Where we are - Development process 

The initial momentum of ```TcOpen``` project was powerful; however, we have seen a slow decline in activity over the past months. [Inxton](inxton.com),  [MTS](www.mts.sk/en) team and some other heroic knights decided to keep the lights on and to carry on this initiative. 

We understand that many of you guys have hard times at work, a lot of late hours, endless traveling. Life in industrial automation is very demanding. We see this as one of the reasons for the declined activity. Also, many automation engineers are not that familiar with modern software development tooling and workflows. It may scare some people off. Unfamiliarity should not be the reason not to participate. We want this place to be welcoming to everyone that sees it as a meaningful path to industrial automation. The community is going to learn along the path. There are not that many open-source projects for industrial automation.

## Recent developments merging to TcOpenGroup

There has been much activity without visible tracking in recent times. We created a set of base classes to help us craft components and projects in industrial automation. Most of the discussions were in pair-programming and online/in-person meetings. We did it this way to speed up initial development when transferring legacy libraries and concepts to TcOpen.

[Here](https://github.com/Inxton/TcOpen.Documentation/blob/dev/articles/TcOpenFramework/application.md) is a conceptual description of the work done so far.

[Here](https://github.com/Inxton/TcOpen.Documentation/blob/dev/articles/Conventions/Conventions.md) is the document describing conventions to adhere to.

## Workflow (modified GitHub flow)

We adopt a modified version of [**GitHub flow**](https://guides.github.com/introduction/flow/) with proper tracking and discussions under PRs. It is a very simple workflow; we would like to work in a continuous integration/deployment fashion. However, we have to consider the need for the LTS versions for stable and long time support. How do we exactly do it is open to discussion. At this point, we work with the following modification: our default branch is ```dev```, and all PRs must be directed there instead of ```main``` (previously ``` master```). We release in the ```main``` branch once we see the version is stable and battle-tested in production by early adopters.

## Versioning

We adopt [semantic versioning](https://semver.org/). The pipeline uses [GitVersion tool](https://gitversion.net/docs/) for the version calculation build. 

At this point, we keep the major version at ```0``` (0.x.x) since we do expect changes to the public interfaces, and time is needed before the public contracts are stable. 

## Monorepo

We have also decided to work in a [monorepo](https://en.wikipedia.org/wiki/Monorepo) at this point. About the structure later down. Each unit (TcoCore, TcoPneumatics, TcoDrives, etc.) has its filtered solution (*.slnf) for that unit development for faster IDE opening and manipulation. Monorepo makes it easier to work with the dependencies and prevent possible dependency hell scenarios, which is a genuine risk at this early stage of the project. Once the framework is stable, we may move to a poly-repository solution with a separate group of maintainers.

## Repository structure

Some of the information here might self-evident for traditionals users of github and open source in general. We provide here more details to aid people that are not familiar with git, github and workflows.

### Root

| FOLDER             | DESCRIPTION                                                                                                    |
|--------------------|----------------------------------------------------------------------------------------------------------------|
| .github            | GitHub related files, templates, ...                                                                           |
| _Vortex            | Inxton tools, builder, CLIs, config files, output files, and folders, ...                                      |
| assets             | misc files, logo, pictures                                                                                     |
| docs               | temporary folder for documentation, notes. etc.                                                                |
| pipelines          | delivery pipeline scripts and configurations                                                                   |
| src                | all sources, tests, UI controls                                                                                |
| GitVersion.yml     | GitVersion configuration file                                                                                  |
| README.md          | This file                                                                                                      |
| TcoOpen.build.slnf | Filtered solution contains all other projects except for TwinCAT project (NuGet restore/build in the pipeline) |
| TcoOpen.plc.slnf   | Filtered solution, contains only TwinCAT projects (bulk library compilation)                                   |
| TcoOpen.sln        | Full solution file                                                                                             |
| notices.md         | Licenses of other open-source projects used in this repository                                                 |

### src

| FOLDER                 | DESCRIPTION                                                    |
|------------------------|----------------------------------------------------------------|
| Tc.Prober              | Unit testing libraries                                         |
| TcoApplicationExamples | Contains application examples that use TcOpen libraries        |
| TcoCore                | Core libraries of TcOpen framework (task, coordinations, etc.) |
| TcoIoBeckhoff          | Beckhoff hardware (I/O) library                                |
| TcoPeumatics           | Pneumatic components library                                   |
| Others.....            | _each group of components will have its separate folder_       |


### Typical structure of library folder

| FOLDER          | DESCRIPTION                        |
|-----------------|------------------------------------|
| src             | library source files               |
| src/..Wpf       | Inxton WPF components              |
| src/..Connector | Inxton compiler connector          |
| src/Xae..       | Plc project/sources                |
| tests           | unit, and integration tests folder |


## Testing

## Documentation

## Communication channels

Some of you complained you were unable to join the Slack Channel for various reasons. After a discussion with @dhullett08 we are opening a new gitter channel:

[![Join the chat at https://gitter.im/dotnet/coreclr](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/TcOpenGroup/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)


## Contributing

## Suggestions? Issues?

### Awesome TwinCAT 3 projects 🌐

- An xUnit testing framework for Beckhoff TwinCAT3.  [TcUnit](https://github.com/tcunit/TcUnit) 
- Unofficial TwinCAT function for HTTP requests with json conversion [Beckhoff Http Client ](https://github.com/fbarresi/BeckhoffHttpClient)
- Opinionated code formatter for TwinCAT. [TcBlack](https://github.com/Roald87/TcBlack)
- Bring the power of Json.Net to TwinCAT [TwinCAT.JsonExtension](https://github.com/fbarresi/TwinCAT.JsonExtension)
