using System.Collections.Generic;
using System.Linq;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class OutputBase_10CEE7DEHWDiagViewModel : RenderableViewModel
    {        
        public IEnumerable<IVortexElement> Outputs { get { return GetArrayed(Component); } }       
        public IVortexObject Component { get; private set; }
        public override object Model { get => Component; set => Component = value as IVortexObject; }

        private IEnumerable<IVortexElement> GetArrayed(IVortexObject obj)
        {
            var arrays = obj.GetType().GetProperties().Where(p => p.PropertyType.IsArray).Select(p => p.GetValue(obj));

            var listOfArrayedMembers = new List<IVortexElement>();

            foreach (IEnumerable<IVortexElement> array in arrays.Where(p => p is IEnumerable<IVortexElement>))
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
