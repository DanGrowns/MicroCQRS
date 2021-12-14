using System;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class NextOnSuccessDecorator<TCmd> : 
        NextOnSuccessDecorator<TCmd, CmdResult>
    {
        protected NextOnSuccessDecorator(ICmdHandler<TCmd, CmdResult> next) 
            : base(next) { }
    }
    
    [CqrsIgnore]
    public abstract class NextOnSuccessDecorator<TCmd, TResult> : ICmdHandler<TCmd, TResult>
        where TResult : ICmdResult, new()
    {
        private ICmdHandler<TCmd, TResult> Next { get; }
        protected TResult CmdResult { get; set; }

        protected NextOnSuccessDecorator(ICmdHandler<TCmd, TResult> next)
            => Next = next;
        
        protected abstract void ExecuteBody(TCmd cmd);
        
        private TResult TryCatchNext(TCmd cmd)
        {
            var current = CmdResult ?? new TResult();
            
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
        
        public TResult Execute(TCmd cmd)
            => TryCatchNext(cmd);
    }
}