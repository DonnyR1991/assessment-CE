using System;

namespace ChannelEngine.Common
{
    public static class Enums
    {
        public enum ChannelOrderSupportType : byte
        {
            NONE = 0,
            ORDERS = 1,
            SPLIT_ORDERS = 2,
            SPLIT_ORDER_LINES = 3
        }

        public enum OrderStatus : byte
        {
            IN_PROGRESS = 0,
            SHIPPED = 1,
            IN_BACKORDER = 2,
            MANCO = 3,
            CANCELED = 4,
            IN_COMBI = 5,
            CLOSED = 6,
            NEW = 7,
            RETURNED = 8,
            REQUIRES_CORRECTION = 9
        }

        public enum Gender : byte
        {
            MALE = 0,
            FEMALE = 1,
            NOT_APPLICABLE = 2
        }

        public enum OrderLineCondition : byte
        {
            NEW = 0,
            NEW_REFURBISHED = 1,
            USED_AS_NEW = 2,
            USED_GOOD = 3,
            USED_REASONABLE = 4,
            USED_MEDIOCRE = 5,
            UNKNOWN = 6,
            USED_VERY_GOOD = 7
        }
    }
}
