using System;
using System.Threading.Tasks;
using TinyCqrs.Attributes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class NextOnSuccessDecoratorAsync<TCmd> : ICmdHandlerAsync<TCmd>
    {
        private ICmdHandlerAsync<TCmd> Next { get; }
        protected abstract ICmdResult CmdResult { get; }

        protected NextOnSuccessDecoratorAsync(ICmdHandlerAsync<TCmd> next)
            => Next = next;
        
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
                current.AddIssue(ex.Message);
            }
            
            if (current.Success)
            {
                if (Next == null) 
                    return current;
                
                var nextResult = await Next.Execute(cmd);
                return nextResult;
            }

            return current;
        }
        
        public async Task<ICmdResult> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
}