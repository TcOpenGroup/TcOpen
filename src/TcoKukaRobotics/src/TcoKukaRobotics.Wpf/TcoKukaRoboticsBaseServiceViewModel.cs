using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoKukaRobotics
{

    abstract public class TcoKukaRoboticsBaseServiceViewModel<T> : RenderableViewModel where T : class, new()
    {

        public TcoKukaRoboticsBaseServiceViewModel() : base()
        {
            Component = new T();


        }


        public T Component { get; internal set; }

        public override object Model
        {
            get => Component;
            set
            {
                Component = value as T;

            }
        }

    }

}