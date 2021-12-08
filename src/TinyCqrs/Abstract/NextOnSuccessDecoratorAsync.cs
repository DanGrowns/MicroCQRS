using System;
using System.Threading.Tasks;
using TinyCqrs.Attributes;
using TinyCqrs.Interfaces;
// TODO: Merging of core code where available.
namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class NextOnSuccessDecoratorAsync<TCmd> : ICmdHandlerAsync<TCmd>
    {
        private ICmdHandlerAsync<TCmd> Next { get; }
        protected abstract ICmdResult CmdResult { get; set; }

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
    
    [CqrsIgnore]
    public abstract class NextOnSuccessDecoratorAsync<TCmd, TResult> : ICmdHandlerAsync<TCmd, TResult> 
        where TResult : ICmdResult
    {
        private ICmdHandlerAsync<TCmd, TResult> Next { get; }
        protected abstract TResult CmdResult { get; set; }

        protected NextOnSuccessDecoratorAsync(ICmdHandlerAsync<TCmd, TResult> next)
            => Next = next;
        
        // ReSharper disable once UnusedParameter.Global
        protected abstract Task ExecuteBody(TCmd cmd);

        private async Task<TResult> TryCatchNext(TCmd cmd)
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
        
        public async Task<TResult> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
}