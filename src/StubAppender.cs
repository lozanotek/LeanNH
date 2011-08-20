namespace LeanNH {
    using System;
    using log4net.Appender;
    using log4net.Core;

    [Serializable]
    public class StubAppender : AppenderSkeleton {
        public StubAppender() {
            Name = GetType().Name;
        }

        public override void ClearFilters() { }

        protected override void Append(LoggingEvent[] loggingEvents) { }

        protected override bool IsAsSevereAsThreshold(Level level) {
            return true;
        }

        protected override bool PreAppendCheck() {
            return true;
        }

        protected override void OnClose() { }

        protected override bool FilterEvent(LoggingEvent loggingEvent) {
            return true;
        }

        public override void AddFilter(log4net.Filter.IFilter filter) { }

        protected override void Append(LoggingEvent loggingEvent) { }
    }
}