using System;
using System.Threading.Tasks;
using MicroCqrs.Attributes;
using MicroCqrs.Interfaces;

namespace MicroCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class TryCatchHandlerAsync<TCmd> : ICmdHandlerAsync<TCmd>
    {
        protected abstract ICmdResult CmdResult { get; }

        // ReSharper disable once UnusedParameter.Global
        protected abstract Task ExecuteBody(TCmd cmd);

        private async Task<ICmdResult> TryCatchNext(TCmd cmd)
        {
            var current = CmdResult;
            
            try
            {
                await ExecuteBody(cmd);
            }
            catch (Exception ex)
            {
                current.AddError(ex.Message);
            }

            return current;
        }
        
        public async Task<ICmdResult> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
}