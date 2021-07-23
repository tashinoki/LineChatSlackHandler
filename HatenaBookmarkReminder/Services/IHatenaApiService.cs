using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HatenaBookmarkReminder.Services
{
    public interface IHatenApiService
    {

        public Task OAuthAsync();
    }
}
