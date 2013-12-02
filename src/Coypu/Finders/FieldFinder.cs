using System;
using Coypu.Drivers;

namespace Coypu.Finders
{
    internal class FieldFinder : XPathQueryFinder
    {
        internal FieldFinder(Driver driver, string locator, DriverScope scope) : base(driver, locator, scope) { }

        public override bool SupportsPartialTextMatching
        {
            get { return true; }
        }

        protected override Func<string, Options, string> GetQuery(XPath xpath)
        {
            return xpath.Field;
        }

        internal override string QueryDescription
        {
            get { return "field: " + Locator; }
        }
    }
}