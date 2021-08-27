using Microsoft.AspNetCore.Identity;

namespace TcOpen.Inxton.Security
{
    public class Role : IdentityRole<string>
    {       
        public string DefaultGroup { get; private set; }

        public Role(string Name, string DefaultGroup)
        {
            var normalizer = new UpperInvariantLookupNormalizer();
            this.Name = Name;
            this.DefaultGroup = DefaultGroup;
            this.NormalizedName = normalizer.NormalizeName(this.Name);
        }

        public override string ToString() => Name;
    }
}
