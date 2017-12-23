using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using REMS.Interfaces;

namespace REMS.Website._classes
{
    public class WebContext : IAppContext
    {

        #region Properties

        /// <summary>
        /// Caching implementation
        /// </summary>
        public ICache Cache { get; set; }

        #endregion

        /// <summary>
        /// Set all app context settigs for site
        /// </summary>
        /// <param name="configSettings"></param>
        /// <param name="cache"></param>
        public WebContext(ICache cache)
        {
            Cache = cache;
        }
    }
}