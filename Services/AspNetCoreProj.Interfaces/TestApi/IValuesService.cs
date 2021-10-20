using System.Collections.Generic;

namespace AspNetCoreProj.Interfaces.TestApi
{
    public interface IValuesService
    {
        IEnumerable<string> GetAll();
        int Count();
        string GetById(int id);
        void Add(string value);
        void Edit(int id, string value);
        bool Delete(int id);

    }
}
