# Pneumatic components

## TcoCylinder

### Declaration

```
    {attribute addProperty Name "Vertical piston"}                  // Name/label of the cyclinder on HMI
	{attribute addProperty _moveHomeDefaultName "<#MOVE DOWN#>"}    // Name/label of the move home task on HMI
	{attribute addProperty _moveWorkDefaultName "<#MOVE UP#>"} // Name/label of the move work task on HMI
	{attribute addProperty _stopDefaultName "<#STOP#>"}             // Name/label of the stop task on HMI
	_verticalPiston : TcoPneumatics.TcoCylinder(THIS^);
```

### Initialization

```
// Must be called cyclically with call tree of `Main` of a context.
_verticalPiston(inoAtHomePos := IO.iA1[0],
    inoAtWorkPos := IO.iA1[1],
    inoToHomePos := IO.qA1[0],
    inoToWorkPos := IO.qA1[1]);

// Abort conditions must be called after init of the component.
_verticalPiston.AbortMoveHome(_sometingInTheWay);
_verticalPiston.AbortMoveWork(_sometingInTheWay);
```

### Use

```
// Return true when done
VerticalAxis.MoveToWork().Done
// Fire and forget
VerticalAxis.MoveToWork();
```
