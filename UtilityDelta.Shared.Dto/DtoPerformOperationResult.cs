using ProtoBuf;

namespace UtilityDelta.Shared.Dto
{
    [ProtoContract]
    public class DtoPerformOperationResult
    {
        [ProtoMember(1)]
        public string ExecutedBy { get; set; }
        [ProtoMember(2)]
        public int Result { get; set; }
    }
}