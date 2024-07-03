using System.Collections.Generic;
using System.Linq;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class InputBase_8311D824HWDiagViewModel : RenderableViewModel
    {
        public IEnumerable<IVortexElement> Inputs
        {
            get { return GetArrayed(Component); }
        }
        public IVortexObject Component { get; private set; }
        public override object Model
        {
            get => Component;
            set => Component = value as IVortexObject;
        }

        private IEnumerable<IVortexElement> GetArrayed(IVortexObject obj)
        {
            var arrays = obj.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsArray)
                .Select(p => p.GetValue(obj));

            if (arrays.Count() > 1) { }
            var listOfArrayedMembers = new List<IVortexElement>();

            foreach (
                IEnumerable<IVortexElement> array in arrays.Where(p =>
                    p is IEnumerable<IVortexElement>
                )
            )
            {
                foreach (var item in array)
                {
                    listOfArrayedMembers.Add(item);
                }
            }
            return listOfArrayedMembers;
        }
    }
}
