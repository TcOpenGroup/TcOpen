using System;
using System.Linq;
using TcOpen.Inxton.Swift;
using Vortex.Connector;

namespace TcoDrivesBeckhoff
{
    public class MoveVelocityTaskCodeProvider : ICodeProvider
    {

        public MoveVelocityTaskCodeProvider(IVortexObject origin)
        {
            Origin = origin;
        }

        public IVortexObject Origin { get; }

        public string Code(params object[] args)
        {
            var task = (Origin as TcoDriveSimple)?._moveVelocityTask;

            return $@"{Origin.Symbol}.MoveVelocity(                                                         
                                                         inVelocity := {task._velocity.Synchron},
                                                         inAcceleration := {task._acceleration.Synchron},
                                                         inDeceleration := {task._deceleration.Synchron},
                                                         inJerk := {task._jerk.Synchron},
                                                         inDirection := {task._direction.Synchron}).Done";

        }
    }
}

