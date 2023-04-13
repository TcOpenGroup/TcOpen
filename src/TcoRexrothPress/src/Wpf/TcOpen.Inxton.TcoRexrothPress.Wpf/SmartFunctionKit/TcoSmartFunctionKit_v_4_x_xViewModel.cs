using Vortex.Presentation.Wpf;


namespace TcoRexrothPress

{
    public class TcoSmartFunctionKit_v_4_x_xViewModel

        : RenderableViewModel
    {

        public TcoSmartFunctionKit_v_4_x_xViewModel () 
        {

         
        }
        public TcoSmartFunctionKit_v_4_x_x Component { get; private set; } 
        public override object Model { get => Component; set { Component = value as TcoSmartFunctionKit_v_4_x_x; } }

    }
    public class TcoSmartFunctionKit_v_4_x_xServiceViewModel : TcoSmartFunctionKit_v_4_x_xViewModel 
    {


    }


}