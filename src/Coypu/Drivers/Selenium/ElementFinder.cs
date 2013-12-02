using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Coypu.Drivers.Selenium
{
    internal class ElementFinder
    {
        public IWebElement Find(By by, Scope scope, string queryDescription, Func<IWebElement, bool> predicate = null)
        {
            var results = FindAll(@by, scope, predicate).Where(e => matches(predicate, e));
                
            return scope.Options.FilterWithMatchStrategy(results, queryDescription);
        }

        public IWebElement Find(IEnumerable<By> bys, Scope scope, string queryDescription, Func<IWebElement, bool> predicate = null)
        {
            var results = bys.Select(by => FindAll(by, scope, predicate))
                             .FirstOrDefault(matches => matches.Any());

            return scope.Options.FilterWithMatchStrategy(results ?? Enumerable.Empty<IWebElement>(), queryDescription);
        }

        private static bool matches(Func<IWebElement, bool> predicate, IWebElement firstMatch)
        {
            return (predicate == null || predicate(firstMatch));
        }

        public IEnumerable<IWebElement> FindAll(By @by, Scope scope, Func<IWebElement, bool> predicate = null)
        {
            return SeleniumScope(scope).FindElements(@by).Where(e => predicate(e) && IsDisplayed(e, scope));
        }

        public ISearchContext SeleniumScope(Scope scope)
        {
            return (ISearchContext) scope.Find().Native;
        }

        public bool IsDisplayed(IWebElement e, Scope scope)
        {
            return scope.ConsiderInvisibleElements || e.IsDisplayed();
        }
    }
}