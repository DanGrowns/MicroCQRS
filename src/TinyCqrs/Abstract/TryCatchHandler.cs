using System;
using TinyCqrs.Attributes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Abstract
{
    [CqrsIgnore]
    public abstract class TryCatchHandler<TCmd> : ICmdHandler<TCmd>
    {
        protected abstract ICmdResult CmdResult { get; }

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
                current.AddError(ex.Message);
            }

            return current;
        }
        
        public ICmdResult Execute(TCmd cmd)
            => TryCatchNext(cmd);
    }
}