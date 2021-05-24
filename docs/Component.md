# Components

This document describes the format and practices for writing components in TcOpen. These are universal rules to observe. Each rule knows exception when there is a reasonable argument behind it. 

## General rules

### I/O variables

- Components must not contain I/O (%I*, %Q*) variables directly.
- Components accept I/O variables via `FB_body` parameter transfer (`INPUT`, `OUTPUT`, `VAR_IN_OUT`).
- All `VAR_IN_OUT` or `REFERENCE TO` parameters, whenever used in methods or properties, must be checked for the valid reference with `__ISVALIDREFENCE`.
- The `FB_body` must be called before any other call that uses the component.

#### I/O variables naming

The TcOpen does not use Hungarian prefixes, with few exceptions. FB_body parameters are one of those exceptions where it is required to use prefixes to indicate the directionality of the variable. For the naming consult [Conventions](https://docs.tcopengroup.org/articles/Conventions/Conventions.html#naming).

### Structure

#### Config

- Config structure can contain arbitrary data relative to the configuration of the component (timeouts, parameters, etc.).
- Config type must be STRUCT.
- Config data structure must be named in the following format `{ComponentName}Config` (e.g. `TcoCylinderConfig`)
- Config structure must be accessible via `Config` property that returns `REFERENCE TO {ComponentName}Config`.  - The backing field of the Config property must be named `_config` (for easy identification in the higher-level applications).
- Config structure can contain multiple nested and complex structures when it is necessary to organize better the information. Nested structures must be STRUCTs and must be named in the following format `{ComponentName}Config{Specifier}`.
- Wherever possible the data must be initialized to default values (e.g., timeouts, speeds etc.). The default settings should not prevent the component from functioning unless there is a specific requirement to provide parameters relative to the component model or a particular hardware configuration (drive model, gearing ratio, etc.).  
- Each data member of the Config structure must be documented in the code, with an example. Whenever possible, a link to more detailed documentation must also be provided in the in-code documentation.
- Property `Config` can be mutable (can have setter) when it is expected to provide an external configuration at runtime.

### Status

- Status structure can contain arbitrary data relative to the state of the component.
- Status type must be STRUCT.
- Status data structure must be named in the following format `{ComponentName}Status` (e.g. `TcoCylinderStatus`)
- Config structure must be accessible via `Status` property that returns `REFERENCE TO {ComponentName}Status`.  - The backing field of the Config property must be named `_status` (for easy identification in the higher-level applications).
- Status structure can contain multiple nested and complex structures when it is necessary to organize the information. Nested structures must be STRUCTs and must be named in the following format `{ComponentName}State{Specifier}`.  
- Each data member of the Config structure must be documented in the code, with an example. Whenever possible, a link to more detailed documentation must also be provided in the in-code documentation.
- Property `Status` must be immutable (cannot have setter).

### Operations

Operations are methods that execute tasks of the component. All operations must be places  into `Operations` folder of the component. Each operation method must return `ITcoTaskStatus` that is typically `ITcoTask` implementation. Operation name typically starts with descriptive verb that explains the operation.

### Tasks

Operations are run by tasks (`TcoTask`).

- Member variable of task must have following format `_{operationName}Task`.
- Each task must be exposed via property in the following format `{OperationName}Task`.
- Executing logic of a task is run from the `FB_body` of components block.

### States

States are properties or methods that retrieve information about arbitrary states that do not require multiple cycles to return the result (sensor signal status).

TBC

----------------------------------------------
- It is advisable that the I/O variables are declared in a GVL/PRG, organized in a structure that resembles the actual hardware topology; this is not mandatory, and it is up to the application developer where the I/O variable is placed.
