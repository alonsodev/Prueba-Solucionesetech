using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solucionesetech.Command.Common.DomainModel.Entities
{
    public class Notification
    {
        private readonly List<Error> _errors = new List<Error>();

        public void AddError(string message)
        {
            _errors.Add(new Error(message));
        }

        public string ErrorMessage()
        {
            return string.Join(",", _errors.Select(x => x.Message));
        }

        public bool HasErrors()
        {
            return _errors.Any();
        }
    }
}
