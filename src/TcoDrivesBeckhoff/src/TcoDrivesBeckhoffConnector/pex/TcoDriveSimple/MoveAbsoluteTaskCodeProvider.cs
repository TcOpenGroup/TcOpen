using System;
using System.Linq;
using TcOpen.Inxton.Swift;
using Vortex.Connector;

namespace TcoDrivesBeckhoff
{
    public class MoveAbsoluteTaskCodeProvider : ICodeProvider
    {
        public MoveAbsoluteTaskCodeProvider(IVortexObject origin)
        {
            Origin = origin;
        }

        public IVortexObject Origin { get; }

        public string Code(params object[] args)
        {
            var task = (Origin as TcoDriveSimple)?._moveAbsoluteTask;

            return $@"{Origin.Symbol}.MoveAbsolute(
                                                         inPosition := {task._position.Synchron},
                                                         inVelocity := {task._velocity.Synchron},
                                                         inAcceleration := {task._acceleration.Synchron},
                                                         inDeceleration := {task._deceleration.Synchron},
                                                         inJerk := {task._jerk.Synchron}).Done";
        }
    }
}
