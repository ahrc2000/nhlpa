using System;

namespace HockeyApi.Common.Services
{
    public interface IHelperService
    {
        int AddPlayerToTeam(string fname, string lname, string tcode, DateTime dt);
    }
}