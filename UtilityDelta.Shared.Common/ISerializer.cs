using System;
using System.IO;
using System.Threading.Tasks;

namespace UtilityDelta.Shared.Common
{
    public interface ISerializer
    {
        Task<object> DeSerialize(Stream data, Type type);
        Task<Stream> Serialize(object item);
    }
}