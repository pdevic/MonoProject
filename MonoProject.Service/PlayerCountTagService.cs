using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class PlayerCountTagService : IPlayerCountTagService
    {
        private readonly IPlayerCountTagRepository repository;

        public async static Task<bool> ValidatePlayerCountTagName(string name)
        {
            var task = Task.Run(() =>
            {
                int len = name.Length;
                int lenMax = MonoProject.Common.Common.playerCountTagMaxNameLength;

                if ((len > 0) && (len < lenMax))
                {
                    return true;
                }

                throw new Exception(String.Format("GameInfo name length ({0}) not in range [{1}, {2}], name = \"{3}\"", len, 1, lenMax, name));
            });

            await task;
            return task.Result;
        }

        public PlayerCountTagService(IPlayerCountTagRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<IPlayerCountTag>> GetListAsync()
        {
            return await repository.GetListAsync();
        }

        public async Task<IPlayerCountTag> GetByIDAsync(int entityKey)
        {
            return await repository.GetByIDAsync(entityKey);
        }

        public async Task<IPlayerCountTag> InsertAsync(IPlayerCountTag entityToInsert)
        {
            try
            {
                await ValidatePlayerCountTagName(entityToInsert.Name);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("GameInfoService failed to insert a GameInfo instance into its repository");
            }

            entityToInsert.ID = Guid.NewGuid().GetHashCode();
            entityToInsert.DateCreated = System.DateTime.Now;
            entityToInsert.DateUpdated = System.DateTime.Now;
            entityToInsert.TimeStamp = System.DateTime.Now;

            return await repository.InsertAsync(entityToInsert);
        }

        public async Task<IPlayerCountTag> UpdateAsync(IPlayerCountTag entityToUpdate)
        {
            try
            {
                await ValidatePlayerCountTagName(entityToUpdate.Name);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("PlayerCountTagService failed to update a PlayerCountTag instance");
            }

            var entry = await repository.GetByIDAsync(entityToUpdate.ID);
            entry.Name = entityToUpdate.Name;
            entry.DateUpdated = System.DateTime.Now;

            return await repository.UpdateAsync(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            return await repository.DeleteAsync(entityKey);
        }

    }
}
