using System;
using System.Linq;
using TcOpen.Inxton.Swift;
using Vortex.Connector;

namespace TcoDrivesBeckhoff
{
    public class MoveRelativeTaskCodeProvider : ICodeProvider
    {

        public MoveRelativeTaskCodeProvider(IVortexObject origin)
        {
            Origin = origin;
        }

        public IVortexObject Origin { get; }

        public string Code(params object[] args)
        {
            var task = (Origin as TcoDriveSimple)?._moveRelativeTask;

            return $@"{Origin.Symbol}.MoveRelative(
                                                         inDistance := {task._distance.Synchron},
                                                         inVelocity := {task._velocity.Synchron},
                                                         inAcceleration := {task._acceleration.Synchron},
                                                         inDeceleration := {task._deceleration.Synchron},
                                                         inJerk := {task._jerk.Synchron}).Done";

        }
    }
}

