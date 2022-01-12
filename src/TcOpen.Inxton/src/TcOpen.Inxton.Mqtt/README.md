# TcoMQTT

Please note that you need use Inxton connector and compiler in order to use the extension functions.

## How to publish primitives

Access the C# Twin of a PLC variable and use the `PublishChanges` extension from `TcOpen.Inxton.Mqtt` namespace.

```csharp
Entry.MAIN_TECHNOLOGY._technology
    ._startCycleCount
    .PublishChanges(client, "fun_with_TcOpen_Hammer");
```

### Publishing with a conditon

You can specify a condition before publishing the value. this code will publish a new value only if the difference is greater than 100.

```csharp
Entry.MAIN_TECHNOLOGY._technology
    ._startCycleCount
    .PublishChanges(client, "fun_with_TcOpen_Hammer",
        publishCondition: (lastPublished, toPublish) => toPublish - lastPublished >= 100);
```

### How to stop publishing 

```csharp
var handle = Entry.MAIN_TECHNOLOGY._technology._mirrorStartCycle.Subscribe(client, "fun_with_TcOpen_Hammer");
handle.StopPublishing();
```

## Subscribe to a value

If I'm streaming the variable `_startCycleCount` I can subscribe with variable `_mirrorStartCycle` of the same type to mirror the value via MQTT.

```csharp
Entry.MAIN_TECHNOLOGY._technology
    ._mirrorStartCycle
    .Subscribe(client, "fun_with_TcOpen_Hammer");
```



## Publishing and observign complex types

### Publish
Similar to primitives.

```csharp
Entry.MAIN_TECHNOLOGY._technology._ST001
    ._components
    .PublishChanges(client, "fun_with_TcOpen_HammerX");
```

Complex objects are published every 500ms by default. To change the sample rate use the parameter `sampleRate`


```csharp
Entry.MAIN_TECHNOLOGY._technology._ST001
    ._components
    .PublishChanges(client, "fun_with_TcOpen_HammerX", TimeSpan.FromSeconds(1));
```

You can also write a condition which will determine wheter to publish or not based on the last and latest value.

```csharp
Entry.MAIN_TECHNOLOGY._technology._ST001
    ._components
    .PublishChanges(client, "fun_with_TcOpen_Hammer", publishCondition: ComponentsCondition); 
```

PublishCondition delegate can be  defined as a method. If you're publishing a certain PLC twin, the type of objects inside the will always be a prefixed with "Plain".
```csharp
private bool ComponentsCondition(object LastPublished, object ToPublish)
{
    var lastPublihed = (PlainST001_ComponentsHandler)LastPublished;
    var toPublihed = (PlainST001_ComponentsHandler)ToPublish;
    return lastPublihed._drive._power != toPublihed._drive._power;
}
```

### Subscribe

When you subscribe to complex type, you need to specify its plain countertype. 
If you want to observe changes of type `ST001_ComponentsHandler` you need to subscribe to `PlainST001_ComponentsHandler`.

```csharp
Entry.MAIN_TECHNOLOGY._technology._ST001
    ._mqttMirrorComponents
    .Subscribe<PlainST001_ComponentsHandler>(client, "fun_with_TcOpen_HammerX");
```

