# Components in TcOpen

This document describes the agreed format and practices for writing components in TcOpen. There are general rules to observe. Each rule knows exception with a reasonable argument behind it. There should be, however, no exception when it comes to the quality of the solution.

## General rules

### I/O variables

- Components must not contain I/O variables directly.
- Components accept I/O variables via FB_body parameter transfer (INPUT, OUTPUT, VAR_IN_OUT).
- The FB_body must be called before any other call that uses the component.
- All `VAR_IN_OUT` parameters, whenever used in methods or properties must be checked for the valid reference with `__ISVALIDREFENCE`.

### Structure

### Config

- Each component should implement `Config` property with the backing field named `_config` of the appropriate type.
- Config structure can contain arbitrary data relative to the configuration of the component.
- Config structure can contain multiple nested and complex structure when it is necessary to better organize the information contained.
- Config must be STRUCT.
- Must be stored in a backing field `_config` of a given type and exposed as property `Config` which should be `REFERENCE TO <config_structure_type>`
- All data must be initialized to default values (e.g. timeouts, etc) that represent the data-set of a fully operational component. 
- The default settings should not prevent the component from functioning
- Mandatory settings relative to specific hardware or configuration must be clearly outlined in the documentation, as well as an example configuration data set with detailed explanation must be provided.
- Property `Config` can be mutable (can have setter) when it is necessary to provide an external configuration set at runtime, otherwise it is preferable to keep it not-settable (without setter) only individual data items can be modified.

## Status

- Should contain arbitrary status information data
- Can contain additional structures to organize the information
- Must NOT contain FBs only STRUCTs
- Must be stored in a backing property `_status` of a given type and exposed as property `Status` which should be REFERENCE TO `structure_type`


- It is advisable that the I/O variables are declared in a GVL/PRG, organized in a structure that resembles the actual hardware topology; this is not mandatory, and it is up to the application developer where the I/O variable is placed.
