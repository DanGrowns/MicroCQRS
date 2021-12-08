using System;
using TinyCqrs.Attributes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class NextOnSuccessDecorator<TCmd> : ICmdHandler<TCmd>
    {
        private ICmdHandler<TCmd> Next { get; }
        protected abstract ICmdResult CmdResult { get; set; }

        protected NextOnSuccessDecorator(ICmdHandler<TCmd> next)
            => Next = next;
        
        // ReSharper disable once UnusedParameter.Global
        protected abstract void ExecuteBody(TCmd cmd);

        private ICmdResult TryCatchNext(TCmd cmd)
        {
            var current = CmdResult;
            
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
        
        public ICmdResult Execute(TCmd cmd)
            => TryCatchNext(cmd);
    }
}