using System;
using System.Threading.Tasks;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

// TODO: Code sharing.
namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class TryCatchHandlerAsync<TCmd> : ICmdHandlerAsync<TCmd>
    {
        protected ICmdResult<object> CmdResult { get; set; }
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

            return current;
        }
        
        public async Task<ICmdResult<object>> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
    
    [CqrsIgnore]
    public abstract class TryCatchHandlerAsync<TCmd, TOutput> : ICmdHandlerAsync<TCmd, TOutput>
    {
        protected ICmdResult<TOutput> CmdResult { get; set; }
        protected abstract Task ExecuteBody(TCmd cmd);

        private async Task<ICmdResult<TOutput>> TryCatchNext(TCmd cmd)
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

            return current;
        }
        
        public async Task<ICmdResult<TOutput>> Execute(TCmd cmd)
            => await TryCatchNext(cmd);
    }
}