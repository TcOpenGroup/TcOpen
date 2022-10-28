# How to write a sequence

You need to have TcoCore installed. If you don't here's how to do it [link](../How_to_get_started_using_tcopen_libraries/article.md)

I recommend to read more about it [here](~/articles/TcOpenFramework/TcoCore/TcoSequencer.md).
                                           

> [!Video https://youtube.com/embed/JoQUKgtXUBM]

1. Create a Context
```
FUNCTION_BLOCK MainContext EXTENDS TcoCore.TcoContext
VAR
END_VAR
```
2. Create a Sequence
```
FUNCTION_BLOCK AutomaticSequence EXTENDS TcoCore.TcoSequencer
VAR
END_VAR
```

3. Create `Main` method in `AutomaticSequence` and start writing your sequence with `Step`
```
METHOD PROTECTED  Main : BOOL
---
IF Step(0, TRUE, 'First Step') THEN
    StepCompleteWhen(TRUE);
END_IF

IF Step(10, TRUE, 'Second Step') THEN
    StepCompleteWhen(TRUE);
END_IF

IF Step(20, TRUE, 'Third Step') THEN
    StepCompleteWhen(TRUE);
END_IF

IF Step(30, TRUE, 'Last Step') THEN
    CompleteSequence();
END_IF
```

4. Create instance of `AutomaticSequence` in `MainContext`
```
FUNCTION_BLOCK MainContext EXTENDS TcoCore.TcoContext
VAR
    AutomaticSeq : AutomaticSequence(THIS^, eRestoreMode.None);
END_VAR
```
5. Run the `AutomaticSequence` in `MainContext.Main` method
```
METHOD PROTECTED  Main
---
AutomaticSeq.Run();
```
6. Run the `MainContext` in `PRG`
```
PROGRAM MAIN
VAR
	MainContext : MainContext;
END_VAR
---
MainContext.Run();
```


