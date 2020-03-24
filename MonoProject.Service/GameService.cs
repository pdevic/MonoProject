using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Service
{
    public class GameService
    {
        public async static Task<bool> ValidateGameInfoName(string name)
        {
            var task = Task.Run(() =>
            {
                var len = name.Length;
                var lenMax = MonoProject.Common.Common.gameInfoNameLength;

                if ((len == 0) || (len > lenMax))
                {
                    return true;
                }

                throw new Exception(String.Format("GameInfo name length ({0}) not in range [{1}, {2}], name = \"{3}\"", len, 1, lenMax, name));
            });

            await task;
            return task.Result;
        }
    }
}
