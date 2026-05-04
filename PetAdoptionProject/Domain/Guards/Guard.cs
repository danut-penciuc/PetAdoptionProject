using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Guards
{
    public static class Guard
    {
        public static void AgainstNullOrWhiteSpace(string value, string field)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException($"{field} is required");
        }


        public static void AgainstNegative(int value, string field)
        {
            if (value < 0)
                throw new DomainException($"{field} cannot be negative");
        }

        public static void AgainstDefaultGuid(Guid value, string field)
        {
            if (value == Guid.Empty)
                throw new DomainException($"{field} is required");
        }

        public static void AgainstFutureDate(DateTime value, string field)
        {
            if (value > DateTime.UtcNow)
                throw new DomainException($"{field} cannot be in the future");
        }
    }
}
