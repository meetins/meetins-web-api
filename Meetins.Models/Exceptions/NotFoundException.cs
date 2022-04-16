using System;

namespace Meetins.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message) { }
    }
}
