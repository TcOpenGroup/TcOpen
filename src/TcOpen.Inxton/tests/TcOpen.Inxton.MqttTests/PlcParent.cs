using Vortex.Connector;
namespace TcOpen.Inxton.MqttTests

{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
    public partial class PlcParent : Vortex.Connector.IVortexObject
    {
        public string Symbol
        {
            get;
            protected set;
        }

        public string HumanReadable
        {
            get
            {
                return _humanReadable;
            }

            protected set
            {
                _humanReadable = value;
            }
        }

        protected string _humanReadable;


        partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
        partial void PexPreConstructorParameterless();
        private System.Collections.Generic.List<Vortex.Connector.IVortexObject> @Children
        {
            get;
            set;
        }

        public System.Collections.Generic.IEnumerable<Vortex.Connector.IVortexObject> @GetChildren()
        {
            return this.@Children;
        }

        public void AddChild(Vortex.Connector.IVortexObject vortexObject)
        {
            this.@Children.Add(vortexObject);
        }

        private System.Collections.Generic.List<Vortex.Connector.IVortexElement> Kids
        {
            get;
            set;
        }

        public System.Collections.Generic.IEnumerable<Vortex.Connector.IVortexElement> GetKids()
        {
            return this.Kids;
        }

        public void AddKid(Vortex.Connector.IVortexElement vortexElement)
        {
            this.Kids.Add(vortexElement);
        }

        partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
        partial void PexConstructorParameterless();
        protected Vortex.Connector.IVortexObject @Parent
        {
            get;
            set;
        }

        public Vortex.Connector.IVortexObject GetParent()
        {
            return this.@Parent;
        }

        private System.Collections.Generic.List<Vortex.Connector.IValueTag> @ValueTags
        {
            get;
            set;
        }

        public System.Collections.Generic.IEnumerable<Vortex.Connector.IValueTag> GetValueTags()
        {
            return this.@ValueTags;
        }

        public void AddValueTag(Vortex.Connector.IValueTag valueTag)
        {
            this.@ValueTags.Add(valueTag);
        }

        public Vortex.Connector.IConnector @Connector
        {
            get;
            set;
        }

        public Vortex.Connector.IConnector GetConnector()
        {
            return this.@Connector;
        }



        public void FlushShadowToOnline()
        {
            this.Write();
        }

        public void FlushOnlineToShadow()
        {
            this.Read();
        }



        protected System.String @SymbolTail
        {
            get;
            set;
        }

        public System.String GetSymbolTail()
        {
            return this.SymbolTail;
        }

        public System.String AttributeName
        {
            get
            {
                return _AttributeName;
            }

            set
            {
                _AttributeName = value;
            }
        }

        private System.String _AttributeName
        {
            get;
            set;
        }

        public PlcParent(Vortex.Connector.IConnector connector, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = connector;
            this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
            this.@Parent = connector;
            _humanReadable = "Parent";
            this.Kids = new System.Collections.Generic.List<Vortex.Connector.IVortexElement>();
            this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();

            Symbol = "Parent";
            AttributeName = "";
            connector.AddChild(this);
            connector.AddKid(this);
        }

        public PlcParent()
        {
            PexPreConstructorParameterless();
            AttributeName = "";
            PexConstructorParameterless();
        }
    }




}
