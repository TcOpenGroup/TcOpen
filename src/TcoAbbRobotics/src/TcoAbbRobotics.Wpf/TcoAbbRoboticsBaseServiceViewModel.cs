using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoAbbRobotics
{

    abstract public class TcoAbbRoboticsBaseServiceViewModel<T> : RenderableViewModel where T : class, new()
    {

        public TcoAbbRoboticsBaseServiceViewModel() : base()
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