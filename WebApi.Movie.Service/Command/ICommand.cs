namespace WebApi.Movie.Service.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface ICommand
    {
        void Execute();
    }
}
