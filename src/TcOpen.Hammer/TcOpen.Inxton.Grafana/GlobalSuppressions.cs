// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'",
    Justification = "If someone uses this project with older target, he will have to rewrite the code",
    Scope = "member",
    Target = "~P:TcOpenHammer.Grafana.API.Transformation.TableWithTimeColumn`1.TimeColumn")]
[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'", 
    Justification = "If someone uses this project with older target, he will have to rewrite the code",
    Scope = "member", 
    Target = "~F:TcOpenHammer.Grafana.API.Controllers.ImageController.NotFoundImage")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "PLC Twin object", Scope = "namespaceanddescendants", Target = "~N:Grafana.Backend.Model")]
