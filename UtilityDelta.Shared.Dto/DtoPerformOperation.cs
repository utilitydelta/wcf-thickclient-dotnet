using ProtoBuf;
using UtilityDelta.Shared.Common;

namespace UtilityDelta.Shared.Dto
{
    [ProtoContract]
    public class DtoPerformOperation
    {
        [ProtoMember(1)]
        public int NumberOne { get; set; }
        [ProtoMember(2)]
        public int NumberTwo { get; set; }
        [ProtoMember(3)]
        public EnumOperationType OperationType { get; set; }
    }
}