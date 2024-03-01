using System;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class TryCatchHandler<TCmd> : ICmdHandler<TCmd>
    {
        protected CmdResult<object> CmdResult { get; set; }
        protected abstract void ExecuteBody(TCmd cmd);

        private CmdResult<object> TryCatchNext(TCmd cmd)
        {
            var current = CmdResult ?? new CmdResult<object>();
            
            try
            {
                ExecuteBody(cmd);
            }
            catch (Exception ex)
            {
                current.AddIssue(ex.Message);
            }

            return current;
        }
        
        public ICmdResult<object> Execute(TCmd cmd)
            => TryCatchNext(cmd);
    }
    
    [CqrsIgnore]
    public abstract class TryCatchHandler<TCmd, TOutput> : ICmdHandler<TCmd, TOutput>
    {
        protected ICmdResult<TOutput> CmdResult { get; set; }
        protected abstract void ExecuteBody(TCmd cmd);

        private ICmdResult<TOutput> TryCatchNext(TCmd cmd)
        {
            var current = CmdResult ?? new CmdResult<TOutput>();
            
            try
            {
                ExecuteBody(cmd);
            }
            catch (Exception ex)
            {
                current.AddIssue(ex.Message);
            }

            return current;
        }
        
        public ICmdResult<TOutput> Execute(TCmd cmd)
            => TryCatchNext(cmd);
    }
}