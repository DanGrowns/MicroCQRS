using System;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class NextOnSuccessDecorator<TCmd> : ICmdHandler<TCmd>
    {
        private ICmdHandler<TCmd> Next { get; }
        protected CmdResult<object> CmdResult { get; set; }

        protected NextOnSuccessDecorator(ICmdHandler<TCmd> next) => Next = next;

        protected abstract void ExecuteBody(TCmd cmd);
        
        private ICmdResult<object> TryCatchNext(TCmd cmd)
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
            
            if (current.Success)
            {
                if (Next == null)
                    return current;
                
                var nextResult = Next.Execute(cmd);
                return nextResult;
            }

            return current;
        }
        
        public ICmdResult<object> Execute(TCmd cmd)
            => TryCatchNext(cmd);
    }
    
    [CqrsIgnore]
    public abstract class NextOnSuccessDecorator<TCmd, TOutput> : ICmdHandler<TCmd, TOutput>
    
    {
        private ICmdHandler<TCmd, TOutput> Next { get; }
        protected ICmdResult<TOutput> CmdResult { get; set; }
        
        protected NextOnSuccessDecorator(ICmdHandler<TCmd, TOutput> next) => Next = next;
        
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
            
            if (current.Success)
            {
                if (Next == null)
                    return current;
                
                var nextResult = Next.Execute(cmd);
                return nextResult;
            }

            return current;
        }
        
        public ICmdResult<TOutput> Execute(TCmd cmd)
            => TryCatchNext(cmd);
    }
}