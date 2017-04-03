using System;
using System.IO;
using System.Threading.Tasks;
using ProtoBuf.Meta;

namespace UtilityDelta.Shared.Common
{
    public class Serializer : ISerializer
    {
        private readonly RuntimeTypeModel m_protobufModel;

        public Serializer()
        {
            //Setup the protobuf-net serializer
            m_protobufModel = TypeModel.Create();

            m_protobufModel.AllowParseableTypes = false;
            m_protobufModel.AutoAddMissingTypes = true;
            m_protobufModel.AutoAddProtoContractTypesOnly = false;
            m_protobufModel.IncludeDateTimeKind = false;
            m_protobufModel.UseImplicitZeroDefaults = false;

            //Add surrogate classes or any other specific serialization logic
            //before the call to CompileInPlace

            //Compile the model to avoid reflection overhead during serialization
            m_protobufModel.CompileInPlace();
            m_protobufModel.AutoCompile = true;
        }

        public async Task<object> DeSerialize(Stream data, Type type)
        {
            return await Task.Run(() => m_protobufModel.Deserialize(data, null, type));
        }

        public async Task<Stream> Serialize(object item)
        {
            var stream = new MemoryStream();
            await Task.Run(() => m_protobufModel.Serialize(stream, item));
            stream.Position = 0;
            return stream;
        }
    }
}