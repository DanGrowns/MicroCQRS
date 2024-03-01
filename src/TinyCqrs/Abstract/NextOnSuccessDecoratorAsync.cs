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
        protected CmdResult<object> CmdResult { get; init; }

        protected NextOnSuccessDecoratorAsync(ICmdHandlerAsync<TCmd> next)
            => Next = next;

        protected abstract Task ExecuteBody(TCmd cmd);
        
        private async Task<ICmdResult<object>> TryCatchNext(TCmd cmd)
        {
            var current = CmdResult ?? new CmdResult<object>();
            
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
        
        public async Task<ICmdResult<object>> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
    
    [CqrsIgnore]
    public abstract class NextOnSuccessDecoratorAsync<TCmd, TOutput> : ICmdHandlerAsync<TCmd, TOutput> 
    {
        private ICmdHandlerAsync<TCmd, TOutput> Next { get; }
        protected ICmdResult<TOutput> CmdResult { get; set; }

        protected NextOnSuccessDecoratorAsync(ICmdHandlerAsync<TCmd, TOutput> next)
            => Next = next;

        protected abstract Task ExecuteBody(TCmd cmd);
        
        private async Task<ICmdResult<TOutput>> TryCatchNext(TCmd cmd)
        {
            var current = CmdResult ?? new CmdResult<TOutput>();
            
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
        
        public async Task<ICmdResult<TOutput>> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
}