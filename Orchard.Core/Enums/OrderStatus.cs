namespace Orchard.Core.Enums;

public enum OrderStatus : byte
{
    Unsubmitted = 1,
    Submitted,
    Confirmed,
    Cancelled,
    Completed,
    Shipped,
    Delayed,
    ReturnRequested,
    Returned
}