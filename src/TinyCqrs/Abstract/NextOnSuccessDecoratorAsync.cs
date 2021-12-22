using System;
using System.Threading.Tasks;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class NextOnSuccessDecoratorAsync<TCmd> : ICmdHandlerAsync<TCmd>
    {
        private ICmdHandlerAsync<TCmd> Next { get; }
        protected CmdResult CmdResult { get; set; }

        protected NextOnSuccessDecoratorAsync(ICmdHandlerAsync<TCmd> next)
            => Next = next;

        protected abstract Task ExecuteBody(TCmd cmd);
        
        private async Task<CmdResult> TryCatchNext(TCmd cmd)
        {
            var current = CmdResult ?? new CmdResult();
            
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
        
        public async Task<CmdResult> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
    
    [CqrsIgnore]
    public abstract class NextOnSuccessDecoratorAsync<TCmd, TResult> : ICmdHandlerAsync<TCmd, TResult> 
        where TResult : ICmdResult, new()
    {
        private ICmdHandlerAsync<TCmd, TResult> Next { get; }
        protected TResult CmdResult { get; set; }

        protected NextOnSuccessDecoratorAsync(ICmdHandlerAsync<TCmd, TResult> next)
            => Next = next;

        protected abstract Task ExecuteBody(TCmd cmd);
        
        private async Task<TResult> TryCatchNext(TCmd cmd)
        {
            var current = CmdResult ?? new TResult();
            
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